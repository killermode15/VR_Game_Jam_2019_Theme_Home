using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShadowStare : MonoBehaviour
{   
    [SerializeField] private float stareTime;
    [SerializeField] private bool staringAtPlayer;
    [SerializeField] private Transform wallOffset;
    [SerializeField] private float emergeSpeed;

    private void Update()
    {
        if(staringAtPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, wallOffset.position, emergeSpeed * Time.deltaTime);
        }
    }

    public void FacePlayer()
    {
        print("POTATO POTATO");
        staringAtPlayer = true;
    }
}
