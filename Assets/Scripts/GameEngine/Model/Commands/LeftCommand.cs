public class LeftCommand : ICommand
{
    public void Execute(RobotWorld world, string[] args)
    {
        world.Left();
    }
}