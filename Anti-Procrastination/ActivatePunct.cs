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