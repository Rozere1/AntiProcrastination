using Anti_Procrastination;
using Anti_Procrastination.Services;
using System.Diagnostics;

public abstract class BlackListModule : Module
{
    
    public List<string> BlackList;
    public List<Process> BannedProcesses = new List<Process>();
    public Process[] CurrentProcesses;
    public bool IsBlackList;
    private void OnListChanged(object obj)
    {
        FileSystemWatcher watcher = (FileSystemWatcher)obj;
        if (watcher.Path == @$"{Directory.GetCurrentDirectory()}\Lists")
        {
            BlackList = ProgramListManager.ReadAList("Blacklist.txt");
        }
    }
    public BlackListModule()
    {
        BlackList = ProgramListManager.ReadAList("Blacklist.txt");
        Program.FileChanged += OnListChanged;
    }
    protected async void HookProcesses()
    {

        CurrentProcesses = Process.GetProcesses();
        for (int i = 0; i < CurrentProcesses.Length; i++)
        {
            if (BlackList.Contains(CurrentProcesses[i].ProcessName.ToLower()))
            {
                BannedProcesses.Add(CurrentProcesses[i]);
                IsBlackList = true;
            }

        }
        if (BannedProcesses.Count == 0)
        {
            IsBlackList = false;
        }

    }
}
