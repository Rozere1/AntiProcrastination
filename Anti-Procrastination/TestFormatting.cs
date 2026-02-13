public class TestFormating
{
    public void Test(int ws)
    {
        var w = TimeFormatter.Format(ws);
        Console.WriteLine(w.ToString());
        Console.ReadLine();
    }
}