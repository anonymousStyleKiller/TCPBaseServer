namespace ServerBase.Interfaces;

public interface IHandler
{
    void Handle(Stream networkStream);
}