using Anti_Procrastination;
using System.Diagnostics;

public abstract class Module
{
    protected List<string> _blackList;
    protected List<Process> _bannedProcesses = new List<Process>();
    protected Process[] _currentProcesses;
    protected bool _isBlackList;
    private ProgramListManager programListManager;
    public Module()
    {
        programListManager = ServiceLocator.Instance.Get<ProgramListManager>();
        _blackList = programListManager.ReadAList("BlackList.txt");
        Program.BlackListChanged += OnBlackListChanged;

        
    }

    private void OnBlackListChanged()
    {
        _blackList = programListManager.ReadAList("BlackList.txt");
        
    }
    


    protected abstract void Activate();
    protected async void HookProcesses()
    {

            _currentProcesses = Process.GetProcesses();
            for (int i = 0; i < _currentProcesses.Length; i++)
            {
                if (_blackList.Contains(_currentProcesses[i].ProcessName))
                {
                    _bannedProcesses.Add(_currentProcesses[i]);
                    _isBlackList = true;
                    
                }
                
            }
            if(_bannedProcesses.Count == 0)
            {
                _isBlackList = false;
            }
            
    }
}
