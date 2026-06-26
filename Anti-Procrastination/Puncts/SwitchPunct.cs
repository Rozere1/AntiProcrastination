using System.IO.Pipes;

public class SwitchModulePunct : IPunct
{


    public async void Activate()
    {
        using var client = new NamedPipeClientStream(".", "Job", PipeDirection.Out);
        try
        {
            client.Connect(5000);
            using var writer = new StreamWriter(client);
            writer.AutoFlush = true;
            writer.WriteLine("Switch");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}\n{ex.StackTrace}");
            Console.WriteLine("Нажмите, чтобы продолжить");
            Console.ReadKey();
        }

    }
}
