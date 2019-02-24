using UnityEngine;

//runs scripts step by step or individual commands
public class SimulationRunner
{
    private RobotWorld _robotWorld;
    private CommandInvoker _invoker;
    private string[] _linesFromfile;
    private int _scriptLineIndex = 0;

    public RobotWorld RobotWorld { get { return _robotWorld; } }

    public SimulationRunner()
    {
        _robotWorld = new RobotWorld(5, 5);
        _invoker = new CommandInvoker();
    }

    public void LoadScriptFile(string scriptName)
    {
        TextAsset file = Resources.Load("RobotScripts/" + scriptName) as TextAsset;
        if (file == null)
        {
            Debug.LogError("failed to load text file" + scriptName);
            return;
        }
        LoadScriptFromTextAsset(file);
    }

    public void LoadScriptFromTextAsset(TextAsset file)
    {
        _linesFromfile = file.text.Split('\n');
    }

    public string RunAllCommands()
    {
        for (int i = 0; i < _linesFromfile.Length; i++)
        {
            RunNextCommand();
        }
        return _robotWorld.Report();
    }

    public void RunNextCommand()
    {
        if (_scriptLineIndex < _linesFromfile.Length)
        {
            RunSingleCommand(_linesFromfile[_scriptLineIndex]);
            _scriptLineIndex++;
        }
    }

    public void RunSingleCommand(string line)
    {
        _invoker.Run(line, _robotWorld);
    }

    public bool IsComplete()
    {
        return _scriptLineIndex >= _linesFromfile.Length;
    }

    public string GetStatus()
    {
        return _robotWorld.Report();
    }
}