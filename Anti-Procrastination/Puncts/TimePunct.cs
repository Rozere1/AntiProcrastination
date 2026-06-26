using System.IO.Pipes;

public class TimePunct : IPunct
{
    private Settings settings;
    public TimePunct()
    {
        settings = SaverManager.Instance.LoadSettings();
    }
    public void Activate()
    {


        Console.Clear();
        if (settings.TimeRemaining <= settings.UseTime * 0.15)
        {
            Console.WriteLine("Вы не можете поменять время из-за малого оставшегося времени");
            Console.ReadLine();
            return;
        }
        Console.Write("Введите время использования в секундах: ");
        try
        {
            int value = Convert.ToInt32(Console.ReadLine());
            if (value > 25200 || value < 600)
            {
                Console.WriteLine("Нельзя ввести данное значение");
                Console.ReadKey();
                Activate();
                return;
            }
            Console.WriteLine($"Вы уверены в выбранном значении {TimeFormatter.Format(value)} (Y/N)");
            var ans = Console.ReadLine();
            if (ans.ToUpper() == "Y")
            {
                using var client = new NamedPipeClientStream(".", "TimeBlocker", PipeDirection.Out);
                client.Connect(5000);
                SaverManager.Instance.SaveSettings(SettingType.UseTime, value);
                using var writer = new StreamWriter(client);
                writer.AutoFlush = true;
                writer.WriteLine("Update");
            }
            else if (ans.ToUpper() == "N")
            {
                Activate();
            }
            else
            {
                return;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}\n{ex.StackTrace}");
            Console.WriteLine("Нажмите, чтобы продолжить");
            Console.ReadKey();
            Activate();
            return;
        }

    }
}
public class SleepPunct : IPunct
{

    public void Activate()
    {
        Console.Clear();

        Console.Write("Введите время использования в часах: ");
        try
        {
            int value = Convert.ToInt32(Console.ReadLine());
            if (value >= 24 || value < 0)
            {
                Console.WriteLine("Нельзя установить такое время");
                Console.ReadLine();

            }
            SaverManager.Instance.SaveSettings(SettingType.SleepHour, value);
            using var client = new NamedPipeClientStream(".", "Sleep", PipeDirection.Out);

            client.Connect(5000);
            using var writer = new StreamWriter(client);
            writer.AutoFlush = true;
            writer.WriteLine("Update");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}\n{ex.StackTrace}");
            Console.WriteLine("Нажмите, чтобы продолжить");
            Console.ReadKey();
            Activate();
        }

    }
}