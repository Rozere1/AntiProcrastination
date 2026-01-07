using Anti_Procrastination;

public class MenuManager
{
    private Dictionary<MenuVariant, Menu> menus = new Dictionary<MenuVariant, Menu>();
    private MenuVariant currentVar;
    private Stack<MenuVariant> prevVars = new Stack<MenuVariant>();
    public MenuManager(MainMenu mainMenu, JobMenu jobMenu, TimeBlockerMenu timeBlockerMenu, TimerMenu timerMenu)
    {
        menus.Add(MenuVariant.MainMenu, mainMenu);
        menus.Add(MenuVariant.JobMenu, jobMenu);
        menus.Add(MenuVariant.TimeBlockerMenu, timeBlockerMenu);
        menus.Add(MenuVariant.TimerMenu, timerMenu);
    }
    public void GoToBackMenu()
    {
        currentVar = prevVars.Peek();
        menus[prevVars.Pop()].Show();

    }

    public void ShowMenu(MenuVariant menuVar)
    {
        prevVars.Push(currentVar);
        currentVar = menuVar;
        menus[currentVar].Show();
    }
}

public enum MenuVariant
{
    MainMenu,
    JobMenu,
    TimeBlockerMenu,
    TimerMenu
}