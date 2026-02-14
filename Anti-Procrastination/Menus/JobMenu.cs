using Anti_Procrastination.Puncts;
using Anti_Procrastination.Services;

namespace Anti_Procrastination.Menus
{

    public class JobMenu : Menu
    {
        public JobMenu(string path)
        {

            _puncts = new string[]
            {
                "1. Переключить модуль",
                "2. Редактировать чёрный список",
                "3. Кастомизация таймера",
                "4. Назад"
            };
            

            _punctsCommand[0] = new SwitchModulePunct(ServiceLocator.Instance.Get<JobModule>());
            _punctsCommand[1] = new OpenProgramEditPunct(path);
            _punctsCommand[2] = new GoToNextMenuPunct(MenuVariant.TimerMenu);
            _punctsCommand[3] = new GoBackPunct();

        }
    }
}