using System.Net;
using System.Net.Sockets;

namespace CustomServer;

public class Server
{
    public void Start()
    {
        var listener = new TcpListener(IPAddress.Any, 81);
        listener.Start();
        var client = listener.AcceptTcpClient();
        using var stream = client.GetStream();
        using var reader = new StreamReader(stream);
        using var writer = new StreamWriter(stream);
        while (true)
        {
            for(string? line = null; line != string.Empty; line = reader.ReadLine())
            {
                Console.WriteLine(line);
            }
            writer.WriteLine("Finish");
        }   
    }
}