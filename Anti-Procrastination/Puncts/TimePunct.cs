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
        if (_module.CurrentTime >= _module.UseTime.Value * 0.15)
        {
            Console.WriteLine("Вы не можете поменять время из-за малого оставшегося времени");
            Console.ReadLine();
            return;
        }
        Console.Write("Введите время использования в секундах: ");
        try
        {
            int value = Convert.ToInt32(Console.ReadLine());
            if (value > 25200)
            {
                Console.WriteLine("Слишком большое значение введите снова");
                Console.ReadKey();
                Activate();
                return;
            }
            if( value < 600)
            {
                Console.WriteLine("Слишком маленькое значение");
                Console.ReadKey();
                Activate(); 
                return;
            }
                _module.UseTime.Value = value;
        }

        catch(Exception ex)
        {
            Logger.Debug($"{ex.Message} {ex.StackTrace}");
        }
        
    }
}