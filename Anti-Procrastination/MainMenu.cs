

namespace Anti_Procrastination;

public class MainMenu : Menu
{
    private bool isJobModuleActivated;
    private string jActivated;
    public MainMenu()
    {
        var jB = ServiceLocator.Instance.Get<JobModule>();
        jB.IsRun.OnChanged += OnChanged;
        jActivated = isJobModuleActivated ? "X" : " ";
        _punctsCommand[0] = new GoToNextMenuPunct( MenuVariant.TimeBlockerMenu);
        _punctsCommand[1] = new GoToNextMenuPunct(MenuVariant.JobMenu);
        _punctsCommand[2] = new ExitPunct();

    }

    private void OnChanged(bool obj)
    {
        isJobModuleActivated = obj;
    }

    public override void Show()
    {
        jActivated = isJobModuleActivated ? "X" : " ";
        _puncts = new string[]
{
            $"1. Ограничение времени",
            $"[{jActivated}]2. Режим работы",
            $"3. Выйти"
};
        base.Show();

    }

}