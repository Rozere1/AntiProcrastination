using System.Diagnostics;

public class OpenProgramEditPunct : IPunct
{
    private string _arguments;
    public OpenProgramEditPunct(string arguments)
    {
        _arguments = arguments;
    }
    public void Activate()
    {
        Process.Start(new ProcessStartInfo("notepad.exe", _arguments));
    }
}