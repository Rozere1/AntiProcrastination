namespace Anti_Procrastination;

public class ProcessDecisionPunct : IPunct
{
    public static event Action JobListChanged;

    public void WriteToList(List<string> processes)
    {
        var path = @$"{Directory.GetCurrentDirectory()}\JobList.txt";

        JobListChanged?.Invoke();
    }
    public void Activate()
    {

    }
}