public struct Settings
{
    public int TimeRemaining;
    public int SleepHour;
    public int UseTime;
    public bool IsJobRun;
    public Settings()
    {
        UseTime = 7200;
        TimeRemaining = UseTime;
        SleepHour = 22;
        IsJobRun = false;
    }
}