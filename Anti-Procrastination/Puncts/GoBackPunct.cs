using Anti_Procrastination.Services;

namespace Anti_Procrastination.Puncts;

public class GoBackPunct : IPunct
{

    public void Activate()
    {
        MenuManager.Instance.GoToBack();
    }
}