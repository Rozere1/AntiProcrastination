using Anti_Procrastination.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ServiceProcess;
namespace Anti_Procrastination
{
    public class Program
    {
        public static readonly string BlackList = @$"{Directory.GetCurrentDirectory()}\Lists\BlackList.txt";
        public static event Action<object> FileChanged;
        public static bool IsOpen { get; private set; }
        public static bool IsSetting { get; private set; }
        private static void Main(params string[] args)
        {
            Validate();
            var bootstrap = new Bootstrap();
            if (args.Length == 0)
            {
                var services = ServiceController.GetServices();
                if (!services.Any(s => s.ServiceName.Equals("Anti-Procrastination", StringComparison.OrdinalIgnoreCase)))
                {
                    CreateService punct = new CreateService();
                    punct.Activate();
                }
                IsOpen = true;
                IsSetting = true;
                bootstrap.StartMenu();
                MenuManager.Instance.Show<MainMenu>();

                while (IsOpen)
                {
                    MenuManager.Instance.OpenCurrent();
                }
            }
            else
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
        private IHostApplicationLifetime lifetime;
        public void StartService()
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "Anti-Procrastination";

            });
            builder.Services.AddSingleton<JobModule>();
            builder.Services.AddHostedService(sp => sp.GetRequiredService<JobModule>());

            builder.Services.AddHostedService<TimeBlockerModule>();
            builder.Services.AddHostedService<SleepModule>();
            builder.Services.AddHostedService<GoalModule>();

            IHost host = builder.Build();
            lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            host.Run();
        }

        public void StartMenu()
        {
            var timerMenu = new TimerMenu();
            var timeBlockerMenu = new TimeBlockerMenu();
            var jobMenu = new JobMenu();
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
            MenuManager.Instance = menuManager;
        }
    }
}