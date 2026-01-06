namespace Anti_Procrastination
{
    public class Program
    {
        private static bool isOpen = true;
        private static void Main()
        {
            Bootstrap bootstrap = new Bootstrap();
            bootstrap.Start();
            MenuManager menuManager = bootstrap.manager.GetComponent<MenuManager>("MenuManager");
            while (isOpen)
            {
                menuManager.ShowMenu(MenuVariant.MainMenu);
            }



        }

        public static void Exit()
        {
            Process process = Process.GetCurrentProcess();
            process.Kill();

        }


    }
    public class Bootstrap
    {
        public ServiceLocator manager;

        public void Start()
        {
            Logger.Init();
            manager = new ServiceLocator();
            var puncts_MainMenu = new string[]
            {
                "1. Ограничение времени",
                "2. Режим работы",
                "3. Выйти"
            };
            var punctsCommand_MainMenu = new Dictionary<int, IPunct>();


            MainMenu mainMenu = new MainMenu(puncts_MainMenu, punctsCommand_MainMenu);

            manager.AddComponent(mainMenu);
            var puncts_TimerMenu = new string[]
            {
                "1. Звук срабатывания",
                "2. Показывать поверх окон",
                "3. Назад"
            };
            var punctsCommand_TimerMenu = new Dictionary<int, IPunct>();

            TimerMenu timerMenu = new TimerMenu(puncts_TimerMenu, punctsCommand_TimerMenu);

            manager.AddComponent(timerMenu);

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

            manager.AddComponent(timeBlockerMenu);

            var puncts_JobMenu = new string[]
            {
                "1. Запустить модуль",
                "2. Выбор программ",
                "3. Кастомизация таймера",
                "4. Назад"
            };
            var punctsCommand_JobMenu = new Dictionary<int, IPunct>();


            JobMenu jobMenu = new JobMenu(puncts_JobMenu, punctsCommand_JobMenu);
            manager.AddComponent(jobMenu);

            MenuManager menuManager = new MenuManager(mainMenu, jobMenu, timeBlockerMenu, timerMenu);
            manager.AddComponent(menuManager);
            JobModule jobModule = new JobModule(ReadJobProgramList());



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

        private List<string> ReadJobProgramList()
        {
            var jobList = new List<string>();
            var path = @$"{Directory.GetCurrentDirectory()}\JobList.txt";
            var fStream = new FileStream(path, FileMode.OpenOrCreate);
            using (StreamReader streamReader = new StreamReader(fStream))
            {
                string rawJobList = streamReader.ReadToEnd();
                jobList = rawJobList.Split("\n").ToList();
                foreach (var job in jobList)
                {
                    Logger.Write(job);
                    Logger.Write(jobList.Count.ToString());
                    Console.WriteLine(job);
                }
            }

            return jobList;
        }
    }


}


