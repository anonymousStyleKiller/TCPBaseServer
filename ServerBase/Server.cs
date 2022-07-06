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

    public void StartV1()
    {
        Console.WriteLine("Server started V1");
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

    public async Task StartV2Async()
    {
        Console.WriteLine("Server started V2");
        var tcpListener = new TcpListener(IPAddress.Any, 5000);
        tcpListener.Start();
        while (true)
        {
            using var client = await tcpListener.AcceptTcpClientAsync();
            var _ = ProcessClientAsync(client);
        }
    }

    private async Task ProcessClientAsync(TcpClient client)
    {
        try
        {
            using var stream = client.GetStream();
            using var reader = new StreamReader(stream);
            var firstLine = await reader.ReadLineAsync();
            for (string line = null; line != string.Empty; line = await reader.ReadLineAsync())
                ;

            var request = RequestParser.Parse(firstLine);
            await _handler.HandleAsync(stream, request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}