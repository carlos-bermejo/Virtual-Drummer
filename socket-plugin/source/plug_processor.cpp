//------------------------------------------------------------------------
// Copyright(c) 2024 Carlos Bermejo.
//------------------------------------------------------------------------

#include "plug_processor.h"
#include "plug_cids.h"

#include "base/source/fstreamer.h"
#include "pluginterfaces/vst/ivstparameterchanges.h"

#include <WinSock2.h> //Para los sockets de la API de Windows (incluir "C:\Program Files (x86)\Windows Kits\10\Include\10.0.22621.0\um" en directorios adicionales)
#include <WS2tcpip.h> // Para trabajar con TCP/IP
#include <iostream> //Entrada y salida de datos en C++ (cin, cout, cerr...)
#include <shlwapi.h> //Para operaciones con rutas y archivos (incluir "shlwapi.lib" en el vinculador)
#include <string> //Para trabajar con cadenas
#include <fstream> // Para trabajar con archivos (log)

#include "pluginterfaces/base/funknown.h" //Para trabajar con mensajes MIDI
#include "pluginterfaces/vst/ivstevents.h"

using namespace Steinberg;

namespace MyCompanyName {


void socket_pluginProcessor::PrintToLogFile(const char* message) {
	std::ofstream logfile("log.txt", std::ios_base::app); // opens or creates file
	if (logfile.is_open()) {
		logfile << message << std::endl;
		logfile.close();
	}
	else {
		std::cerr << "Error opening log file!" << std::endl;
	}
}

void socket_pluginProcessor::sendToUnity(const char* data) {
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
		std::cerr << "WSAStartup failed." << std::endl;
		return;
	}

