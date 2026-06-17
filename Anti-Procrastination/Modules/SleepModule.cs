using Anti_Procrastination;
using System.Diagnostics;
using System.IO.Pipes;
using System.Timers;

public class SleepModule : Module
{
    public ReactiveProperty<int> Hours { get; private set; }
    public ScheduleTimer Timer { get; private set; }
 
    private DateTime SetDate()
    {
        
        var now = DateTime.UtcNow;
        var date = new DateTime(now.Year, now.Month, now.Day, Hours.Value, 0, 0);
        return date;
    }
    
    public override void Activate()
    {
        Timer.Start();
    }

    private void Update()
    {
        var settings = SaveManager.Instance.LoadSettings();
        Hours.Value = settings.SleepHour;
    }


    private void OnTimeOvered(object? sender, ElapsedEventArgs e)
    {
        Sleep();
    }

    private void Sleep()
    {
        var shut = new ProcessStartInfo();
        shut.FileName = "cmd";
        shut.Arguments = "/c shutdown /s /t 120 /c \"Иди спать через 2 минуты\"";
        shut.CreateNoWindow = false;
        shut.UseShellExecute = false;
        Process.Start(shut);
    }

    protected override void StartServer()
    {
        using var server = new NamedPipeServerStream("Sleep", PipeDirection.In);
        server.WaitForConnectionAsync();
        using var reader = new StreamReader(server);
        if(reader.ReadLine() == "Update")
        {
            Update();
            Timer.Date = SetDate();
            Timer.Start();
        }    
    }

    public override void Init()
    {
        Hours = new ReactiveProperty<int>();
        Update();
        Timer = new ScheduleTimer(SetDate());
    }
}