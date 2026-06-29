using Microsoft.Extensions.Logging;
using System.Timers;

public class TimeBlockerModule : BlackListModule
{
    private ScheduleTimer Timer;
    private ILogger<TimeBlockerModule> _logger;
    public TimeBlockerModule(JobModule module, ILogger<TimeBlockerModule> logger) : base()
    {
        _logger = logger;
        var now = DateTime.Now;
        var lastStart = SaverManager.Instance.LoadSettings().Date;
        if (lastStart.Day < now.Day)
            RemainingTime = UseTime;
        var date = new DateTime(now.Year, now.Month, now.Day + 1, 0, 0, 0);
        Timer = new ScheduleTimer(date);
        Timer.Start();
        Timer.timer.Elapsed += Reset;
        pipeName = "TimeBlocker";
        jobModule = module;
        Init();
        Update();
        
    }
    public int UseTime;
    public int RemainingTime;
    public bool IsOvered { get; set; }

    private JobModule jobModule;
    private void Update()
    {
        var settings = SaverManager.Instance.LoadSettings();
        UseTime = settings.UseTime;
        RemainingTime = settings.TimeRemaining;
    }


    public async override void Activate()
    {
        if (IsOvered)
        {
            KillAllProcesses();
            return;
        }
        HookProcesses();
        StartTimer();
        BannedProcesses.Clear();
    }

    private void Reset(object? sender, ElapsedEventArgs e)
    {
        RemainingTime = UseTime;
        
    }


    private async void StartTimer()
    {
        if (IsBlackList)
        {
            RemainingTime -= 1;
            if (RemainingTime <= 0)
            {
                KillAllProcesses();
                IsOvered = true;
            }
        }
    }
    private void KillAllProcesses()
    {
        jobModule.SafeEnable();
        jobModule.Activate();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        try
        {
            var background = ReadCommand(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
                _logger.LogInformation($"Time Remaining: {RemainingTime}");
                Activate();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }
    public override async Task StopAsync(CancellationToken stoppingToken)
    {

        server.Dispose();
        Timer.timer.Elapsed -= Reset;
        Timer.timer.Dispose();
        SaverManager.Instance.SaveSettings(SettingType.TimeRemaining, RemainingTime);
        await base.StopAsync(stoppingToken);
    }

    protected override void CheckCommand(string? command)
    {
        switch (command)
        {
            case "update":
                Update();
                break;
        }
    }
}
