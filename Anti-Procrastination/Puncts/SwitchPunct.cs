using Anti_Procrastination;
using System.IO.Pipes;
using System.Threading.Tasks;

public class SwitchModulePunct : IPunct
{


    public async void Activate()
    {
        using var client = new NamedPipeClientStream(".","Job", PipeDirection.Out);
        await client.ConnectAsync();
        using var writer = new StreamWriter(client);
        writer.AutoFlush = true;
        writer.WriteLine("Switch");
    }
}
