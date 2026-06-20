using Anti_Procrastination;
using Newtonsoft.Json;
public class SaverManager
{
    public readonly static SaverManager Instance = new SaverManager();
    public Settings settings;
    public readonly string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Anti-Procrastination\\settings.json";
    public SaverManager()
    {
        var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Anti-Procrastination");
        if (!Directory.Exists(dataPath))
        {        
            Directory.CreateDirectory(dataPath);
        }
        if (!File.Exists(path))
        {
            var file = File.Create(path);
            file.Close();
        }
        SaveSettings(SettingType.Default, 0);
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
        catch 
        {
            return new Settings();
        }

    }
}


public enum SettingType
{
    TimeRemaining,
    UseTime,
    SleepHour,
    IsJobRun,
    Default
}