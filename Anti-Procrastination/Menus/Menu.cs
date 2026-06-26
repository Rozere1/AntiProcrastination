public abstract class Menu
{
    private List<string> _puncts = new List<string>();
    private Dictionary<int, IPunct> _punctsCommand = new Dictionary<int, IPunct>();


    public virtual void Show()
    {
        Console.Clear();
        foreach (var punct in _puncts)
        {
            Console.WriteLine(punct);
        }
        GetInput();
    }

    protected void GetInput()
    {

        try
        {
            var key = Convert.ToInt32(Console.ReadLine());
            _punctsCommand[key - 1].Activate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}\n{ex.StackTrace}");
            Console.WriteLine("Нажмите, чтобы продолжить");
            Console.ReadKey();
        }
    }
    public void AddPunct(string punct, int id, IPunct punctCommand)
    {
        if (id < 0)
        {

            return;
        }
        _puncts.Add($"{id + 1}. {punct}");
        _punctsCommand.Add(id, punctCommand);
    }
    protected void ChangePunct(int id, string changed)
    {
        if (_puncts[id] != null)
        {
            _puncts[id] = $"{id + 1}. {changed}";
        }
        else
        {
            _puncts[id] = "This punct is null";
        }
    }
}