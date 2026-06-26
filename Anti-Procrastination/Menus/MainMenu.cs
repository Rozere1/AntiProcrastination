using Anti_Procrastination.Puncts;

namespace Anti_Procrastination.Menus;

public class MainMenu : Menu
{
    private bool IsJobModuleActivated() => SaverManager.Instance.LoadSettings().IsJobRun;

    public MainMenu()
    {
        AddPunct("Ограничение времени", 0, new GoToNextMenuPunct<TimeBlockerMenu>());
        AddPunct("Режим работы", 1, new GoToNextMenuPunct<JobMenu>());
        AddPunct("Режим сна", 2, new GoToNextMenuPunct<SleepMenu>());
        AddPunct("Выйти", 3, new ExitPunct());

    }


    public override void Show()
    {
        string jActivated = IsJobModuleActivated() ? "X" : "";
        ChangePunct(1, $"Режим работы [{jActivated}]");
        base.Show();
    }

}