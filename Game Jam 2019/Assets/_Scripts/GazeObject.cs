using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(OutlineScript))]
public class GazeObject : MonoBehaviour
{
    [Header("Scripts")]
    public List<EmissionScript> emissionScripts;
    public List<OutlineScript> outlineScripts;

    [Header("Events")]
    public UnityEvent UE_OnGazeStart;
    public UnityEvent UE_OnGazeUpdate;
    public UnityEvent UE_OnGazeEnd;
    public UnityEvent UE_OnGazeExit;

    [Header("Gaze Properties")]
    public float MaxGazeTime;

    public bool IsGazingFinished => isGazing && currentGazeTime <= 0;
    public float GazePercent => currentGazeTime / MaxGazeTime;

    // Is this gaze object activated
    public bool isEnded = false;

    private float currentGazeTime;
    private bool isGazing;

    private void Start()
    {
        currentGazeTime = MaxGazeTime;

    }

    private void Update()
    {
        if (isGazing)
        {
            OnGazeUpdate();
        }
        else
        {
            if (currentGazeTime < MaxGazeTime)
            {
                currentGazeTime += Time.deltaTime;

                if (currentGazeTime > MaxGazeTime)
                {
                    currentGazeTime = MaxGazeTime;
                }
            }
        }

        foreach (OutlineScript script in outlineScripts)
        {
            script.percent = 1.0f - GazePercent;
        }

        foreach (EmissionScript script in emissionScripts)
        {
            script.percent = 1.0f - GazePercent;
        }
    }

    [ContextMenu("TEST")]
    public void OnGazeStart()
    {
        if (!isGazing)
        {
            isGazing = true;
            UE_OnGazeStart.Invoke();

            foreach (OutlineScript script in outlineScripts)
            {
                script.GazedAt();
            }

            foreach (EmissionScript script in emissionScripts)
            {
                script.GazedAt();
            }
            Debug.Log("Start");
        }
    }

    public void OnGazeUpdate()
    {
        currentGazeTime -= Time.deltaTime;

        foreach (OutlineScript script in outlineScripts)
        {
            script.percent = 1.0f - GazePercent;
        }

        foreach (EmissionScript script in emissionScripts)
        {
            script.percent = 1.0f - GazePercent;
        }

        UE_OnGazeUpdate.Invoke();
        if (currentGazeTime <= 0)
        {
            currentGazeTime = 0;
            OnGazeEnd();
        }
    }

    public void OnGazeEnd()
    {
        if (isGazing)
        {
            isGazing = false;
            isEnded = true;
            UE_OnGazeEnd.Invoke();

            foreach (OutlineScript script in outlineScripts)
            {
                script.OnActivate();
            }

            foreach (EmissionScript script in emissionScripts)
            {
                script.OnActivate();
            }
            Debug.Log("End");
        }
    }

    public void OnGazeExit()
    {
        isGazing = false;
        UE_OnGazeExit.Invoke();

        foreach (OutlineScript script in outlineScripts)
        {
            script.NotGazedAt();
        }

        foreach (EmissionScript script in emissionScripts)
        {
            script.NotGazedAt();
        }
        Debug.Log("Exit");
    }
}
