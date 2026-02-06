namespace Anti_Procrastination;

public class JobMenu : Menu
{
    private JobModule _module;
    public JobMenu(JobModule module, string path)
    {

        _puncts = new string[]
        {
                "1. Запустить модуль",
                "2. Выбор программ",
                "3. Кастомизация таймера",
                "4. Назад"
        };
        _module = module;

        _punctsCommand[0] = new SwitchJobModulePunct(_module);
        _punctsCommand[1] = new OpenProgramEditPunct(path);
        _punctsCommand[2] = new GoToNextMenuPunct(MenuVariant.TimerMenu);
        _punctsCommand[3] = new GoBackPunct();
        
    }
}