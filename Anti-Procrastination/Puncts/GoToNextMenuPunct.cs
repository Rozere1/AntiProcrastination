using Anti_Procrastination.Services;

namespace Anti_Procrastination.Puncts;

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