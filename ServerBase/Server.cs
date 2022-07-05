using System.Net;
using System.Net.Sockets;
using ServerBase.Interfaces;
using ServerBase.Parsers;

namespace ServerBase;

public class Server
{
    private readonly IHandler _handler;

    public Server(IHandler handler)
    {
        _handler = handler;
    }

    public void Start()
    {
        var tcpListener = new TcpListener(IPAddress.Any, 5000);
        tcpListener.Start();
        while (true)
        {
            try
            {
                using var client = tcpListener.AcceptTcpClient();
                using var stream = client.GetStream();
                using var reader = new StreamReader(stream);
                var firstLine = reader.ReadLine();
                for (string line = null; line != string.Empty; line = reader.ReadLine())
                    ;

                var request = RequestParser.Parse(firstLine);
                _handler.Handle(stream, request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
       
    }
}