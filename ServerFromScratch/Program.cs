using ServerBase;
using ServerBase.Handlers;

var paths = Directory.GetCurrentDirectory().Split("\\");
var currentDirectory = Path.Combine(paths[0], paths[1], paths[2],paths[3], paths[4]);
var host = new Server(new StaticFileHandler(Path.Combine(currentDirectory, "wwwroot")));
host.Start();

