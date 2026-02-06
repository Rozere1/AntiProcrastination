namespace Anti_Procrastination;

public class GoBackPunct : IPunct
{
    private MenuManager menuManager;

    public void Activate()
    {
        ServiceLocator.Instance.Get<MenuManager>().GoToBackMenu();
    }
}