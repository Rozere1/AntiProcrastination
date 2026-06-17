using Newtonsoft.Json;
public class CreateTaskPunct : IPunct
{
    private string name;
    private DateTime deadline;
    public void Activate()
    {
        ChangeName();
        ChangeDeadline();
        Check();
        var taskData = new Task(1,"AS", DateTime.Now);
        SaveManager.Instance.SaveTask(taskData, $"Tasks\\{name}.json");
        
    }
    private void ChangeName()
    {
        Console.WriteLine("Введите название задачи:");
        var name = Console.ReadLine();
        Console.Clear();
    }
    private void ChangeDeadline()
    {
        Console.WriteLine("Например: 2029-08-19 13:20");
        Console.WriteLine("Введите дедлайн:");
        DateTime.TryParse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture, out DateTime deadline);
        Console.Clear();
    }
    private void Check()
    {
        Console.WriteLine($"1.{name}");
        Console.WriteLine($"2.{deadline.ToString()}");
        Console.WriteLine("Изменить(оставьте пустым, чтобы пропустить):");
        try
        {
            int id = Convert.ToInt32(Console.ReadLine());
            switch (id)
            {
                case 1:
                    ChangeName();
                    Check();
                    break;
                case 2:
                    ChangeDeadline();
                    Check();
                    break;
                default:
                    break;

            }
        }
        catch
        {
            return;
        }
    }
}