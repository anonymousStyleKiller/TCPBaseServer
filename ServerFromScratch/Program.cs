using ServerBase;
using ServerBase.Handlers;

var host = new Server(new ControllersHandler(typeof(Program).Assembly));
await host.StartV2Async();

