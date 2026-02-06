using System.Diagnostics;

public class JobModule
{
    public List<string> JobProcessesName { get; private set; }

    public ReactiveProperty<bool> IsRun { get; private set; }
    
    public JobModule(List<string> jobProcessesName)
    {

        IsRun = new ReactiveProperty<bool>();
        JobProcessesName = jobProcessesName;

    }

    public void Switch()
    {
        IsRun.Value = !IsRun.Value;
        if (IsRun.Value)
        {
            HookProcesses();
        }
    }
    private void ScanForProcesses()
    {
        var processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            if (JobProcessesName.Contains(process.ProcessName))
            {

                process.Kill();
                Logger.Write(process.ProcessName + " Killed");
            }
        }

    }

    private async void HookProcesses()
    {
        while (IsRun.Value)
        {
            ScanForProcesses();
            await Task.Delay(5000);
        }
    }

}