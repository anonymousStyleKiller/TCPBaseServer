using System.Net;
using System.Net.Sockets;
using ServerBase.Interfaces;

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
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        var client = listener.AcceptTcpClient();
        using var stream = client.GetStream();
        _handler.Handle(stream);
    }
}