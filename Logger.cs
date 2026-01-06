public static class Logger
{

    private static string _path = @$"{Directory.GetCurrentDirectory()}\Logs";

    public static void Init()
    {
        if (Directory.Exists(_path))
            _path += $"{DateTime.Today.ToFileTime()}.txt";
        else
        {
            Directory.CreateDirectory(_path);
            _path += $"{DateTime.Today.ToFileTime()}.txt";
        }
    }
    public static void Write(string log)
    {


        var fStream = new FileStream(_path, FileMode.OpenOrCreate);
        using (StreamWriter sWriter = new StreamWriter(fStream))
        {
            sWriter.WriteLine(log);
        }
    }
}