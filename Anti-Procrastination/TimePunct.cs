public class TimePunct : IPunct
{
    private TimeBlockerModule _module;
    public TimePunct(TimeBlockerModule module)
    { 
        _module = module; 
    }
    public void Activate()
    {
         Console.Clear();
        Console.Write("Введите время использования в минутах: ");
        int value = Convert.ToInt32(Console.ReadLine());
        _module.UseTime.Value = value;
        Saver.Save(_module.UseTime);
    }
}