public class RightCommand : ICommand
{
    public void Execute(RobotWorld world, string[] args)
    {
        world.Right();
    }
}