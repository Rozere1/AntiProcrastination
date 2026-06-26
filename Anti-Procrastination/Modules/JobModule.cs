public class JobModule : BlackListModule, ISwitch, IService
{
    public bool IsRun { get; protected set; }
    public JobModule() : base()
    {
        pipeName = "Job";
        Init();
        Update();
    }
    private bool safeEnable;
    public void Switch()
    {
        if (safeEnable)
            return;
        IsRun = !IsRun;
        SaverManager.Instance.SaveSettings(SettingType.IsJobRun, IsRun);
    }

    private void Update()
    {
        var settings = SaverManager.Instance.LoadSettings();
        IsRun = settings.IsJobRun;
    }
    public void SafeEnable()
    {
        safeEnable = true;
        IsRun = true;

    }

    protected void KillBlackListProcesess()
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
            }

        }

    }


    public async override void Activate()
    {
        if (!IsRun)
            return;
        HookProcesses();
        KillBlackListProcesess();

    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Activate();
                await ReadCommand(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {

        }

    }
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        server.Dispose();
        SaverManager.Instance.SaveSettings(SettingType.IsJobRun, IsRun);
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
}