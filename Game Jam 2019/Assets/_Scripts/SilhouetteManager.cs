using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public BoxCollider collider;
        public bool isDone;
    }

    public enum Sequence { NONE, ONE, TWO, THREE, FOUR, FIVE};

    [SerializeField] private Sequence currentSequence;

    public ShadowGaze Area1;
    public List<ShadowGaze> Area2;
    //public ShadowGaze Area2_1;
    //public ShadowGaze Area2_2;
    public List<ShadowGaze> Area3;

    public GameObject Area4;
    public Light sunlight;
    public UnityEvent OnAreasFinish = new UnityEvent();

    bool isSunlight = false;

    public FadeOutCanvas fadeOut;

    private void Start()
    {
        currentSequence = Sequence.ONE;
    }

    // Shitty code incoming pls forgive me
    // kinda fixed shitty code
    void Update()
    {
        #region anton's old SH manager
        /*
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
            ClearingArea(Area2,Area3);
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
        if (!AreaClear(Area3) && AreaClear(Area2))
        {
            ClearingArea(Area3);
        }
        Debug.Log("Area 1: " + AreaClear(Area1));
        Debug.Log("Area 2: " + AreaClear(Area2));
        Debug.Log("Area 3: " + AreaClear(Area3));
        */
        #endregion anton's old SH Manager
    }

    private void FixedUpdate()
    {
        UpdateSequence();
    }

    void UpdateSequence()
    {
        switch(currentSequence)
        {
            case Sequence.ONE:   Sequence1(); break;
            case Sequence.TWO:   Sequence2(); break;
            case Sequence.THREE: Sequence3(); break;
            case Sequence.FIVE: Sequence5(); break;
        }
    }

    void Sequence1()
    {
        if (!AreaClear(Area1))
        {
            if (Area1.gazeObj.isEnded)
            {
                Area1.shadow.gameObject.SetActive(false);
                Area1.isDone = true;
                Area1.gazeObj.enabled = false;
                Area1.collider.enabled = false;
            }
        }
        else
        {
            Debug.Log("Area 1 clear");

            foreach (ShadowGaze sg in Area2)
            {
                sg.shadow.gameObject.SetActive(true);
                sg.gazeObj.enabled = true;
                sg.collider.enabled = true;
            }

            currentSequence = Sequence.TWO;
        }
    }

    void Sequence2()
    {
        if (!Area2[0].isDone || !Area2[1].isDone)
        {
            if (Area2[0].gazeObj.isEnded)
            {
                Area2[0].shadow.gameObject.SetActive(false);
                Area2[0].isDone = true;
                Area2[0].gazeObj.enabled = false;
                Area2[0].collider.enabled = false;
            }

            if (Area2[1].gazeObj.isEnded)
            {
                Area2[1].shadow.gameObject.SetActive(false);
                Area2[1].isDone = true;
                Area2[1].gazeObj.enabled = false;
                Area2[1].collider.enabled = false;
            }
        }
        else
        {
            Debug.Log("Area 2 clear");

            foreach (ShadowGaze sg in Area3)
            {
                sg.shadow.gameObject.SetActive(true);
                sg.gazeObj.enabled = true;
                sg.collider.enabled = true;
            }

            currentSequence = Sequence.THREE;
        }
    }

    void Sequence3()
    {
        if(!Area3[0].isDone || !Area3[1].isDone || !Area3[2].isDone)
        {
            if (Area3[0].gazeObj.isEnded)
            {
                Area3[0].shadow.gameObject.SetActive(false);
                Area3[0].isDone = true;
                Area3[0].gazeObj.enabled = false;
                Area3[0].collider.enabled = false;
            }

            if (Area3[1].gazeObj.isEnded)
            {
                Area3[1].shadow.gameObject.SetActive(false);
                Area3[1].isDone = true;
                Area3[1].gazeObj.enabled = false;
                Area3[1].collider.enabled = false;
            }

            if (Area3[2].gazeObj.isEnded)
            {
                Area3[2].shadow.gameObject.SetActive(false);
                Area3[2].isDone = true;
                Area3[2].gazeObj.enabled = false;
                Area3[2].collider.enabled = false;
            }
        }
        else
        {
            Debug.Log("Area 3 clear");
            currentSequence = Sequence.FOUR;
            StartCoroutine(TurnOffAllTheLights());
        }
    }

    void Sequence5()
    {
        if (Area4.GetComponent<GazeObject>().isEnded && !isSunlight)
        {
            Area4.SetActive(false);
            StartCoroutine(WaitForSun());
        }
        if (isSunlight)
        {
            sunlight.intensity += Time.deltaTime * 0.1f;
            sunlight.shadowStrength -= Time.deltaTime * 0.1f;

            if (sunlight.shadowStrength <= 0.5f)
            {
                fadeOut.StartFadeOut();
            }
        }
    }

    void LightsTurnOff()
    {
        OnAreasFinish.Invoke();
        foreach (EmissionScript emission in Area1.gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area1.gazeObj.outlineScripts)
            outline.TurnOff();

        foreach (EmissionScript emission in Area2[0].gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area2[0].gazeObj.outlineScripts)
            outline.TurnOff();

        foreach (EmissionScript emission in Area2[1].gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area2[1].gazeObj.outlineScripts)
            outline.TurnOff();

        foreach (EmissionScript emission in Area3[0].gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area3[0].gazeObj.outlineScripts)
            outline.TurnOff();

        foreach (EmissionScript emission in Area3[1].gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area3[1].gazeObj.outlineScripts)
            outline.TurnOff();

        foreach (EmissionScript emission in Area3[2].gazeObj.emissionScripts)
            emission.TurnOff();

        foreach (OutlineScript outline in Area3[2].gazeObj.outlineScripts)
            outline.TurnOff();
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

    void ClearingArea(List<ShadowGaze> currArea, List<ShadowGaze> nextArea)
    {
        for (int i = 0; i < currArea.Count; i++)
        {
            if (!currArea[i].isDone && currArea[i].gazeObj.isEnded)
            {
                Debug.Log("DELETED TOMATO");
                currArea[i].shadow.gameObject.SetActive(false);
                currArea[i].isDone = true;
            }

            if (currArea[i].isDone && currArea[i].gazeObj.isEnded)
            {
                Debug.Log("TOMATO POTATO");
                ActivateArea(nextArea);
            }
        }
    }

    #region this thing breaks unity don't use unless u has fix
    /*
    void ClearingArea(List<ShadowGaze> currArea, List<ShadowGaze> nextArea)
    {
        while (!AreaClear(currArea))
        {
            for (int i = 0; i < currArea.Count; i++)
            {
                if (!currArea[i].isDone && currArea[i].gazeObj.isEnded)
                {
                    currArea[i].shadow.gameObject.SetActive(false);
                    currArea[i].isDone = true;
                    return;
                }
            }
        }
        ActivateArea(nextArea);
    }
    */
    #endregion

    void ActivateArea(List<ShadowGaze> area)
    {
        foreach(ShadowGaze sg in area)
        {
            sg.shadow.gameObject.SetActive(true);
            sg.gazeObj.GetComponent<BoxCollider>().enabled = true;
            sg.gazeObj.enabled = true;
        }
    }

    IEnumerator TurnOffAllTheLights()
    {
        yield return new WaitForSeconds(5.0f);
        LightsTurnOff();
        yield return new WaitForSeconds(3.0f);
        Area4.SetActive(true);
        currentSequence = Sequence.FIVE;
    }

    IEnumerator WaitForSun()
    {
        yield return new WaitForSeconds(2.0f);
        isSunlight = true;
        sunlight.enabled = true;
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
