using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteManager : MonoBehaviour
{
    [System.Serializable]
    public struct ShadowGaze
    {
        public ShadowMoveTowards shadow;
        public GazeObject gazeObj;
    }

    public ShadowGaze Area1;

    void Start()
    {
        //Temp pls remove
        //StartGame();
    }
    
    void Update()
    {
        
    }

    public void StartGame()
    {
        Area1.shadow.gameObject.SetActive(true);
    }
}
