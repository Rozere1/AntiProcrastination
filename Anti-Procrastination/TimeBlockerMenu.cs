namespace Anti_Procrastination;

public class TimeBlockerMenu : Menu
{
    private TimeBlockerModule _module;
    private int _useTime;
    public TimeBlockerMenu(TimeBlockerModule module, string path)
    {
        _module = module;
        _useTime = _module.UseTime.Value;
        _module.UseTime.OnChanged += UseTimeChanged;
        _puncts = new string[]
        {
                $"1. Время пользования: {_useTime} минут",
                "2. Редактировать чёрный список",
                "3. Кастомизация таймера",
                "4. Назад"
        };
        _punctsCommand[0] = new TimePunct(_module);
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
        _puncts[1] = $"2. Время пользования: {_useTime} минут";
        base.Show();
    }
}