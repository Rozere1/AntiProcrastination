using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class Saver
{
    public static void Save(object data)
    {
        var serializedObj = JsonConvert.SerializeObject(data, Formatting.Indented);
        using var sw = new StreamWriter(new FileStream("Settings.json", FileMode.Create));
        sw.WriteLine(serializedObj);

    }
}
public class Loader
{
    private static T GetData<T>(object data)
    {
        if (data == null && typeof(T).GetConstructor(Type.EmptyTypes) != null)
        {
            data = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
        }
        return (T)data;
    }
    public static T Load<T>()
    {
        var fs = new FileStream("Settings.json", FileMode.OpenOrCreate);
        using (var sr = new StreamReader(fs))
        {
            try
            {
                object data = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                return GetData<T>(data);
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
                
                return GetData<T>(default);
            }
        }

    }
}
