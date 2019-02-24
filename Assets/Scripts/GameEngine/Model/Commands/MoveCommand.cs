public class MoveCommand : ICommand
{
    public void Execute(RobotWorld world, string[] args)
    {
        world.Move();
    }
}