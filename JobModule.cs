public class JobModule
{
    public List<string> JobProcessesName { get; private set; }

    private bool _isRun = false;

    public JobModule(List<string> jobProcessesName)
    {
        JobProcessesName = jobProcessesName;

    }

    public void Switch()
    {
        _isRun = !_isRun;
        if (_isRun)
            HookProcesses();
    }
    private void ScanForProcesses()
    {
        var processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            if (!JobProcessesName.Contains(process.ProcessName))
            {
                process.Close();
                process.Kill();
                Logger.Write(process.ProcessName + " Killed");
            }
        }

    }

    private async void HookProcesses()
    {
        while (_isRun)
        {
            ScanForProcesses();
            await Task.Delay(1000);
        }
    }

}