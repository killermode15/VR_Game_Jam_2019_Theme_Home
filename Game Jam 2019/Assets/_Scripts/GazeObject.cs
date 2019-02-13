using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(OutlineScript))]
public class GazeObject : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent UE_OnGazeStart;
    public UnityEvent UE_OnGazeUpdate;
    public UnityEvent UE_OnGazeEnd;
    public UnityEvent UE_OnGazeExit;

    [Header("Gaze Properties")]
    public float MaxGazeTime;

    public bool IsGazingFinished => isGazing && currentGazeTime <= 0;
    public float GazePercent => currentGazeTime / MaxGazeTime;

    private OutlineScript outline;
    private float currentGazeTime;
    private bool isGazing;

    private void Start()
    {
        currentGazeTime = MaxGazeTime;
        outline = GetComponent<OutlineScript>();
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
    }

    [ContextMenu("TEST")]
    public void OnGazeStart()
    {
        if (!isGazing)
        {
            isGazing = true;
            UE_OnGazeStart.Invoke();
            outline.GazedAt();
            Debug.Log("Start");
        }
    }

    public void OnGazeUpdate()
    {
        currentGazeTime -= Time.deltaTime;

        outline.percent = GazePercent;

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
            UE_OnGazeEnd.Invoke();
            outline.OnActivate();
            Debug.Log("End");
        }
    }

    public void OnGazeExit()
    {
        isGazing = false;
        UE_OnGazeExit.Invoke();
        outline.NotGazedAt();
        Debug.Log("Exit");
    }
}
