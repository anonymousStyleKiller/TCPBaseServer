using System.Net;
using ServerBase.Interfaces;
using ServerBase.Parsers;
using ServerBase.Requests;
using ServerBase.Response;

namespace ServerBase.Handlers;

public class StaticFileHandler : IHandler
{
    private readonly string _path;

    public StaticFileHandler(string path)
    {
        _path = path;
    }


    public void Handle(Stream networkStream, Request request)
    {
        using (var writer = new StreamWriter(networkStream))
        {
            var filePath = Path.Combine(_path, request.Path.Substring(1));
                
            if (!File.Exists(filePath))
            {
                //TODO: write 404
                // HTTP/1.0 200 OK
                // HTTP/1.0 404 NotFound
                ResponseWriter.WriteStatus(HttpStatusCode.NotFound, networkStream);
            }
            else
            {
                ResponseWriter.WriteStatus(HttpStatusCode.OK, networkStream);
                using (var fileStream = File.OpenRead(filePath))
                {
                    fileStream.CopyTo(networkStream);
                }
            }
                
            System.Console.WriteLine(filePath);
        }
    }
    
}