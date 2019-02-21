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
    public class ShadowGaze
    {
        public ShadowMoveTowards shadow;
        public GazeObject gazeObj;
        public bool isDone;
    }

    public ShadowGaze Area1;
    public List<ShadowGaze> Area2;
    //public ShadowGaze Area2_1;
    //public ShadowGaze Area2_2;
    public List<ShadowGaze> Area3;

    void Start()
    {

    }

    // Shitty code incoming pls forgive me
    void Update()
    {
        // Area 1 check
        if (!AreaClear(Area1))
        {
            if (Area1.gazeObj.isEnded)
            {
                Area1.shadow.gameObject.SetActive(false);
                Area1.isDone = true;
                ActivateArea(Area2);
            }
        }

        // Area 2 check
        if (AreaClear(Area1) && !AreaClear(Area2))
        {
            ClearingArea(Area2);
        }

        if (AreaClear(Area2))
        {
            ActivateArea(Area3);
        }

        #region old area 2 check
        //else if ((!Area2_1.isDone || !Area2_2.isDone))
        //{
        //    if (!Area2_1.isDone && Area2_1.gazeObj.isEnded)
        //    {
        //        Area2_1.shadow.gameObject.SetActive(false);
        //        Area2_1.isDone = true;
        //    }
        //    if (!Area2_2.isDone && Area2_2.gazeObj.isEnded)
        //    {
        //        Area2_2.shadow.gameObject.SetActive(false);
        //        Area2_2.isDone = true;
        //    }
        //}
        #endregion

        // Area 3 check
        if (!AreaClear(Area3) && (AreaClear(Area2) && AreaClear(Area1)))
        {
            ClearingArea(Area3);
        }

        Debug.Log("Area 1: " + AreaClear(Area1));
        Debug.Log("Area 2: " + AreaClear(Area2));
        Debug.Log("Area 3: " + AreaClear(Area3));
    }

    public void StartGame()
    {
        Area1.shadow.gameObject.SetActive(true);
    }

    void ClearingArea(List<ShadowGaze> currArea)
    {
        for (int i = 0; i < currArea.Count; i++)
        {
            if (!currArea[i].isDone && currArea[i].gazeObj.isEnded)
            {
                currArea[i].shadow.gameObject.SetActive(false);
                currArea[i].isDone = true;
            }
        }
    }
    
    void ActivateArea(List<ShadowGaze> area)
    {
        foreach(ShadowGaze sg in area)
        {
            sg.shadow.gameObject.SetActive(true);
            sg.gazeObj.enabled = true;
        }
    }

    /// <summary>
    /// To check if an area with singular threat is clear
    /// </summary>
    /// <param name="shadowGaze"></param>
    /// <returns></returns>
    public bool AreaClear(ShadowGaze shadowGaze)
    {
        if(shadowGaze.isDone)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// To check if an area with multiple threat is clear
    /// </summary>
    /// <param name="shadowGazes"></param>
    /// <returns></returns>
    public bool AreaClear(List<ShadowGaze> shadowGazes)
    {
        int num = shadowGazes.Count;

        if (num > 0)
        {
            foreach (ShadowGaze sg in shadowGazes)
            {
                if (sg.isDone) num--;
            }
            return false;
        }
        else
        {
            return true;
        }
    }
}
