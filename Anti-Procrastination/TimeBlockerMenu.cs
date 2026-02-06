namespace Anti_Procrastination;

public class TimeBlockerMenu : Menu
{
    public TimeBlockerMenu()
    {
        _puncts = new string[]
        {
                "1. Запустить модуль",
                "2. Время ограничения",
                "3. Выбор программ",
                "4. Кастомизация таймера",
                "5. Назад"
        };
        _punctsCommand[3] = new GoToNextMenuPunct(MenuVariant.TimerMenu);
        _punctsCommand[4] = new GoBackPunct();

    }
}