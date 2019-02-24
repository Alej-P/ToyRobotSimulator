using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobotView : MonoBehaviour
{
    private RobotWorld _robotWorld;
    private float _simulationTimestep;

    public void Init(RobotWorld robotWorld, float simulationTimestep)
    {
        _robotWorld = robotWorld;
        _simulationTimestep = simulationTimestep;
        _robotWorld.RobotPlaced.AddListener(HandleRobotPlaced);
        _robotWorld.RobotMoved.AddListener(HandleRobotMoved);
        _robotWorld.RobotRotated.AddListener(HandleRobotRotated);
    }

    private void HandleRobotPlaced()
    {
        transform.position = ConvertModelCoordsToWorldUnits();
        transform.eulerAngles = ConvertModelDirectionToEulerAngles();
    }

    private void HandleRobotMoved()
    {
        transform.DOMove(ConvertModelCoordsToWorldUnits(), _simulationTimestep).SetEase(Ease.InOutSine);
        //transform.DOLocalRotate(new Vector3(10f, transform.eulerAngles.y, 0), _simulationTimestep / 4f).SetLoops(2, LoopType.Yoyo);
    }


    private void HandleRobotRotated()
    {
        transform.DOLocalRotate(ConvertModelDirectionToEulerAngles(),_simulationTimestep);
    }

    private Vector3 ConvertModelCoordsToWorldUnits()
    {
        Vector3 origin = new Vector3(-_robotWorld.WorldWidth/2, 0, -_robotWorld.WorldHeight/2);
        return origin  + new Vector3(_robotWorld.RobotX, 0, _robotWorld.RobotY);
    }

    private Vector3 ConvertModelDirectionToEulerAngles()
    {
        float angle = ((int)_robotWorld.RobotDirection) * 90f;
        return new Vector3(0, angle, 0);
    }

    private void OnDestroy()
    {
        _robotWorld.RobotPlaced.RemoveListener(HandleRobotPlaced);
        _robotWorld.RobotMoved.RemoveListener(HandleRobotMoved);
        _robotWorld.RobotRotated.RemoveListener(HandleRobotRotated);
    }
}
