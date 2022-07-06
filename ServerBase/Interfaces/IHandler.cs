using ServerBase.Requests;

namespace ServerBase.Interfaces;

public interface IHandler
{
    void Handle(Stream stream, Request request);
}