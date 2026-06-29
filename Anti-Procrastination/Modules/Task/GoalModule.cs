public class GoalModule : Module
{
    private List<Goal> tasks = new List<Goal>(10);
    public GoalModule()
    {
        pipeName = "Goal";
        Init();
    }
    public override void Activate()
    {

    }

    protected override void CheckCommand(string? command)
    {
        switch (command)
        {
            case "update":
                Update();
                break;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var background = ReadCommand(stoppingToken);
        }
        catch
        {

        }

    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (server.IsConnected)
        {
            server.Disconnect();
        }
        server.Dispose();
        await base.StopAsync(cancellationToken);
    }
    private void ActivateTask()
    {

    }
    private void Update()
    {

    }
}
