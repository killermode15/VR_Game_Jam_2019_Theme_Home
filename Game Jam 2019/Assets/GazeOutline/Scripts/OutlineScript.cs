using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Homebrew;
using System.Linq;

/// <summary>
/// IMPORTANT SETUP BEFORE USAGE:
/// Mesh Renderer -> Materials
/// Must be size 2
/// Element 0 is null / none
/// Element 1 is the material of the mesh
/// 
/// No need to create a material for each unique object in the scene, this script instances its own material,
///     just dont forget to set the outlineShader(VRTK/OutlineBasic)
///     
/// Percent is from 0.0f to 1.0f
/// </summary>
public class OutlineScript : MonoBehaviour
{
    [Header("See script for setup notes")]

    public Shader outlineShader;

    Material outlineMaterial;

    [Foldout("Outline Properties", true)]
    public Color minColor = Color.white;
    public Color maxColor = Color.black;

    public float minThickness = 0.0f;
    public float maxThickness = 3.0f;

    // The final color and thickness once this object has been activated
    public Color finalColor = Color.red;
    public float finalThickness = 2.5f;

    //[Foldout("Timer Properties (Set)", true)]
    //public float maxGazeTimeInSecs = 5.0f;

    //[Foldout("Timer Properties (Read Only)", true)]
    [HideInInspector]
    public float percent = 0.0f;
    [HideInInspector]
    public bool isGazedAt = false;

    //[Tooltip("Is true when the player has reached the max gaze time. Sets the outline permanently and completes/stops the timer for this object.")]
    [HideInInspector]
    public bool isActivated = false;
    [HideInInspector]
    public float curTime;

    //[Foldout("Called when this object has been gazed on beyond maxGazeTimeInSecs. Called only once.")]
    [Foldout("On Full Outline Event")]
    public UnityEvent OnActivateEvent = new UnityEvent();

    void Start()
    {
        outlineMaterial = new Material(outlineShader);

        outlineMaterial.SetColor("_OutlineColor", minColor);
        outlineMaterial.SetFloat("_Thickness", minThickness);

        List<Material> materials = GetComponent<MeshRenderer>().materials.ToList();
        materials.Add(outlineMaterial);
        GetComponent<MeshRenderer>().materials = materials.ToArray();

        //GetComponent<MeshRenderer>().materials[GetComponent<MeshRenderer>().materials.Length] = outlineMaterial;

        curTime = 0.0f;
        isActivated = false;
    }

    void Update()
    {
        //if (!isActivated)
        //{
        //    if (isGazedAt)
        //    {
        //        curTime += Time.deltaTime;

        //        if (curTime >= maxGazeTimeInSecs)
        //        {
        //            OnActivate();
        //        }
        //    }
        //    else
        //    {
        //        curTime -= Time.deltaTime;
        //    }

        //    curTime = Mathf.Clamp(curTime, 0.0f, maxGazeTimeInSecs);
        //    percent = Mathf.Lerp(0.0f, 1.0f, curTime / maxGazeTimeInSecs);
        //}

        if (!isActivated)
        {
            outlineMaterial.SetColor("_OutlineColor", Color.Lerp(minColor, maxColor, percent));
            outlineMaterial.SetFloat("_Thickness", Mathf.Lerp(minThickness, maxThickness, percent));
        }
    }

    public void OnActivate()
    {
        isActivated = true;

        outlineMaterial.SetColor("_OutlineColor", finalColor);
        outlineMaterial.SetFloat("_Thickness", finalThickness);

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
