using Anti_Procrastination.Puncts;

namespace Anti_Procrastination.Menus
{

    public class JobMenu : Menu
    {
        public JobMenu()
        {
            AddPunct($"Переключить модуль", 0, new SwitchModulePunct());
            AddPunct("Редактировать чёрный список", 1, new OpenProgramEditPunct(Program.BlackList));
            AddPunct("Кастомизация таймера", 2, new GoToNextMenuPunct<TimerMenu>());
            AddPunct("Назад", 3, new GoBackPunct());
        }
    }
}