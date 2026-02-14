using Anti_Procrastination.Puncts;

namespace Anti_Procrastination.Menus;

public class TimerMenu : Menu
{
    public TimerMenu()
    {
        _puncts = new string[]
        {
                "1. Звук срабатывания",
                "2. Показывать поверх окон",
                "3. Назад"
        };
        _punctsCommand[0] = new SoundSetPunct();
        _punctsCommand[1] = new SoundSetPunct();
        _punctsCommand[2] = new GoBackPunct();
    }
}