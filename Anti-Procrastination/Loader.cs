using Anti_Procrastination;
using Newtonsoft.Json;
using System.Reflection;
public class Saver
{
    public static void Save(object data)
    {
        var serializedObj = JsonConvert.SerializeObject(data, Formatting.Indented);
        using var sw = new StreamWriter(new FileStream("Settings.json", FileMode.OpenOrCreate));
            sw.WriteLine(serializedObj);

    }
}
public class Loader
{
    public static T Load<T>()
    {
        var fs = new FileStream("Settings.json", FileMode.OpenOrCreate);
        using (var sr = new StreamReader(fs))
        {
            try
            {
                object data = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                if (data == null && typeof(T).GetConstructor(Type.EmptyTypes) != null)
                {
                    data = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[] {});
                }
                return (T)data;
            }
            catch(Exception e)
            {
                Logger.Write(e.Message);

                return default;
            }
        }

    }
}
