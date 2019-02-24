using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    private readonly float simulationTimestep = 1f;
    private SimulationRunner _simulation;
    public RobotView robotView;
    public TextAsset scriptFile;

    void Awake()
    {
        //init Model
        _simulation = new SimulationRunner();
        _simulation.LoadScriptFromTextAsset(scriptFile);

        //init View
        robotView.Init(_simulation.RobotWorld, simulationTimestep);

        //start running
        StartCoroutine(AdvanceSimulation());
    }

    private IEnumerator AdvanceSimulation()
    {
        YieldInstruction waitForTimestep = new WaitForSeconds(simulationTimestep);
        while (!_simulation.IsComplete())
        {
            _simulation.RunNextCommand();

            yield return waitForTimestep;
        }
    }

    void OnGUI()
    {
        if (_simulation.IsComplete()) //only allow to runtime command after the scripted commands finished
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle(GUI.skin.button);

            float buttonWidth = w / 4f;
            float buttonHeight = h * 10f / 100f;
            Rect rect = new Rect(0, buttonHeight, buttonWidth * .97f, buttonHeight);
            style.fontSize = h * 3 / 100;

            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();

            GUILayout.Space(10f);

            if (GUI.Button(rect, "MOVE", style))
                _simulation.RunSingleCommand("MOVE");

            rect.x = buttonWidth;
            if (GUI.Button(rect, "LEFT", style))
                _simulation.RunSingleCommand("LEFT");

            rect.x = buttonWidth * 2f;
            if (GUI.Button(rect, "RIGHT", style))
                _simulation.RunSingleCommand("RIGHT");

            rect.x = buttonWidth * 3f;
            if (GUI.Button(rect, "REPORT", style))
                _simulation.RunSingleCommand("REPORT");


            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
