using Anti_Procrastination;
using Anti_Procrastination.Services;
using System.IO.Pipes;
using System.Timers;

public class TimeBlockerModule : BlackListModule
{
    public ReactiveProperty<int> UseTime { get; protected set; }
    public ReactiveProperty<int> RemainingTime = new ReactiveProperty<int>();
    public bool IsOvered { get; set; }
    
    private JobModule jobModule;
    private int defaultTime = 10800;
    public override void Init()
    {
        UseTime = new ReactiveProperty<int>();
        Update();

    }
    private void Update()
    {
        var settings = SaveManager.Instance.LoadSettings();
        settings.UseTime = UseTime.Value;
    }
    private void OnTimeOvered(object? sender, ElapsedEventArgs e)
    {
        RemainingTime.Value = UseTime.Value;
    }

    private void OnQuit(object? sender, EventArgs e)
    {        
        
    }

    public async override void Activate()
    {

        var now = DateTime.Now;
        var date = new DateTime(now.Year, now.Month, now.Day + 1, 0, 0, 0);
        var timer = new ScheduleTimer(date);
        timer.OnTimeOver += Reset();
        jobModule = ServiceLocator.Instance.Get<JobModule>();
        if (UseTime.Value <= 600)
            UseTime.Value = defaultTime;
        if (Program.IsSetting)
            return;
        if (IsOvered)
        {
            KillAllProcesses();
            return;
        }
        await System.Threading.Tasks.Task.Run(HookProcesses);
        await System.Threading.Tasks.Task.Run(StartTimer);

    }

    private ElapsedEventHandler Reset()
    {
        RemainingTime.Value = UseTime.Value;
        return default;
    }

    private async void StartTimer()
    {

        while (true)
        {
            if (IsBlackList)
            {
                await System.Threading.Tasks.Task.Delay(1000);
                RemainingTime.Value -= 1;
                if (RemainingTime.Value <= 0)
                {
                    await System.Threading.Tasks.Task.Run(KillAllProcesses);
                    IsOvered = true;
                    break;
                }
            }
            else
            {
                await System.Threading.Tasks.Task.Delay(1000);
            }

        }

    }
    private void KillAllProcesses()
    {
        jobModule.SafeEnable();
        jobModule.Activate();
    }

    protected override async void StartServer()
    {
        using var server = new NamedPipeServerStream("TimeBlocker", PipeDirection.In);
        server.WaitForConnectionAsync();
        using var reader = new StreamReader(server);
        string command = await reader.ReadLineAsync();
        
        if (command == "Update")
        {
            Update();
            
        }
        
    }
}
