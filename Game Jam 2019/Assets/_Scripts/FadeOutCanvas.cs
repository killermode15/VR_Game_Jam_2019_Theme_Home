using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeOutCanvas : MonoBehaviour
{
    public float fadeOutTime = 3.0f;

    public float timer;

    CanvasGroup cg;
    bool isTimer;

    public UnityEvent OnTimerDone = new UnityEvent();

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0.0f;
        timer = 0.0f;
        isTimer = false;
    }
    
    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            cg.alpha = timer / fadeOutTime;

            if (timer >= fadeOutTime)
            {
                cg.alpha = 1.0f;
                isTimer = false;
                OnTimerDone.Invoke();
            }
        }
    }

    public void StartFadeOut()
    {
        isTimer = true;
    }
}
