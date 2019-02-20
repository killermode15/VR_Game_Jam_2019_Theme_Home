using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    public LayerMask GazeLayer;
    public Transform Eye;

    public GazeObject gazedObject = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (Eye)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawRay(Eye.position, Eye.forward * 20);
        }
    }

    #region Attached to Player Parent

    /// <summary>
    /// Raycast/Sphercast way to detect mementos
    /// </summary>
    /*
    void FixedUpdate()
    {        
        Ray ray = new Ray(Eye.position, Eye.forward);
        RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, GazeLayer))
        if(Physics.SphereCast(ray, 2.5f, out hit, Mathf.Infinity, GazeLayer))
        {
            // If there is no current gazed object, set gazed object to the hit
            if (!gazedObject)
                gazedObject = hit.collider.GetComponent<GazeObject>();

            // If there is previous gazed object
            if (gazedObject)
            {
                // If the previous game object is not the same as the new hit
                if (gazedObject != hit.collider.GetComponent<GazeObject>())
                {
                    gazedObject.OnGazeExit();
                    gazedObject = hit.collider.GetComponent<GazeObject>();
                    gazedObject.OnGazeStart();
                }
                else // If the previous game object is the same as the new hit
                {
                    gazedObject.OnGazeStart();
                    Debug.Log(gazedObject.name);
                }
            }
            
        }

        // If not gazing at anything
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, GazeLayer))
        {
            if (gazedObject)
            {
                gazedObject.OnGazeExit();
                //gazedObject = null;
            }
        }

    }
      */
    #endregion

    #region Attached to Capsule under Camera (eye) 
    
    /// <summary>
    /// Pinocchio Nose Da Wae
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GazeObject"))
        {
            // If there is no current gazed object, set gazed object to the hit
            if (!gazedObject)
                gazedObject = other.GetComponent<GazeObject>();

            // If there is previous gazed object
            if (gazedObject)
            {
                // If the previous game object is not the same as the new hit
                if (gazedObject != other.GetComponent<GazeObject>())
                {
                    gazedObject.OnGazeExit();
                    gazedObject = other.GetComponent<GazeObject>();
                    gazedObject.OnGazeStart();
                }
                else // If the previous game object is the same as the new hit
                {
                    gazedObject.OnGazeStart();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("GazeObject"))
        {
            if(gazedObject)
            {
                gazedObject.OnGazeExit();
            }
        }
    }
    #endregion
}
