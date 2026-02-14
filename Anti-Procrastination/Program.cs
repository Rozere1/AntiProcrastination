using Anti_Procrastination.Menus;
using Anti_Procrastination.Services;

namespace Anti_Procrastination
{
    public class Program
    {
        public static readonly string BlackList = @$"{Directory.GetCurrentDirectory()}\Lists\BlackList.txt";
        public static event Action BlackListChanged;
        public static bool IsOpen { get; private set; }
        private static void Main()
        {
            Validate();
            Logger.Init();
            var bootstrap = new Bootstrap();
            bootstrap.Start();
            IsOpen = true;
            using var fileWatcher = new FileSystemWatcher(@$"{Directory.GetCurrentDirectory()}\Lists");
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fileWatcher.Changed += OnChanged;
            fileWatcher.EnableRaisingEvents = true;
            MenuManager menuManager = ServiceLocator.Instance.Get<MenuManager>();
            menuManager.Show(MenuVariant.MainMenu);
            while (IsOpen)
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
            IsOpen = false;
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
        public void Start()
        {
            ProgramListManager listManager = new ProgramListManager();
            ServiceLocator.Instance.AddComponent(listManager);
            var jobModule = new JobModule();
            ServiceLocator.Instance.AddComponent(jobModule);
            var blockerModule = Loader.Load<TimeBlockerModule>();
            blockerModule.Activate();
            ServiceLocator.Instance.AddComponent(blockerModule);
            var timerMenu = new TimerMenu();
            var timeBlockerMenu = new TimeBlockerMenu(Program.BlackList);
            var jobMenu = new JobMenu(Program.BlackList);
            var mainMenu = new MainMenu();
            var menuManager = new MenuManager(mainMenu, jobMenu, timeBlockerMenu, timerMenu);
            ServiceLocator.Instance.AddComponent(menuManager);
        }
    }
}