
using Microsoft.Extensions.Hosting;
using System.IO.Pipes;



public abstract class Module : BackgroundService
{
    protected abstract void CheckCommand(string? command);
    protected NamedPipeServerStream server;
    protected string pipeName;
    protected void Init()
    {
        server = new NamedPipeServerStream(pipeName, PipeDirection.In);
    }
    public abstract void Activate();
    protected async Task ReadCommand(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await server.WaitForConnectionAsync(stoppingToken);
            var reader = new StreamReader(server);
            var command = await reader.ReadLineAsync();
            if (command != null)
            CheckCommand(command.ToLower());
            server.Disconnect();
        }
    }

}
