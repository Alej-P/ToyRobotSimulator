using System;

public class PlaceCommand : ICommand
{
    public void Execute(RobotWorld world, string[] args)
    {
        int x = int.Parse(args[0]);
        int y = int.Parse(args[1]);
        RobotWorld.Direction direction = (RobotWorld.Direction)Enum.Parse(typeof(RobotWorld.Direction), args[2]);
        world.Place(x, y, direction);
    }
}