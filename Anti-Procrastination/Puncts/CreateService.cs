using System.Diagnostics;
public class CreateService : IPunct
{
    public void Activate()
    {

        Console.WriteLine("Чтобы программа корректно работала необходимо создать службу(Y/N)");
        Check(Console.ReadLine());
        Console.WriteLine("Нажмите, чтобы продолжить");
        Console.ReadKey();
    }
    private void Check(string? ans)
    {
        if (ans.ToUpper() == "N")
        {
            Console.WriteLine("Вы отклонили запрос, служба не будет создана");
        }
        else if (ans.ToUpper() == "Y")
        {

            try
            {
                var shut = new ProcessStartInfo();
                shut.FileName = "cmd";
                shut.Arguments = $"/k sc create Anti-Procrastination binpath= \"\"{Directory.GetCurrentDirectory()}\\Anti-Procrastination.exe\" -start\" start=auto";
                shut.UseShellExecute = true;
                shut.Verb = "runas";
                Process.Start(shut);
                Console.WriteLine("Сервис создан");
            }
            catch
            {
                Console.WriteLine("Вы отклонили запрос, служба не будет создана");
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод, попробуйте снова: ");
            Check(Console.ReadLine());
        }
    }
}