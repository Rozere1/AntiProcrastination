using Anti_Procrastination;
using Newtonsoft.Json;
public class SaverInstance
{
    public Settings settings;
    public readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Anti-Procrastination\\settings.json";
    public SaverInstance()
    {
        SaveSettings(SettingType.Default, null);
    }
    public void SaveSettings(SettingType type, object data)
    {
        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.TypeNameHandling = TypeNameHandling.All;
        switch(type)
        {
            case SettingType.TimeRemaining:
                settings.TimeRemaining = (int)data;
                break;
            case SettingType.UseTime:
                settings.UseTime = (int)data;
                break;
            case SettingType.SleepHour:
                settings.SleepHour = (int)data;
                break;
            case SettingType.IsJobRun:
                settings.IsJobRun = (bool)data;
                break;
            case SettingType.Default:
                settings = LoadSettings();
                break;
        }
        var serializedObj = JsonConvert.SerializeObject(settings, jsonSettings);
        using var sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate));
        sw.WriteLine(serializedObj);
        sw.Close();
    }
    
     public  void SaveTask(Task taskData, string v)
    {
        throw new NotImplementedException();
    }
    public Settings LoadSettings()
    {
        using var sr = new StreamReader(path);
        try
        {
            Settings data = JsonConvert.DeserializeObject<Settings>(sr.ReadToEnd());
            sr.Close();
            return data;
        }
        catch (Exception ex)
        {
            return new Settings();
        }

    }
}
public static class SaveManager
{
    public readonly static SaverInstance Instance = new SaverInstance();
    
    
}


public enum SettingType
{
    TimeRemaining,
    UseTime,
    SleepHour,
    IsJobRun,
    Default
}