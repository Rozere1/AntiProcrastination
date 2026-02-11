using Anti_Procrastination;
using Newtonsoft.Json;
public class Saver
{
    public static void Save(object data)
    {
        var serializedObj = JsonConvert.SerializeObject(data, Formatting.Indented);
        using (StreamWriter sw = new StreamWriter(new FileStream("Settings.json", FileMode.OpenOrCreate)))
        {
            sw.WriteLine(serializedObj);
        }
    }
}
public class SaveLoader
{
    public static T? Load<T>()
    {
        FileStream fs = new FileStream("Settings.json", FileMode.OpenOrCreate);
        using (StreamReader sr = new StreamReader(fs))
        {
            try
            {
                var data = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());

                return data;
            }
            catch(Exception e)
            {
                Logger.Write(e.Message);

                return default;
            }
        }

    }
}
