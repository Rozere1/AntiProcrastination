public class NotDevelopedPunct : IPunct
{
    public void Activate()
    {
        Console.Clear();
        Console.WriteLine("Функционал не доделан");
        Console.ReadLine();
    }
}