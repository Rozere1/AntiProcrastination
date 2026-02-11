using System.Diagnostics;

namespace Anti_Procrastination;

public class ServiceLocator
{
    public static ServiceLocator Instance = new ServiceLocator();
    private Dictionary<string, object> services = new Dictionary<string, object>();

    public void AddComponent<T>(T service)
    {
        string name = typeof(T).Name;
        if(!services.ContainsKey(name))
        {
            services[name] = service;
        }

    }

    public void RemoveComponent<T>(T service)
    {
        string name = typeof(T).Name;
        if (services.ContainsKey(name))
        {
            services.Remove(name);
        }

    }

    public T Get<T>()
    {
        string name = typeof(T).Name;
        if (services.ContainsKey(name))
        {

            return (T)services[name];
        }
        else
        {
            Logger.Write("None Service to return");
            throw new NullReferenceException();
        }
    }
}