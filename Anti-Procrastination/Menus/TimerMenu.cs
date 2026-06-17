using Anti_Procrastination.Puncts;

namespace Anti_Procrastination.Menus;

public class TimerMenu : Menu
{
    public TimerMenu()
    {
        AddPunct("Назад", 0, new GoBackPunct());
    }
}