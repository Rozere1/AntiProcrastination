namespace Anti_Procrastination.Puncts;

public class ExitPunct : IPunct
{
    public void Activate()
    {
        Program.Exit();
    }
}