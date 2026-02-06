
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
            MenuManager menuManager = ServiceLocator.Instance.Get<MenuManager>();
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

            Logger.Init();
        }

    }
    public class Bootstrap
    {
        private string BlackList = @$"{Directory.GetCurrentDirectory()}\Lists\JobList.txt";
        public void Start()
        {

            ProgramListManager listManager = new ProgramListManager();
            JobModule jobModule = new JobModule(listManager.ReadAList("JobList.txt"));

            ServiceLocator.Instance.AddComponent(jobModule);
            TimerMenu timerMenu = new TimerMenu();

            TimeBlockerMenu timeBlockerMenu = new TimeBlockerMenu();

            JobMenu jobMenu = new JobMenu(jobModule, BlackList);

            MainMenu mainMenu = new MainMenu();

            MenuManager menuManager = new MenuManager(mainMenu, jobMenu, timeBlockerMenu, timerMenu);
            ServiceLocator.Instance.AddComponent(menuManager);
        }

    }


}


