
namespace Anti_Procrastination
{
    public class DirectoryExplorer
    {
        
        public string path = @"C:\";
        private int selectedDirectory;
        private string[] currentDirectories;
        private Dictionary<int, string> selectedDirectories = new Dictionary<int, string>();
        private bool isDisplayed = false;
        int height = Console.WindowHeight;
        int width = Console.WindowWidth;
        public void ControlCursor()
        {

            if(!isDisplayed)
                SetCurrentDirectories();
            Display();
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            if (consoleKey.Key == ConsoleKey.UpArrow)
            {
                selectedDirectory--;

            }
            else if (consoleKey.Key == ConsoleKey.DownArrow)
            {
                selectedDirectory++;
            }
            
        }
        public void DrawCursor()
        {
            Console.Write(selectedDirectories[selectedDirectory].StartsWith('>'));
        }
        private void SetCurrentDirectories()
        {
            currentDirectories = Directory.GetDirectories(path);
            for (int i = 0; i < currentDirectories.Length; i++)
            {
                if (i < height - 5)
                    selectedDirectories.Add(i, currentDirectories[i] + "\n");
                else
                    selectedDirectories.Add(i, "\t\t" + currentDirectories[i] + "\n");
            }
        }
        public void Display()
        {
            
            Console.Clear();

            for (int i = 0; i < selectedDirectories.Count; i++)
            {
                    Console.Write(selectedDirectories[i]);
            }


            isDisplayed = true;

        }
    }
}
