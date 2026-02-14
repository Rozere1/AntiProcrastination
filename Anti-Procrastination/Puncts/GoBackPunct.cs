using Anti_Procrastination.Services;

namespace Anti_Procrastination.Puncts;

public class GoBackPunct : IPunct
{

    public void Activate()
    {
        ServiceLocator.Instance.Get<MenuManager>().GoToBack();
    }
}