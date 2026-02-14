using Anti_Procrastination.Services;
using Newtonsoft.Json;

public class TimeBlockerModule : Module, IService
{

    public ReactiveProperty<int> UseTime { get; set; }
    private JobModule jobModule;
    public int CurrentTime { get; private set; }
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
        CurrentTime = UseTime.Value;
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

        while (true)
        {
            if (_isBlackList)
            {
                await Task.Delay(1000);
                CurrentTime -= 1;
                if (CurrentTime <= 0)
                {
                    await Task.Run(KillAllProcesses);
                    IsOvered = true;
                    Saver.Save(this);
                    break;
                }
            }
            else
            {
                await Task.Delay(1000);
            }

        }

    }
    private void KillAllProcesses()
    {
        jobModule.SafeEnable();
        jobModule.Activate();
    }



}
public class TimerModule
{
    private TimeBlockerModule _module;
   
    public TimerModule(TimeBlockerModule module)
    {
        _module = module;
    }
    public void Display()
    {
        Console.Clear();
        Console.WriteLine(TimeFormatter.Format(_module.CurrentTime));
    }
    
}
