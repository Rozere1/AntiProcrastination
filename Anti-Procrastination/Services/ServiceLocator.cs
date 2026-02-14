namespace Anti_Procrastination.Services;

public class ServiceLocator
{
    public static readonly ServiceLocator Instance = new ServiceLocator();
    private Dictionary<string, IService> services = new Dictionary<string, IService>();

    public void AddComponent<T>(T service) where T : IService
    {
        string name = typeof(T).Name;
        if (!services.ContainsKey(name))
        {
            services[name] = service;
        }

    }

    public void RemoveComponent<T>(T service) where T : IService
    {
        string name = typeof(T).Name;
        if (services.ContainsKey(name))
        {
            services.Remove(name);
        }

    }

    public T Get<T>() where T : IService
    {
        string name = typeof(T).Name;
        if (services.TryGetValue(name, out IService? value))
        {

            return (T)value;
        }
        else
        {
            Logger.Debug("None Service to return");
            throw new NullReferenceException();
        }
    }
}