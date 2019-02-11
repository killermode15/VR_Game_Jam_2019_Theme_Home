using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeObject : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent UE_OnGazeStart;
    public UnityEvent UE_OnGazeUpdate;
    public UnityEvent UE_OnGazeEnd;
    public UnityEvent UE_OnGazeExit;

    [Header("Gaze Properties")]
    public float GazeTime;
    public Material OutlineMaterial;

    public bool IsGazingFinished => isGazing && currentGazeTime <= 0;

    private float currentGazeTime;
    private bool isGazing;

    private void Start()
    {
        currentGazeTime = GazeTime;
    }

    private void Update()
    {
        if (isGazing)
        {
            OnGazeUpdate();
        }
        else
        {
            if(currentGazeTime < GazeTime)
            {
                UpdateOutlineAlpha();
                currentGazeTime += Time.deltaTime;

                if(currentGazeTime > GazeTime)
                {
                    currentGazeTime = GazeTime;
                }
            }
        }
    }

    private void UpdateOutlineAlpha()
    {
        Color currColor = GetComponent<Renderer>().material.GetColor("_OutlineColor");
        currColor.a = 1 - (currentGazeTime / GazeTime);
        GetComponent<Renderer>().material.SetColor("_OutlineColor", currColor);
    }

    public void OnGazeStart()
    {
        if (!isGazing)
        {
            isGazing = true;
            UE_OnGazeStart.Invoke();
            Debug.Log("Start");
        }
    }

    public void OnGazeUpdate()
    {
        currentGazeTime -= Time.deltaTime;

        UpdateOutlineAlpha();

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
            Debug.Log("End");
        }
    }

    public void OnGazeExit()
    {
        isGazing = false;
        UE_OnGazeExit.Invoke();
        Debug.Log("Exit");
    }
}
