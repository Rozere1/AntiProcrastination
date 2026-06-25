using System.Timers;
public class ScheduleTimer
{
    public DateTime Date { get; set; }
    public System.Timers.Timer timer;
    public ScheduleTimer(DateTime date)
    {
        Date = date;
        timer = new System.Timers.Timer();
    }
    
    public void Start()
    {
        var substactedTime = Date.Subtract(DateTime.Now).TotalMilliseconds;
        timer.AutoReset = false;
        if (substactedTime <= 0)
        {
            timer.Interval = 1;
            timer.Start();
        }
        else
        {
            timer.Interval = substactedTime;

            timer.Start();
        }
    }

}