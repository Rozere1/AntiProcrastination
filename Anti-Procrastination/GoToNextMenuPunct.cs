namespace Anti_Procrastination;

public class GoToNextMenuPunct : IPunct
{
    private MenuManager _menuManager;
    private MenuVariant _menuVar;
    public GoToNextMenuPunct(ServiceLocator serviceLocator, MenuVariant menuVar)
    {
        _menuVar = menuVar;
        _menuManager = serviceLocator.GetComponent<MenuManager>("MenuManager");
    }
    public void Activate()
    {
        _menuManager.ShowMenu(_menuVar);
    }
}