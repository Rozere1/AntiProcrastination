using Anti_Procrastination.Menus;
using Anti_Procrastination.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Anti_Procrastination
{
    public class Program
    {
        public static readonly string BlackList = @$"{Directory.GetCurrentDirectory()}\Lists\BlackList.txt";
        public static event Action<object> FileChanged;
        public const string Settings = "settings.json";
        public static bool IsOpen { get; private set; }
        public static bool IsSetting { get; private set; }
        private static void Main(params string[] args)
        {
            Validate();
            var bootstrap = new Bootstrap();

            using var settingWatcher = new FileSystemWatcher(Directory.GetCurrentDirectory(), Settings);
            settingWatcher.NotifyFilter = NotifyFilters.LastWrite;
            settingWatcher.Changed += OnFileChanged;
            settingWatcher.EnableRaisingEvents = true;
            if (args.Length == 0)
            {
                IsOpen = true;
                IsSetting = true;
                bootstrap.StartMenu();
                MenuManager menuManager = ServiceLocator.Instance.Get<MenuManager>();
                menuManager.Show<MainMenu>();

                while (IsOpen)
                {
                    menuManager.OpenCurrent();
                }
            }
            else if (args[0] == "/start")
            {
                using var fileWatcher = new FileSystemWatcher(@$"{Directory.GetCurrentDirectory()}\Lists");

                fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
                fileWatcher.Changed += OnFileChanged;
                fileWatcher.EnableRaisingEvents = true;

                bootstrap.StartService();

            }

        }


        private static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            FileChanged?.Invoke(sender);
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
            var dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Anti-Procrastination");
            if (!Directory.Exists(logsDirPath))
            {
                Directory.CreateDirectory(logsDirPath);
            }
            if (!Directory.Exists(listDirPath))
            {
                Directory.CreateDirectory(listDirPath);
            }
            if (!Directory.Exists(dataPath))
            {        
                Directory.CreateDirectory(dataPath);
            }
                
            if (!File.Exists(SaveManager.Instance.path))
            {
                var file = File.Create(SaveManager.Instance.path);
                file.Close();
            }
        }
    }

    public class Bootstrap
    {
        private AntiProcrastinationService GetService(IServiceProvider provider)
        {
            var service = new AntiProcrastinationService();

            var jobModule = new JobModule();
            service.AddModule(jobModule);
            ServiceLocator.Instance.AddComponent(jobModule);

            var timeMod = new TimeBlockerModule();
            service.AddModule(timeMod);

            var sleepMod = new SleepModule();
            service.AddModule(sleepMod);
            ServiceLocator.Instance.AddComponent(sleepMod);

            var taskMod = new TaskModule();
            service.AddModule(taskMod);
            
            return service;
        }
        public void StartService()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "AntiProcrastination";
            });

            builder.Services.AddHostedService(GetService);
            IHost host = builder.Build();
            host.Run();
        }
        public void StartMenu()
        {
            var timerMenu = new TimerMenu();
            var timeBlockerMenu = new TimeBlockerMenu(Program.BlackList);
            var jobMenu = new JobMenu(Program.BlackList);
            var sleepMenu = new SleepMenu();
            var mainMenu = new MainMenu();
            var taskMenu = new TaskMenu();

            var menuManager = new MenuManager();
            menuManager.Add(timerMenu);
            menuManager.Add(timeBlockerMenu);
            menuManager.Add(jobMenu);
            menuManager.Add(sleepMenu);
            menuManager.Add(mainMenu);
            menuManager.Add(taskMenu);

            ServiceLocator.Instance.AddComponent(menuManager);
        }

    }
}