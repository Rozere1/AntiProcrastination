namespace Anti_Procrastination
{
    public class DirectoryExplorerView
    {

        public void Display(DirectoriesDictionary directories,
            FilesDictionary files)
        {
            Console.Clear();
            Console.WriteLine("------");
            Console.WriteLine("------");
            Console.WriteLine("------");
            Console.WriteLine("------");

            for (int i = 0; i < directories.Count; i++)
            {
                Console.WriteLine(directories.GetValueWithIndex(i));
            }
            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine(files.GetValueWithIndex(i));
            }
        }

        public void Display(DirectoriesDictionary directories)
        {
            Console.Clear();
            for (int i = 0; i < directories.Count; i++)
            {
                Console.WriteLine(directories.GetValueWithIndex(i));
            }

        }
    }
}
