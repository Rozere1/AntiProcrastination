public struct Goal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public Goal(int id, string name, DateTime deadline)
    {
        Id = id;
        Name = name;
        Deadline = deadline;
    }
}