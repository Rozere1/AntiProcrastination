namespace Anti_Procrastination;

public class GoToNextMenuPunct : IPunct
{
    private MenuVariant _menuVar;
    public GoToNextMenuPunct(MenuVariant menuVar)
    {
        _menuVar = menuVar;
    }
    public void Activate()
    {
        ServiceLocator.Instance.Get<MenuManager>().Show(_menuVar);
    }
}