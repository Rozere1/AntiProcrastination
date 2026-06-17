using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus;

public class SleepMenu : Menu
{
    private int _sleepTime;

    public SleepMenu()
    {
        AddPunct($"Когда спать в {_sleepTime}", 0, new SleepPunct());
        AddPunct("Назад", 1, new GoBackPunct());

    }
    private void UseTimeChanged(int time)
    {
        _sleepTime = time;
    }
    public override void Show()
    {
        ChangePunct(0, $"Когда спать в {_sleepTime}");
        base.Show();
    }
}