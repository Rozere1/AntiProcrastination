
namespace Anti_Procrastination
{
    public class Program
    {
        public static event Action BlackListChanged;
        public static bool isOpen { get; private set; }
        private static void Main()
        {
            Validate();
            Logger.Init();
            Bootstrap bootstrap = new Bootstrap();
            bootstrap.Start();
            isOpen = true;
            using var fileWatcher = new FileSystemWatcher(@$"{Directory.GetCurrentDirectory()}\Lists");
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fileWatcher.Changed += OnChanged;
            fileWatcher.EnableRaisingEvents = true;

            MenuManager menuManager = ServiceLocator.Instance.Get<MenuManager>();
            menuManager.Show(MenuVariant.MainMenu);
            
            while (isOpen)
            {
                menuManager.OpenCurrent();
                
            }
            
            


        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            BlackListChanged.Invoke();
        }

        public static void Exit()
        {
            isOpen = false;
            Environment.Exit(0);

        }
        public static void Validate()
        {

            var listDirPath = @$"{Directory.GetCurrentDirectory()}\Lists";
            var logsDirPath = @$"{Directory.GetCurrentDirectory()}\Logs";
            if (!Directory.Exists(logsDirPath))
            {
                Directory.CreateDirectory(logsDirPath);
                
            }
            if (!Directory.Exists(listDirPath))
            {
                Directory.CreateDirectory(listDirPath);
            }

            
        }

    }
    public class Bootstrap
    {
        private string BlackList = @$"{Directory.GetCurrentDirectory()}\Lists\BlackList.txt";
        public void Start()
        {
            
            ProgramListManager listManager = new ProgramListManager();
            ServiceLocator.Instance.AddComponent(listManager);
            var jobModule = new JobModule();
            
            ServiceLocator.Instance.AddComponent(jobModule);

            var blockerModule = new TimeBlockerModule();
            ServiceLocator.Instance.AddComponent(blockerModule);
            TimerMenu timerMenu = new TimerMenu();

            TimeBlockerMenu timeBlockerMenu = new TimeBlockerMenu(blockerModule ,BlackList);

            JobMenu jobMenu = new JobMenu(jobModule, BlackList);

            MainMenu mainMenu = new MainMenu();

            MenuManager menuManager = new MenuManager(mainMenu, jobMenu, timeBlockerMenu, timerMenu);
            ServiceLocator.Instance.AddComponent(menuManager);
        }

    }


}


