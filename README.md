# Virtual-Drummer
Virtual Drummer is a simple project that uses VST3 technology to send MIDI information created in Digital Audio Workstations (DAWs) to a Unity program able to animate this information in 3D. It uses TCP/IP sockets for communication between the plugin and Unity and it uses the VST3 interface to communicate with the DAW.

## Installation manual

Prior to using the application, it is essential to install the project and the Digital Audio Workstation on your machine. This process is really simple; nonetheless, the essential steps are included in this brief installation manual.

### 1. Install the Digital Audio Workstation
The only DAW used in the project, whose functionality has been proven effective, is Ableton Live 12. Ableton Live offers a free 1-month trial for users to try the application without needing to enter payment details.
To download the Ableton Live 12 Trial, access the following link: https://www.ableton.com/en/trial/

Click on “Download” and wait for the content to finish downloading. Once downloaded, you will see a compressed file; unzip it and run the installer. You should see a screen similar to the following:

Accept the agreements, select a destination directory, and continue with the installation. Once finished, click “Finish” and the installer will close. Now run the program and accept the permissions. You should see the following window on the screen:

If you want to try Ableton Live 12 with access to save and export project options, click on “Start your free trial” and you will be redirected to the Ableton Live website registration page. Create an account or log in. The page will try to redirect you to the Ableton application; accept and you should see the following message:

Click the “OK” button, and you will have the application ready to use without issues, being able to save and export your projects if needed.

### 2. Install Virtual Drummer
Installing the project is really simple. To download the first version, access the following link: https://github.com/carlos-bermejo/Virtual-Drummer/releases/tag/1.0.0

Download “VirtualDrummer.zip” by clicking on the link with the same name in the asset list of the Virtual Drummer release 1.0.0. Once downloaded, unzip it into a directory.

The installation would be complete. If you want to start the application, go to “animator” and you should see an executable file “unity-project.exe”. Run this file, and you will have the viewer application fully operational.

## User manual

As specified in the project requirements, the application values simplicity and ease of use. This philosophy is respected in the final product design, thus promoting experimentation with the tool and recreational use. In this section, we will explain in a simple and visual manner how to use the application.

### 1. Digital Audio Workstation
For this manual, Ableton Live 12 trial version has been used as the workstation, but there are multiple DAWs compatible with VST3 plug-ins.

The first step is to start the program. It is recommended that if you want to track the program's execution, make sure to host Ableton Live in a directory that does not require write permissions or, alternatively, run the program with administrator privileges.

Once inside, you will see something similar to what is shown in the image (Figure X). To use the event transmitter plug-in, you need to add the path where the plug-in is located. In the top menu, go to “Options,” “Preferences” (or press the “CONTROL” + “,” combination), and a preferences window should open.

In the sidebar, look for the “Plug-ins” option and access it. Then, go to the personal VST3 plug-ins folder and use the “Browse” button (Figure X). Select the directory where the VST3 event transmitter plug-in is located (the default name for this plug-in is “socket-plugin.vst3”).

Next, close the preferences window and go to the sidebar menu of the Ableton Live interface. Within the categories, go to “Plug-ins,” and you should be able to see the event transmitter plug-in in the list, as shown in the image:

Now drag the plug-in onto a MIDI event track (the word “MIDI” should appear in the track header). If everything has gone well, the plug-in window should open, which, since it has no parameters or interface, is a black window. Additionally, the track name should match the plug-in’s name.

Double-click on one of the rows of the MIDI track. A MIDI playback section should then be created.

By selecting the created section, a keyboard representing the MIDI playback track will appear at the bottom of the screen. You can now enter the desired notes by double-clicking on the track at the desired position. You can also edit the length of the notes by dragging their edges or move them at will.

### 2. Viewer Application
To start the viewer, open the directory where the application has been downloaded and access the executable file with the EXE extension that contains the program.

Once the application is running, you don’t have to do anything else; Unity’s socket server should start correctly and receive data from the plug-in. Now play the MIDI event track in Ableton as shown in the image:

You should then see in the viewer how the drummer character moves according to the notes entered. If you want to change the view, simply press one of the buttons located in the top left interface of the viewer.
