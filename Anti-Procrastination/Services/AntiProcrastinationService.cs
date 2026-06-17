using Anti_Procrastination.Services;
using Microsoft.Extensions.Hosting;
public class AntiProcrastinationService : BackgroundService
{
    public Dictionary<string, Module> modules = new Dictionary<string, Module>();
    
    public void AddModule<T>(T module) where T: Module
    {
        module.Init();
        modules.Add(typeof(T).Name ,module);
        ServiceLocator.Instance.AddComponent(module);
        

    }
    protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach(var module in modules)
        {
            module.Value.Activate();
        }
       
    }
}