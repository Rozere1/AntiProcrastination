namespace Anti_Procrastination;

public class ServiceLocator
{
    private Dictionary<string, object> components = new Dictionary<string, object>();

    public void AddComponent<T>(T component)
    {
        if (components.ContainsValue(component))
            return;
        components.Add(component.ToString(), component);
    }

    public void RemoveComponent<T>(T component)
    {
        if (components.ContainsValue(component))
            components.Remove(component.ToString());
    }

    public T GetComponent<T>(string component)
    {
        if (components.ContainsKey(component))
            return (T)components[component];
        throw new NullReferenceException();
    }
}