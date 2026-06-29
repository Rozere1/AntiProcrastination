[Serializable]
public struct Settings
{
    public int TimeRemaining;
    public int SleepHour;
    public int UseTime;
    public bool IsJobRun;
    public bool IsSleepRun;
    public DateTime Date;
    public Settings()
    {
        UseTime = 7200;
        TimeRemaining = UseTime;
        SleepHour = 22;
        IsJobRun = false;
        IsSleepRun = false;
        Date = DateTime.Now;
    }
}