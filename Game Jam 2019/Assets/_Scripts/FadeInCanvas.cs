using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeInCanvas : MonoBehaviour
{
    public float fadeInTime = 3.0f;

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

        StartFadeIn();
    }

    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            cg.alpha = 1.0f - ( timer / fadeInTime);

            if (timer >= fadeInTime)
            {
                cg.alpha = 0.0f;
                isTimer = false;
                OnTimerDone.Invoke();
            }
        }
    }

    public void StartFadeIn()
    {
        isTimer = true;
    }
}
