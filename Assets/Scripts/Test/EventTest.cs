using System;
using System.Collections;
using System.Collections.Generic;
using Tilia.Interactions.Controllables.LinearDriver;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public LinearDriveFacade button;
    public bool isLogOnTargetValueReached = true;
    public bool isLogOnValueChanged = true;
    public bool isLogInitialTargetValueReached = true;
    public bool isLogStepValueChanged = true;
    public bool isLogOnStoppedMoving = true;
    public bool isLogOnStartedMoving= true;
    void Start()
    {
        button.InitialTargetValueReached.AddListener(InitialTargetValueReached);
        button.ValueChanged.AddListener(OnValueChanged);
        button.TargetValueReached.AddListener(OnTargetValueReached);
        button.StepValueChanged.AddListener(OnStepValueChanged);
        button.StoppedMoving.AddListener(OnStoppedMoving);
        button.StartedMoving.AddListener(OnStartedMoving);
    }

    private void OnStartedMoving(float arg0)
    {
        if (!isLogOnStartedMoving) return;
        Debug.Log($"OnStartedMoving {arg0}");
    }

    private void OnStoppedMoving(float arg0)
    {
        if (!isLogOnStoppedMoving) return;
        Debug.Log($"isLogOnStoppedMoving {arg0}");
    }

    private void OnStepValueChanged(float arg0)
    {
        //if (!isLogStepValueChanged) return;
        Debug.Log($"OnStepValueChanged {arg0} - {name}");
    }

    private void OnTargetValueReached(float arg0)
    {
        if (!isLogOnTargetValueReached) return;
        Debug.Log($"OnTargetValueReached {arg0}");
    }

    private void OnValueChanged(float arg0)
    {
        if (!isLogOnValueChanged) return;
        Debug.Log($"OnValueChanged {arg0}");

    }

    private void InitialTargetValueReached(float arg0)
    {
        if (!isLogInitialTargetValueReached) return;
        Debug.Log($"InitialTargetValueReached {arg0}");
    }


    //private void OnDestroy()
    //{
    //    button.InitialTargetValueReached.RemoveListener(InitialTargetValueReached);
    //    button.ValueChanged.RemoveListener(OnValueChanged);
    //    button.TargetValueReached.RemoveListener(OnTargetValueReached);
    //    button.StepValueChanged.RemoveListener(OnStepValueChanged);
    //    button.StoppedMoving.RemoveListener(OnStoppedMoving);
    //    button.StartedMoving.RemoveListener(OnStartedMoving);
    //}
}
