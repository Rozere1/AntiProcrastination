public class MenuManager
{
    public static MenuManager Instance;
    private Dictionary<string, Menu> menus = new Dictionary<string, Menu>();
    private string currentVar;
    private Stack<string> prevVars = new Stack<string>();
    public void Add<T>(T menu) where T : Menu
    {
        var name = typeof(T).Name;
        if (!menus.ContainsKey(name))
        {
            menus.Add(name, menu);
        }
    }
    public void GoToBack()
    {
        currentVar = prevVars.Peek();
        menus[prevVars.Pop()].Show();

    }

    public void Show<T>() where T : Menu
    {
        prevVars.Push(currentVar);
        currentVar = typeof(T).Name;
        menus[currentVar].Show();
    }
    public void OpenCurrent()
    {
        menus[currentVar].Show();
    }
}
