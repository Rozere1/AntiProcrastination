

using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using Anti_Procrastination;

public class JobModule : BlackListModule, ISwitch, IService
{
    private NamedPipeServerStream server;
    public ReactiveProperty<bool> IsRun { get; protected set; }

    private bool safeEnable;
    public async void Switch()
    {
        if (safeEnable)
            return;
    }
    
    public async override void Init()
    {
        IsRun = new ReactiveProperty<bool>();
        StartServer();
        Update();
        await System.Threading.Tasks.Task.Run(ReadCommand);
    }
    private void Update()
    {
        var settings = SaverManager.Instance.LoadSettings();
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
        HookProcesses();
        await System.Threading.Tasks.Task.Run(KillBlackListProcesess);
        await System.Threading.Tasks.Task.Delay(1000);
    }

    protected async override void StartServer()
    {
        server = new NamedPipeServerStream("Job");
    }
    private async void ReadCommand()
    {
        while (true)
        {
            await server.WaitForConnectionAsync();
            using var reader = new StreamReader(server);
            var command = reader.ReadLine();
            switch(command)
            {
                case "Update":
                Update();
                break;
                case "Switch":
                Switch();
                break;
                
            }
        }
        
    }

    protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)      
        {
            await  System.Threading.Tasks.Task.Run(Activate);
        }
        server.Dispose();
    }
}