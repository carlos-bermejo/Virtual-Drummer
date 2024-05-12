using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

public class UnitySocketServer : MonoBehaviour
{
    public int port = 12345;
    private TcpListener listener;
    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    private MidiMapper midiMapper;

    void Start()
    {
        midiMapper = FindObjectOfType<MidiMapper>();
        if (midiMapper == null)
        {
            Debug.LogError("No object of type MidiMapper found in scene for UnitySocketServer.");
        }
        StartServer();
    }

    async void StartServer()
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Debug.Log("Servidor de sockets iniciado en el puerto " + port + "...");

        await AcceptConnections();
    }

    async Task AcceptConnections()
    {
        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Debug.Log("Conexión establecida");

            await Task.Run(() => HandleClient(client));
        }
    }

    async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Debug.Log("Datos recibidos: " + data);

        messageQueue.Enqueue(data);

        client.Close();
    }

    void Update()
    {
        while (messageQueue.TryDequeue(out string data))
        {
            //gets pitch and velocity from any string message
            string patronPitch = @"Pitch:\s*(\d+)";
            string patronVelocity = @"Velocity:\s*(\d+)";

            Match matchPitch = Regex.Match(data, patronPitch);
            Match matchVelocity = Regex.Match(data, patronVelocity);

            int pitch = 0;
            int velocity = 0;

            if (matchPitch.Success)
            {
                pitch = int.Parse(matchPitch.Groups[1].Value);
            }

            if (matchVelocity.Success)
            {
                velocity = int.Parse(matchVelocity.Groups[1].Value);
            }

            midiMapper.MapMidiToAction(pitch, velocity);
        }
    }
}
