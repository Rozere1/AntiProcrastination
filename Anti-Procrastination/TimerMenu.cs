namespace Anti_Procrastination;

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
        _punctsCommand[2] = new GoBackPunct();
    }
}