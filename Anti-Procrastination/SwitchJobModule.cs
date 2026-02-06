using System.Diagnostics;

namespace Anti_Procrastination;

public class SwitchJobModulePunct : IPunct
{
    private JobModule module;

    public SwitchJobModulePunct(JobModule jobModule)
    {
        module = jobModule;
    }
    public void Activate()
    {
        module.Switch();
    }
}
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