using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

public class UnitySocketServer : MonoBehaviour
{
    public int port = 12345;
    private TcpListener listener;
    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();

    void Start()
    {
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
            //MiPrograma.Run("# " + data);
        }
    }
}
