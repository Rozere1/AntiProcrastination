
using Anti_Procrastination;
using Newtonsoft.Json;

public class TimeBlockerModule : Module, IService
{
    
    public ReactiveProperty<int> UseTime { get; set;  }
    private JobModule jobModule;
    private int _currentTime;
    [JsonProperty("IsOvered")]
    public bool IsOvered { get; private set; }
    public TimeBlockerModule() : base()
    {
       
        UseTime = new ReactiveProperty<int>();
        UseTime.Value = 10800;

        
    }

    public async override void Activate()
    {
        jobModule = ServiceLocator.Instance.Get<JobModule>();
        _currentTime = UseTime.Value;
        if (IsOvered)
        {
            KillAllProcesses();
            return;
        }
        await Task.Run(HookProcesses);
        await Task.Run(StartTimer);
       
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
            if (_currentTime <= 0)
            {
                await Task.Run(KillAllProcesses);
                IsOvered = true;
                Saver.Save(this);
                break;
            }
        }

    }
    private void KillAllProcesses()
    {
        jobModule.IsRun.Value = true;
        jobModule.SafeEnable();
        jobModule.Activate();
    }



}