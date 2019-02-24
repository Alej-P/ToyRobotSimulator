public interface ICommand
{
    void Execute(RobotWorld world, string[] commandArguments);
}