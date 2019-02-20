using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Don't expect much from this.
/// This is fucking hardcoded as hell XDDDD
/// </summary>
public class SilhouetteManager : MonoBehaviour
{
    [System.Serializable]
    public struct ShadowGaze
    {
        public ShadowMoveTowards shadow;
        public GazeObject gazeObj;
        public bool isDone;
    }

    public ShadowGaze Area1;
    public ShadowGaze Area2_1;
    public ShadowGaze Area2_2;

    void Start()
    {

    }
    
    // Shitty code incoming pls forgive me
    void Update()
    {
        // Area 1 check
        if (!Area1.isDone)
        {
            if (Area1.gazeObj.isEnded)
            {
                Area1.shadow.gameObject.SetActive(false);
                Area1.isDone = true;
                Area2_1.shadow.gameObject.SetActive(true);
                Area2_2.shadow.gameObject.SetActive(true);

                Area2_1.gazeObj.enabled = true;
                Area2_2.gazeObj.enabled = true;
            }
        }
        else
        {
            // Area 2 check
            if (!Area2_1.isDone || !Area2_2.isDone)
            {
                if (!Area2_1.isDone && Area2_1.gazeObj.isEnded)
                {
                    Area2_1.shadow.gameObject.SetActive(false);
                    Area2_1.isDone = true;
                }
                if (!Area2_2.isDone && Area2_2.gazeObj.isEnded)
                {
                    Area2_2.shadow.gameObject.SetActive(false);
                    Area2_2.isDone = true;
                }
            }
            else
            {
                // Area 3 check
            }
        }
    }

    public void StartGame()
    {
        Area1.shadow.gameObject.SetActive(true);
    }
}
