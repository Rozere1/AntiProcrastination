using Anti_Procrastination;
using System.Diagnostics;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Timers;

public class SleepModule : Module
{
    
    public int Hours { get; private set; }
    public ScheduleTimer Timer { get; private set; }
    public SleepModule()
    {
        server = new NamedPipeServerStream("Sleep", PipeDirection.In);
        Update();
        Timer = new ScheduleTimer(SetDate());
        Timer.timer.Elapsed += OnTimeOvered;
    }
    private DateTime SetDate()
    {
        
        var now = DateTime.UtcNow;
        var date = new DateTime(now.Year, now.Month, now.Day, Hours, 0, 0);
        return date;
    }
    
    public override void Activate()
    {
        Timer.Start();
    }

    private void Update()
    {
        var settings = SaverManager.Instance.LoadSettings();
        Hours = settings.SleepHour;
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



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Activate();
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ReadCommand(stoppingToken);
            }
        }
        catch(OperationCanceledException)
        {
                
        }
    }
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        server.Dispose();
        Timer.timer.Elapsed -= OnTimeOvered;
        Timer.timer.Dispose();
        await base.StopAsync(stoppingToken);
    }

    protected override void CheckCommand(string? command)
    {
        switch(command)
        {
            case "update":
            Update();
            break;
            
        }
    }
}