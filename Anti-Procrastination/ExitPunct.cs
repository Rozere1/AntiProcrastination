namespace Anti_Procrastination;

public class ExitPunct : IPunct
{
    public void Activate()
    {
        Program.Exit();
    }
}