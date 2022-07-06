using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ServerBase.Interfaces;
using ServerBase.Requests;
using ServerBase.Response;

namespace ServerBase.Handlers;

public class ControllersHandler : IHandler
{
    private readonly Dictionary<string, Func<object>> _routes;

    public ControllersHandler(Assembly controllersAssembly)
    {
        _routes = controllersAssembly.GetTypes()
            .Where(x => typeof(IController).IsAssignableFrom(x))
            .SelectMany(Controller => Controller.GetMethods().Select(Method => new
            {
                Controller,
                Method
            })).ToDictionary(
                key => GetPath(key.Controller, key.Method),
                value => GetEndPointMethod(value.Controller, value.Method)
            );
    }

    private Func<object> GetEndPointMethod(Type conroller, MethodInfo method)
    {
        return () => method.Invoke(Activator.CreateInstance(conroller), Array.Empty<object>());
    }

    private string GetPath(Type controller, MethodInfo method)
    {
        var name = controller.Name;
        if (name.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
            name = name[..^"controller".Length];
        if (method.Name.Equals("Index", StringComparison.InvariantCultureIgnoreCase))
            return "/" + name;
        return "/" + name + "/" + method.Name;
    }

    public void Handle(Stream stream, Request request)
    {
        if(!_routes.TryGetValue(request.Path, out var func))
            ResponseWriter.WriteStatus(HttpStatusCode.NotFound, stream);
        else
        {
            ResponseWriter.WriteStatus(HttpStatusCode.OK, stream);
            WriteControllerResponse(func(), stream);
        }
    }

    private void WriteControllerResponse(object response, Stream stream)
    {
        if (response is string str)
        {
            using var writer = new StreamWriter(stream, leaveOpen: true);
            writer.Write(str);
        }
        else if (response is byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }
        else
        {
           WriteControllerResponse(JsonConvert.SerializeObject(response), stream);
        }
    }
}