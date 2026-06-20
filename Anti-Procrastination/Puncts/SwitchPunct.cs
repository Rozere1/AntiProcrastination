using Anti_Procrastination;
using System.IO.Pipes;

public class SwitchModulePunct : IPunct
{


    public void Activate()
    {
        using var client = new NamedPipeClientStream(".","JobModule", PipeDirection.Out);
        client.ConnectAsync();
        using var writer = new StreamWriter(client);
        writer.AutoFlush = true;
        writer.WriteLine("Switch");
        return;
    }
}
