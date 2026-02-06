namespace Anti_Procrastination
{
    public class DirectoryExplorerModel
    {
        public string currentPath { get; private set; }
        private string[] currentDirectories;
        public DirectoriesDictionary Directories { get; private set; }
        public bool isDisplayed { get; private set; }

        private string[] currentFiles;
        public FilesDictionary Files { get; private set; }
        private int indexForGetting;
        public Stack<string> PrevPaths { get; private set; }
        public Dictionary<string, DirectoriesDictionary> CachedDirectories { get; private set; }
        public Dictionary<string, FilesDictionary> CachedFiles { get; private set; }
        public DirectoryExplorerModel()
        {
            Directories = new DirectoriesDictionary();
            Files = new FilesDictionary();
            PrevPaths = new Stack<string>();
            CachedDirectories = new Dictionary<string, DirectoriesDictionary>();
            CachedFiles = new Dictionary<string, FilesDictionary>();
        }
        public void SetLogicalDisks()
        {
            currentPath = "";
            currentDirectories = Directory.GetLogicalDrives();

            SetDirectories(0);

        }
        public void SetCurrentDirectories(string path)
        {
            if (path == currentPath)
                return;
            PrevPaths.TryPeek(out string prevPath);
            if (prevPath != null && path == prevPath)
            {
                PrevPaths.Pop();
            }
            else
            {
                PrevPaths.Push(currentPath);
                currentPath = path;
            }

            indexForGetting = 0;

            currentDirectories = Directory.GetDirectories(path);
            currentFiles = Directory.GetFiles(path);

            if (currentDirectories == null)
            {
                currentDirectories = Array.Empty<string>();
                Console.WriteLine("Эта папка не существует");
            }
            if (currentFiles == null)
            {
                currentDirectories = Array.Empty<string>();

                Console.WriteLine("Эта папка не содержит файлов");
            }

            else
            {
                SetDictionaries(path.Length);
            }

        }
        private void SetDictionaries(int removeLength)
        {
            SetDirectories(removeLength);
            SetFiles(removeLength);
        }
        private void SetFiles(int removeLength)
        {
            Files.Clear();

            for (int i = 0; i < currentFiles.Length; i++)
            {
                if (currentFiles[i].EndsWith("exe"))
                {
                    Files.SetValue(indexForGetting, currentFiles[i].Remove(0, removeLength));
                    indexForGetting++;
                }

            }
        }
        private void SetDirectories(int removeLength)
        {
            Directories.Clear();
            for (int i = 0; i < currentDirectories.Length; i++)
            {
                Directories.SetValue(indexForGetting, currentDirectories[i].Remove(0, removeLength));
                indexForGetting++;
            }
        }
        public void SetCachedDirectories()
        {
            Directories = CachedDirectories[currentPath];
            Files = CachedFiles[currentPath];
        }
    }


}