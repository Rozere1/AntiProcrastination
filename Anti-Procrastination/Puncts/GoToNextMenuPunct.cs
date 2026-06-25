using Anti_Procrastination.Services;

namespace Anti_Procrastination.Puncts;

public class GoToNextMenuPunct<T> : IPunct where T : Menu
{
    public void Activate()
    {
        MenuManager.Instance.Show<T>();
    }
}