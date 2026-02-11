namespace Anti_Procrastination;

public class ProcessDecisionPunct : IPunct
{
    public static event Action JobListChanged;

    public void CheckList()
    {
        
        JobListChanged?.Invoke();
    }
    public void Activate()
    {
        Check();
    }
    private async void Check()
    {
        while (true)
        {
            CheckList();
            Task.Delay(5000).Wait();
        }
    }
}