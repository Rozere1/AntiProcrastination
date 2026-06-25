
using System.IO.Pipes;
using Anti_Procrastination;
using Microsoft.Extensions.Hosting;



public abstract class Module : BackgroundService, IService 
{
    protected abstract void CheckCommand(string? command);
    protected NamedPipeServerStream server;
    public abstract void Activate();
    protected async Task ReadCommand(CancellationToken stoppingToken)
    {
        await server.WaitForConnectionAsync(stoppingToken);
        using var reader = new StreamReader(server);
        var command = reader.ReadLine();
        CheckCommand(command);
    }

}
