public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public Task(int id, string name, DateTime deadline)
    {
        Id = id;
        Name = name;
        Deadline = deadline;
    }
}