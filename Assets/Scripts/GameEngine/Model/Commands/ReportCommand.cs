public class ReportCommand : ICommand
{
    public void Execute(RobotWorld world, string[] args)
    {
        world.Report();
    }
}