

public class JobModule : Module, ISwitch, IService
{
    public ReactiveProperty<bool> IsRun { get; protected set; }
    private bool safeEnable;
    public async void Switch()
    {
        if (safeEnable)
            return;
        IsRun.Value = !IsRun.Value;
        if (IsRun.Value)
        {
            Activate();
        }
    }

    public JobModule() : base()
    {
        IsRun = new ReactiveProperty<bool>();
        
    }
    public void SafeEnable()
    {
        safeEnable = true;
        IsRun.Value = true;
        
    }
    
    protected async void KillBlackListProcesess()
    {
        if (_bannedProcesses.Count == 0)
            return;
        for (int i = 0; i < _bannedProcesses.Count; i++)
        {
            var process = _bannedProcesses[i];
            _bannedProcesses.Remove(process);
            process.CloseMainWindow();
            process.WaitForExit(5000);
            
            if (!process.HasExited)
            {
                process.Kill();
                process.WaitForExit();
            }

            Logger.Write(process.ProcessName + " Killed");
            
        }

    }


    public async override void Activate()
    {
        while (IsRun.Value)
        {
            HookProcesses();
            await Task.Run(KillBlackListProcesess);
            
            await Task.Delay(1000);
        }
    }


}