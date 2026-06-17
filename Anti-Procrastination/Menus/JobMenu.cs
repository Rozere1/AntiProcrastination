using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus
{

    public class JobMenu : Menu
    {
        public JobMenu(string path)
        {
            AddPunct($"Переключить модуль", 0, new SwitchModulePunct());
            AddPunct("Редактировать чёрный список", 1, new OpenProgramEditPunct(path));
            AddPunct("Кастомизация таймера", 2, new GoToNextMenuPunct<TimerMenu>());
            AddPunct("Назад", 3, new GoBackPunct());
        }
    }
}