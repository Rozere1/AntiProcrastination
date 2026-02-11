namespace Anti_Procrastination;

public class GoBackPunct : IPunct
{

    public void Activate()
    {
        ServiceLocator.Instance.Get<MenuManager>().GoToBack();
    }
}