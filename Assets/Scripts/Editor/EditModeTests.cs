using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EditModeTests 
{

    [Test]
    public void TestMoveNorth()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.LoadScriptFile("ScriptA");
        string result = simulation.RunAllCommands();
        Assert.AreEqual(result, "Output: 0,1,NORTH");
    }


    [Test]
    public void TestRotateLeft()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.LoadScriptFile("ScriptB");
        string result = simulation.RunAllCommands();
        Assert.AreEqual(result, "Output: 0,0,WEST");
    }

    [Test]
    public void TestMoveRightAndReturn()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.LoadScriptFile("ScriptC");
        string result = simulation.RunAllCommands();
        Assert.AreEqual(result, "Output: 3,3,NORTH");
    }


    [Test]
    public void TestDontFallWest()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.RunSingleCommand("PLACE 0,0,WEST");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        string result = simulation.GetStatus();
        Assert.AreEqual(result, "Output: 0,0,WEST");
    }

    [Test]
    public void TestDontFallEast()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.RunSingleCommand("PLACE 0,0,EAST");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        string result = simulation.GetStatus();
        Assert.AreEqual(result, "Output: 4,0,EAST");
    }


    [Test]
    public void TestSkipCommandsUntilPlacement()
    {
        SimulationRunner simulation = new SimulationRunner();
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("PLACE 0,0,EAST");
        simulation.RunSingleCommand("MOVE");
        simulation.RunSingleCommand("MOVE");
        string result = simulation.GetStatus();
        LogAssert.Expect(LogType.Error, "can't run commands until robot is placed");

        Assert.AreEqual(result, "Output: 2,0,EAST");
    }
}
