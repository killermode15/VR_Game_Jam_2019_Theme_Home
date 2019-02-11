using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    public LayerMask GazeLayer;
    public Transform Eye;

    private GazeObject gazedObject;

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
            gazedObject = hit.collider.GetComponent<GazeObject>();

            if (!gazedObject)
            {
                return;
            }

            gazedObject.OnGazeStart();
        }
        else
        {
            if (gazedObject)
            {
                gazedObject.OnGazeExit();
                gazedObject = null;
            }
        }

    }
}
