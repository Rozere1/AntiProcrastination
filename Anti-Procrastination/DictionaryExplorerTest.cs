

namespace Anti_Procrastination
{
    public class DirectoryExplorerTest
    {
        private DirectoryExplorerController _controller;
        public DirectoryExplorerTest(DirectoryExplorerController controller)
        {
            _controller = controller;
        }
        public void Test()
        {
            _controller.SelectPath(@"C:\");
            _controller.SelectPath(@"C:\Users\123\Downloads");
        }
    }
}
