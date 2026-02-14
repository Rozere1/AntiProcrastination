public static class Logger
{
    private static string _path = @$"{Directory.GetCurrentDirectory()}\Logs";
    private static string _pathToDailyLog;
    private static string _pathToDebug;
    public static void Init()
    {
        _pathToDailyLog = @$"{_path}\{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}.txt";
        _pathToDebug = @$"{_path}\Debug.txt";
        var file0 = File.Create(_pathToDailyLog);
        var file1 = File.Create(_pathToDebug);
        file0.Close();
        file1.Close();
        Debug("Logger Inited");
    }
    public static void Debug(string log)
    {
        using (StreamWriter sWriter = new StreamWriter(_pathToDebug, true))
        {
            sWriter.WriteLine($"{log}");
        }
    }
    public static void Log()
    {

    }
}