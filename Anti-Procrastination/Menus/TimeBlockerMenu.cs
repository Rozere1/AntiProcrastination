
using System.IO.Pipes;
using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus;

public class TimeBlockerMenu : Menu
{
    
    public TimeBlockerMenu(string path)
    {

        AddPunct($"Время пользования: {TimeFormatter.Format(useTime)}", 0, new TimePunct());
        AddPunct("Редактировать чёрный список", 1, new OpenProgramEditPunct(path));
        AddPunct("Кастомизация таймера", 2, new GoToNextMenuPunct<TimerMenu>());
        AddPunct("Назад", 3, new GoBackPunct());

    }
    private int useTime => SaverManager.Instance.LoadSettings().UseTime;
        
    public override void Show()
    {
        ChangePunct(0, $"Время пользования: {TimeFormatter.Format(useTime)}");
        base.Show();
    }
    
}