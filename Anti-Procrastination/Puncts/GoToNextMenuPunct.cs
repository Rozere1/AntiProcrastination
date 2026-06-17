using Anti_Procrastination.Services;

namespace Anti_Procrastination.Puncts;

public class GoToNextMenuPunct<T> : IPunct where T : Menu
{
    public void Activate()
    {
        ServiceLocator.Instance.Get<MenuManager>().Show<T>();
    }
}