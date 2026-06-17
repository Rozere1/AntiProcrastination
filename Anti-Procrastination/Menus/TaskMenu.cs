using Anti_Procrastination.Puncts;

public class TaskMenu : Menu
{
    public TaskMenu()
    {
        AddPunct("Создать новую задачу", 0, new CreateTaskPunct());
        AddPunct("Назад", 1, new ExitPunct());
    }
    private void DisplayTasks()
    {

    }
}