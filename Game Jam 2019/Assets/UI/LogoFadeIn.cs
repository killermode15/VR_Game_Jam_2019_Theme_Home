using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoFadeIn : MonoBehaviour
{
    public GazeObject gazeObj;

    CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }
    
    void Update()
    {
        cg.alpha = 0.5f - gazeObj.GazePercent;
    }
}
