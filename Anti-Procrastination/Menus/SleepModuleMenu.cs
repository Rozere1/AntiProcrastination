using Anti_Procrastination.Puncts;

namespace Anti_Procrastination.Menus;

public class SleepMenu : Menu
{
    private int SleepTime() => SaverManager.Instance.settings.SleepHour;

    public SleepMenu()
    {
        AddPunct($"Когда спать в {SleepTime()}", 0, new SleepPunct());
        AddPunct("Назад", 1, new GoBackPunct());

    }
    public override void Show()
    {
        ChangePunct(0, $"Когда спать в {SleepTime()}");
        base.Show();
    }
}