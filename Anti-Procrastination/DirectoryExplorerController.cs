
namespace Anti_Procrastination
{
    public class DirectoryExplorerController
    {
        private DirectoryExplorerView _view;
        private DirectoryExplorerModel _model;

        public DirectoryExplorerController(DirectoryExplorerView view, DirectoryExplorerModel model)
        {
            _view = view;
            _model = model;
        }

        public void SelectPath(string path)
        {
            if (path == "" || _model.currentPath == null)
            {
                _model.SetLogicalDisks();
                _view.Display(_model.Directories);
                Control();
                return;
            }
            _model.SetCurrentDirectories(path);
            _view.Display(_model.Directories, _model.Files);
            Control();
        }

        public void Control()
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                GoBack();
            }
            else
            {
                GetInput();
            }
        }
        private void GetInput()
        {

            string path = "";
            try
            {
                Console.Write("Введите индекс: ");
                int index = Int32.Parse(Console.ReadLine());
                if (index >= _model.Directories.Count)
                {
                    path = $@"{_model.currentPath}\{_model.Files.GetValue(index)}";
                    return;
                }
                path = $@"{_model.currentPath}{_model.Directories.GetValue(index)}\";
                SelectPath(path);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                Logger.Write(e.Message);
                Console.WriteLine("Попытайтесь ещё раз");
                Console.ReadKey();
                SelectPath(_model.currentPath);
            }
            return;
        }
        public void GoBack()
        {
            var path = _model.PrevPaths.Peek();
            SelectPath(path);
        }
    }
}
