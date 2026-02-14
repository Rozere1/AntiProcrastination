using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus;

public class TimeBlockerMenu : Menu
{
    private int _useTime;

    public TimeBlockerMenu(string path)
    {
        var module = ServiceLocator.Instance.Get<TimeBlockerModule>();
        _useTime = module.UseTime.Value;
        module.UseTime.OnChanged += UseTimeChanged;

        _puncts = new string[]
        {
                $"1. Время пользования: {TimeFormatter.Format(_useTime)}",
                "2. Редактировать чёрный список",
                "3. Кастомизация таймера",
                "4. Назад"
        };

        _punctsCommand[0] = new TimePunct(module);
        _punctsCommand[1] = new OpenProgramEditPunct(path);
        _punctsCommand[2] = new GoToNextMenuPunct(MenuVariant.TimerMenu);
        _punctsCommand[3] = new GoBackPunct();
    }
    private void UseTimeChanged(int time)
    {
        _useTime = time;
    }
    public override void Show()
    {
        _puncts[0] = $"1. Время пользования: {TimeFormatter.Format(_useTime)}";
        base.Show();
    }
}