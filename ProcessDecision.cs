namespace Anti_Procrastination;

public class ProcessDecisionPunct : IPunct
{
    public static event Action JobListChanged;

    public void WriteToList(List<string> processes)
    {
        var path = @$"{Directory.GetCurrentDirectory()}\JobList.txt";
        using (StreamWriter sWriter = new StreamWriter(path))
        {
            foreach (var process in processes)
            {
                sWriter.WriteLine(process);
            }
        }
    }
    public void Activate()
    {

    }
}