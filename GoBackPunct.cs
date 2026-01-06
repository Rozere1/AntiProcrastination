namespace Anti_Procrastination;

public class GoBackPunct : IPunct
{
    private ServiceLocator _serviceLocator;
    private MenuManager menuManager;
    public GoBackPunct(ServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }
    public void Activate()
    {
        menuManager = _serviceLocator.GetComponent<MenuManager>("MenuManager");
        menuManager.GoToBackMenu();
    }
}