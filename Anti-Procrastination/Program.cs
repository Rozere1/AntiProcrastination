using System.Diagnostics;

namespace Anti_Procrastination
{
    public class Program
    {
        private static bool isOpen = true;
        private static void Main()
        {
            Validate();
            Bootstrap bootstrap = new Bootstrap();
            bootstrap.Start();
            var dExp = new DirectoryExplorer();
            while (true)
            {
                dExp.ControlCursor();
            }
            
            MenuManager menuManager = bootstrap.manager.GetComponent<MenuManager>("MenuManager");
            while (isOpen)
            {
                menuManager.ShowMenu(MenuVariant.MainMenu);
            }



        }

        public static void Exit()
        {
            isOpen = false;
            Environment.Exit(0);

        }
        private static void Validate()
        {
            
            var listDirPath = @$"{Directory.GetCurrentDirectory()}\Lists";
            var logsDirPath = @$"{Directory.GetCurrentDirectory()}\Logs";

            if (!Directory.Exists(listDirPath))
                Directory.CreateDirectory(listDirPath);
            if(!Directory.Exists(logsDirPath))
                Directory.CreateDirectory(logsDirPath);
            Logger.Init();
        }

    }
    public class Bootstrap
    {
        public ServiceLocator manager { get; private set; } = new ServiceLocator();
        public void Start()
        {

            ProgramListManager listManager = new ProgramListManager();
            manager.AddComponent(listManager);
            
            
            var puncts_MainMenu = new string[]
            {
                "1. Ограничение времени",
                "2. Режим работы",
                "3. Выйти"
            };
            var punctsCommand_MainMenu = new Dictionary<int, IPunct>();


            MainMenu mainMenu = new MainMenu(puncts_MainMenu, punctsCommand_MainMenu);

            var puncts_TimerMenu = new string[]
            {
                "1. Звук срабатывания",
                "2. Показывать поверх окон",
                "3. Назад"
            };
            var punctsCommand_TimerMenu = new Dictionary<int, IPunct>();

            TimerMenu timerMenu = new TimerMenu(puncts_TimerMenu, punctsCommand_TimerMenu);


            var puncts_TimeBlockerMenu = new string[]
            {
                "1. Запустить модуль",
                "2. Время ограничения",
                "3. Выбор программ",
                "4. Кастомизация таймера",
                "5. Назад"
            };
            var punctsCommand_TimeBlockerMenu = new Dictionary<int, IPunct>();


            TimeBlockerMenu timeBlockerMenu = new TimeBlockerMenu(puncts_TimeBlockerMenu, punctsCommand_TimeBlockerMenu);


            var puncts_JobMenu = new string[]
            {
                "1. Запустить модуль",
                "2. Выбор программ",
                "3. Кастомизация таймера",
                "4. Назад"
            };
            var punctsCommand_JobMenu = new Dictionary<int, IPunct>();


            JobMenu jobMenu = new JobMenu(puncts_JobMenu, punctsCommand_JobMenu);

            MenuManager menuManager = new MenuManager(mainMenu, jobMenu, timeBlockerMenu, timerMenu);
            manager.AddComponent(menuManager);
            JobModule jobModule = new JobModule(listManager.ReadAList("JobList.txt"));
            


            punctsCommand_MainMenu[0] = new GoToNextMenuPunct(manager, MenuVariant.TimeBlockerMenu);
            punctsCommand_MainMenu[1] = new GoToNextMenuPunct(manager, MenuVariant.JobMenu);
            punctsCommand_MainMenu[2] = new ExitPunct();

            punctsCommand_JobMenu[0] = new SwitchJobModulePunct(jobModule);
            punctsCommand_JobMenu[2] = new GoToNextMenuPunct(manager, MenuVariant.TimerMenu);
            punctsCommand_JobMenu[3] = new GoBackPunct(manager);

            punctsCommand_TimeBlockerMenu[3] = new GoToNextMenuPunct(manager, MenuVariant.TimerMenu);
            punctsCommand_TimeBlockerMenu[4] = new GoBackPunct(manager);

            punctsCommand_TimerMenu[2] = new GoBackPunct(manager);


        }

    }


}


