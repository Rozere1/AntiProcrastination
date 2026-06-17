using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus;

public class MainMenu : Menu
{
    private bool isJobModuleActivated;

    public MainMenu()
    {
        AddPunct("Ограничение времени", 0, new GoToNextMenuPunct<TimeBlockerMenu>());
        AddPunct("Режим работы", 1, new GoToNextMenuPunct<JobMenu>());
        AddPunct("Режим сна", 2, new GoToNextMenuPunct<SleepMenu>());
        AddPunct("Выйти", 3, new ExitPunct());
        
    }

    private void OnJobModuleChanged(bool obj)
    {
        isJobModuleActivated = obj;
    }

    public override void Show()
    {
        string jActivated = isJobModuleActivated ? "X" : "";
        ChangePunct(1, $"Режим работы [{jActivated}]");
        base.Show();

    }

}