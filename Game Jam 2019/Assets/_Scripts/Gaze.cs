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
    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(Eye.position, Eye.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GazeLayer))
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
}