	SOCKET sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET) {
		PrintToLogFile(("Error creating socket: " + std::to_string(WSAGetLastError())).c_str());
		std::cerr << "Error creating socket: " << WSAGetLastError() << std::endl;
		WSACleanup();
		return;
	}

	sockaddr_in serverAddr;
	memset(&serverAddr, 0, sizeof(serverAddr));
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_port = htons(12345);
	inet_pton(AF_INET, "127.0.0.1", &serverAddr.sin_addr); //TODO: implement dynamic IP changes

	if (::connect(sock, (sockaddr*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
		std::cerr << "Unable to connect to server: " << WSAGetLastError() << std::endl;
		closesocket(sock);
		WSACleanup();
		return;
	}

	send(sock, data, strlen(data), 0);

	closesocket(sock);
	WSACleanup();
}

//------------------------------------------------------------------------
// socket_pluginProcessor
//------------------------------------------------------------------------
socket_pluginProcessor::socket_pluginProcessor ()
{
	//--- set the wanted controller for our processor
	setControllerClass (ksocket_pluginControllerUID);
}

//------------------------------------------------------------------------
socket_pluginProcessor::~socket_pluginProcessor ()
{}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::initialize (FUnknown* context)
{
	try {
		// Here the Plug-in will be instantiated


		//---always initialize the parent-------
		tresult result = AudioEffect::initialize(context);
		// if everything Ok, continue
		if (result == kResultOk)
		{
			PrintToLogFile("Plugin initializing...");

			addAudioInput(STR16("Stereo In"), Steinberg::Vst::SpeakerArr::kStereo);
			addAudioOutput(STR16("Stereo Out"), Steinberg::Vst::SpeakerArr::kStereo);
			addEventInput(STR16("Event Input"), 1);

			sendToUnity("Hola mundo");
			PrintToLogFile("Plugin initialized");
			return result;
		}
		return result;
	}
	catch (const std::exception& e)
	{
		PrintToLogFile(("Exception in initialize(): " + std::string(e.what())).c_str());
		return kResultFalse;
	}
	catch (...)
	{
		PrintToLogFile("Unknown exception in initialize()");
		return kResultFalse;
	}
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::terminate ()
{
	// Here the Plug-in will be de-instantiated, last possibility to remove some memory!
	
	//---do not forget to call parent ------
	return AudioEffect::terminate ();
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::setActive (TBool state)
{
	//--- called when the Plug-in is enable/disable (On/Off) -----
	return AudioEffect::setActive (state);
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::process (Vst::ProcessData& data)
{
	try {
		std::string log = "";
		log = "Processing method has been called... In:" + std::to_string(data.numInputs) + ", Out:" + std::to_string(data.numOutputs);
		PrintToLogFile(log.c_str());

		Vst::IEventList* events = data.inputEvents;
		if (!events) {
			//-------------PROCESAMIENTO CON AUDIO (sin testear)--------------------
			PrintToLogFile("--Audio Processing--");
			// flush case
			if (data.numInputs == 0 || data.numSamples == 0) {
				return kResultOk;
			}

			// bloque de audio
			Steinberg::Vst::Sample32** inputChannels = data.inputs[0].channelBuffers32;
			int32 numSamples = data.numSamples;
			int32 numChannels = data.inputs[0].numChannels;

			if (!inputChannels || numSamples <= 0 || numChannels <= 0) {
				PrintToLogFile("Error: Invalid input data");
				return kResultFalse;
			}

			// por cada canal y muestra obtener la amplitud
			float sum = 0.0f;
			for (int32 channelIndex = 0; channelIndex < numChannels; ++channelIndex) {
				PrintToLogFile("Entered channels");
				const float* channelData = inputChannels[channelIndex];
				for (int32 sampleIndex = 0; sampleIndex < numSamples; ++sampleIndex) {
					PrintToLogFile("Entered samples");
					sum += std::abs(channelData[sampleIndex]);
				}
			}

			float average = sum / (numSamples * numChannels); // amplitud promedia
			float sampleRate = data.processContext->sampleRate;
			float hertz = average * sampleRate; // herzios

			if (hertz != 0. || average != 0. || sum != 0.) {
				log = "Herzios: " + std::to_string(hertz) + ", Sum: " + std::to_string(sum) + ", Average: " + std::to_string(average);
				PrintToLogFile(log.c_str());
			}
		}
		else {
			//-------------PROCESAMIENTO CON EVENTOS--------------------
			PrintToLogFile("--Event Processing--");
			data.outputs[0].silenceFlags = 0;
			if (events) {
				int32 numEvents = events->getEventCount();
				for (int32 i = 0; i < numEvents; i++) {
					Vst::Event event;
					if (events->getEvent(i, event) == kResultOk) {
						PrintToLogFile("---Event received---");
						if (event.type == Vst::Event::kNoteOnEvent) {
							// datos de MIDI para pasar a Unity
							Steinberg::int16 pitch = event.noteOn.pitch;
							Steinberg::int16 velocity = event.noteOn.velocity;

							std::string midiMessage = "";
							midiMessage += "Note On - Pitch: " + std::to_string(pitch) + ", Velocity: " + std::to_string(velocity);

							PrintToLogFile(midiMessage.c_str());
							sendToUnity(midiMessage.c_str());
						}
						else {
							PrintToLogFile("'event' is not kNoteOnEvent");
						}
					}
					else {
						PrintToLogFile(("Couldn't obtain event #" + std::to_string(i)).c_str());
					}
				}
			}
			else {
				PrintToLogFile("No events");
			}
		}

		return kResultOk;
	}
	catch (const std::exception& e)
	{
		PrintToLogFile(("Exception in process(): " + std::string(e.what())).c_str());
		return kResultFalse;
	}
	catch (...)
	{
		PrintToLogFile("Unknown exception in process()");
		return kResultFalse;
	}
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::setupProcessing (Vst::ProcessSetup& newSetup)
{
	//--- called before any processing ----
	return AudioEffect::setupProcessing (newSetup);
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::canProcessSampleSize (int32 symbolicSampleSize)
{
	// by default kSample32 is supported
	if (symbolicSampleSize == Vst::kSample32)
		return kResultTrue;

	// disable the following comment if your processing support kSample64
	/* if (symbolicSampleSize == Vst::kSample64)
		return kResultTrue; */

	return kResultFalse;
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::setState (IBStream* state)
{
	// called when we load a preset, the model has to be reloaded
	IBStreamer streamer (state, kLittleEndian);
	
	return kResultOk;
}

//------------------------------------------------------------------------
tresult PLUGIN_API socket_pluginProcessor::getState (IBStream* state)
{
	// here we need to save the model
	IBStreamer streamer (state, kLittleEndian);

	return kResultOk;
}

//------------------------------------------------------------------------
} // namespace MyCompanyName
