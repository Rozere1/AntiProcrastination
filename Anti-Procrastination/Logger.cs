public static class Logger
{
    private static readonly string v = Directory.GetCurrentDirectory();
    private static string _path = v;

    public static void Init()
    {
        _path += @$"\Logs\{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}.txt";
        var file = File.Create(_path);
        StreamWriter writer = new StreamWriter(file);
        writer.Write("Logger Initialized");
        writer.Close();
    }
    public static void Write(string log)
    {
        using (StreamWriter sWriter = new StreamWriter(_path, true))
        {
            sWriter.Write($"\n{log}");
        }
    }
}