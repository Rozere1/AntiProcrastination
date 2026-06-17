

using Anti_Procrastination;

public class JobModule : BlackListModule, ISwitch, IService
{
    public ReactiveProperty<bool> IsRun { get; protected set; }

    private bool safeEnable;
    public async void Switch()
    {
        if (safeEnable)
            return;
    }
    
    public override void Init()
    {
        IsRun = new ReactiveProperty<bool>();
        Update();
    }
    private void Update()
    {
        var settings = SaveManager.Instance.LoadSettings();
        IsRun.Value = settings.IsJobRun;
    }
    public void SafeEnable()
    {
        safeEnable = true;
        IsRun.Value = true;

    }

    protected async void KillBlackListProcesess()
    {
        if (BannedProcesses.Count == 0)
            return;
        for (int i = 0; i < BannedProcesses.Count; i++)
        {
            var process = BannedProcesses[i];
            BannedProcesses.Remove(process);
            process.CloseMainWindow();
            process.WaitForExit(5000);

            if (!process.HasExited)
            {
                process.Kill();
                process.WaitForExit();
            }

        }

    }


    public async override void Activate()
    {
        while (IsRun.Value)
        {
            HookProcesses();
            await System.Threading.Tasks.Task.Run(KillBlackListProcesess);

            await System.Threading.Tasks.Task.Delay(1000);
        }
    }

    protected override void StartServer()
    {
        throw new NotImplementedException();
    }
}