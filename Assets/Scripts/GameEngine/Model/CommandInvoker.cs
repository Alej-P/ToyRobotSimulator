using System.Collections.Generic;
using UnityEngine;

//executes commands by name
public class CommandInvoker
{
    private Dictionary<string, ICommand> _commandsDictionary = new Dictionary<string, ICommand>();

    enum CommandNames
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT,
    }

    public CommandInvoker()
    {
        //create commands
        _commandsDictionary.Add(CommandNames.PLACE.ToString(), new PlaceCommand());
        _commandsDictionary.Add(CommandNames.MOVE.ToString(), new MoveCommand());
        _commandsDictionary.Add(CommandNames.LEFT.ToString(), new LeftCommand());
        _commandsDictionary.Add(CommandNames.RIGHT.ToString(), new RightCommand());
        _commandsDictionary.Add(CommandNames.REPORT.ToString(), new ReportCommand());
    }

    public void Run(string commandAndArguments, RobotWorld world)
    {
        //extract commandName and Arguments
        string commandName;
        string[] arguments;
        ParseCommand(commandAndArguments, out commandName, out arguments);

        //check command is valid
        if (!_commandsDictionary.ContainsKey(commandName))
        {
            Debug.LogError("command not recognised:" + commandName);
            return;
        }

        //execute
        ICommand commandInstance = _commandsDictionary[commandName];
        if (world.IsRobotPlaced() || commandName == CommandNames.PLACE.ToString())
            commandInstance.Execute(world, arguments);
        else
            Debug.LogError("can't run commands until robot is placed");
    }

    //split command name and arguments
    public void ParseCommand(string commandAndArguments, out string commandName, out string[] arguments)
    {
        arguments = null;
        int firstSpace = commandAndArguments.IndexOf(' ');
        if (firstSpace == -1)
        {
            commandName = commandAndArguments;
        }
        else
        {
            commandName = commandAndArguments.Substring(0, firstSpace);
            arguments = commandAndArguments.Substring(firstSpace + 1).Split(',');
        }
    }
}