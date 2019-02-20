using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Homebrew;

public class EmissionScript : MonoBehaviour
{
    // Private
    Material mat;

    // Public
    [Foldout("Emission Properties", true)]
    public Color minColor = Color.black;
    public Color maxColor = Color.yellow;

    public float minEmit = 0.0f;
    public float maxEmit = 1.0f;

    public Color finalColor = Color.yellow;
    public float finalEmit = 1.0f;

    [Foldout("Read Only", true)]
    public float percent = 0.0f;
    public bool isGazedAt = false;
    public bool isActivated = false;

    [Foldout("On Full Emission Event")]
    public UnityEvent OnActivateEvent = new UnityEvent();

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", minColor * Mathf.LinearToGammaSpace(minEmit));
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
        {
            Color setColor = Color.Lerp(minColor, maxColor, percent);
            float setEmit = Mathf.Lerp(minEmit, maxEmit, percent);

            mat.SetColor("_EmissionColor", setColor * Mathf.LinearToGammaSpace(setEmit));
        }
    }

    public void OnActivate()
    {
        isActivated = true;

        mat.SetColor("_EmissionColor", finalColor * Mathf.LinearToGammaSpace(finalEmit));

        OnActivateEvent.Invoke();
    }

    // Public Functions
    /// <summary>
    /// Call this once the player starts gazing on this object
    /// </summary>
    public void GazedAt()
    {
        isGazedAt = true;
    }

    /// <summary>
    /// Call this once the player stops gazing on this object
    /// </summary>
    public void NotGazedAt()
    {
        isGazedAt = false;
    }
}
