using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Timers;

public class SleepModule : Module, ISwitch
{
    public bool IsRun { get; set; }
    public int Hours { get; private set; }
    public ScheduleTimer Timer { get; private set; }
    public SleepModule() : base()
    {
        pipeName = "Sleep";
        Init();
        Update();
        Timer = new ScheduleTimer(SetDate());
        Timer.timer.Elapsed += OnTimeOvered;

    }
    private DateTime SetDate()
    {
        var now = DateTime.Now;
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
        try
        {
            var background = ReadCommand(stoppingToken);
        }
        catch(Exception ex)
        {

        }
        Activate();

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
        switch (command)
        {
            case "update":
                Update();
                break;
            case "switch":
                Switch();
                break;

        }
    }

    public void Switch()
    {
        IsRun = !IsRun;
    }
}