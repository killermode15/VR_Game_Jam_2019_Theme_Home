using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZoneTrigger : MonoBehaviour
{
    public AudioMixerSnapshot SnapshotTo;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Spirit")
        {
            SnapshotTo.TransitionTo(1.25f);
            Debug.Log("Spirit Entered");
        }
    }
}
