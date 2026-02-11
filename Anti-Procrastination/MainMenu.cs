

namespace Anti_Procrastination;

public class MainMenu : Menu
{
    private bool isJobModuleActivated;

    public MainMenu()
    {
        var jobModule = ServiceLocator.Instance.Get<JobModule>();
        jobModule.IsRun.OnChanged += OnJobModuleChanged;
        
        _puncts = new string[]
        {
            $"1. Ограничение времени",
            $"[ ]2. Режим работы",
            "3. Выйти"
        };
        
        _punctsCommand[0] = new GoToNextMenuPunct( MenuVariant.TimeBlockerMenu);
        _punctsCommand[1] = new GoToNextMenuPunct(MenuVariant.JobMenu);
        _punctsCommand[2] = new ExitPunct();

    }

    private void OnJobModuleChanged(bool obj)
    {
        isJobModuleActivated = obj;
    }

    public override void Show()
    {
        string jActivated = isJobModuleActivated ? "X" : " ";
        _puncts[1] = $"[{jActivated}]2. Режим работы";
        base.Show();

    }

}