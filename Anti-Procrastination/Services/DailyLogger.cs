using Newtonsoft.Json;

public class DailyLogger
{
    private DailyLog log;
    private string serviceLogs;
    public DailyLogger(int useTime, int remainingTime, int blockedApps)
    {
        log.ProductivityTime = useTime - remainingTime;
        log.BlockedApps = blockedApps;
    }
    public void Log(string message)
    {
        
    }
    public void CreateDaily()
    {
      
    }
}