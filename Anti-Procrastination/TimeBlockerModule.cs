
using Anti_Procrastination;
using Newtonsoft.Json;

public class TimeBlockerModule : Module
{
    
    public ReactiveProperty<int> UseTime { get; set;  }
    private JobModule jobModule;
    private int _currentTime;

    public TimeBlockerModule() : base()
    {
        
        UseTime = SaveLoader.Load<ReactiveProperty<int>>();
        if(UseTime == null)
        {
            UseTime = new ReactiveProperty<int>();
            UseTime.Value = 240;
        }
        jobModule = ServiceLocator.Instance.Get<JobModule>();
    }

    protected async override void Activate()
    {
        _currentTime = UseTime.Value * 60;
        await Task.Run(StartTimer);
        ServiceLocator.Instance.Get<MenuManager>().OpenCurrent();
    }
    private async void StartTimer()
    {
        
        while(true)
        {
            if (_isBlackList)
            {
                await Task.Delay(1000);
                _currentTime -= 1;
            }
            if (_currentTime < 0)
            {
                await Task.Run(KillAllProcesses);
                break;
            }
        }
    }
    private void KillAllProcesses()
    {
        jobModule.IsRun.Value = true;
        jobModule.IsRun.OnChanged += OnTimeOver;
        
        jobModule.Act();
    }

    private void OnTimeOver(bool obj)
    {
        jobModule.IsRun.Value = true;
    }
}