using UnityEngine;
using UnityEngine.Events;

//game model 
public class RobotWorld
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    //status
    private int _robotX;
    private int _robotY;
    private Direction _robotDirection;
    private int _worldWidth;
    private int _worldHeight;
    private bool _placed;

    //getters
    public int RobotX { get { return _robotX; } }
    public int RobotY { get { return _robotY; } }
    public int WorldWidth { get { return _worldWidth; } }
    public int WorldHeight { get { return _worldHeight; } }
    public Direction RobotDirection { get { return _robotDirection; } }

    //events
    public UnityEvent RobotPlaced = new UnityEvent();
    public UnityEvent RobotMoved = new UnityEvent();
    public UnityEvent RobotRotated = new UnityEvent();

    public RobotWorld(int worldWidth, int worldHeight)
    {
        _worldWidth = worldWidth;
        _worldHeight = worldHeight;
    }

    //put the toy robot on the table in position X,Y and facing a direction
    public void Place(int x, int y, Direction direction)
    {
        _robotX = Mathf.Clamp(x, 0, _worldWidth - 1);
        _robotY = Mathf.Clamp(y, 0, _worldHeight - 1);
        _robotDirection = direction;
        _placed = true;

        RobotPlaced.Invoke();
    }

    //move the toy robot one unit forward in the direction it is currently facing
    public void Move()
    {
        switch (_robotDirection)
        {
            case Direction.EAST:
                _robotX = Mathf.Clamp(_robotX + 1, 0, _worldWidth - 1);
                break;

            case Direction.WEST:
                _robotX = Mathf.Clamp(_robotX - 1, 0, _worldWidth - 1);
                break;

            case Direction.NORTH:
                _robotY = Mathf.Clamp(_robotY + 1, 0, _worldHeight - 1);
                break;

            case Direction.SOUTH:
                _robotY = Mathf.Clamp(_robotY - 1, 0, _worldHeight - 1);
                break;
        }

        RobotMoved.Invoke();
    }

    //rotate the robot 90 degrees
    public void Left()
    {
        int currentDirection = (int)_robotDirection;
        currentDirection = (currentDirection - 1 + 4) % 4;
        _robotDirection = (Direction)currentDirection;

        RobotRotated.Invoke();
    }

    //rotate the robot 90 degrees
    public void Right()
    {
        int currentDirection = (int)_robotDirection;
        currentDirection = (currentDirection + 1) % 4;
        _robotDirection = (Direction)currentDirection;

        RobotRotated.Invoke();
    }

    //will announce the X,Y and F of the robot. 
    public string Report()
    {
        string output = "Output: " + _robotX + "," + _robotY + "," + _robotDirection;
        Debug.Log(output);
        return output;
    }

    public bool IsRobotPlaced()
    {
        return _placed;
    }
}