using System.Timers;
public class ScheduleTimer
{
    public DateTime Date { get; set; }
    public System.Timers.ElapsedEventHandler OnTimeOver;
    private System.Timers.Timer timer;
    public ScheduleTimer(DateTime date)
    {
        Date = date;
        timer = new System.Timers.Timer();
    }
    
    public async void Start()
    {
        var substactedTime = Date.Subtract(DateTime.Now).TotalMilliseconds;
        timer.Elapsed += OnTimeOver;
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