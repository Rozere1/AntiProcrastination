using System.Diagnostics;

public class SwitchModulePunct : IPunct
{
    private ISwitch _module;

    public SwitchModulePunct(ISwitch module)
    {
        _module = module;
    }
    public void Activate()
    {
        _module.Switch();
    }
}
