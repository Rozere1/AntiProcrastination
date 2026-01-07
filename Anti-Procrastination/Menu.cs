

using Anti_Procrastination;

public abstract class Menu
{
    protected string[] _puncts;
    protected Dictionary<int, IPunct> _punctsCommand;

    public Menu(string[] puncts, Dictionary<int, IPunct> punctsCommand)
    {
        _puncts = puncts;
        _punctsCommand = punctsCommand;
    }
    public void Show()
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
        catch (Exception e)
        {
            Logger.Write(e.Message);
            Console.Write($"Произошла ошибка: {e.Message}\n");
            Program.Exit();
        }
    }
}