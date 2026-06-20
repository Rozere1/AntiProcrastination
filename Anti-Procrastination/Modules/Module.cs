
using Anti_Procrastination;
using Microsoft.Extensions.Hosting;



public abstract class Module : BackgroundService, IService 
{

    public abstract void Activate();
    public abstract void Init();

    protected abstract void StartServer();

}
