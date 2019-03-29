using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTo : MonoBehaviour
{
    /// <summary>
    /// A script that justs transitions an Audio Mixer Snapshot
    /// </summary>
    /// <param name="snapTo"></param>
    public void TransitionToSnap(AudioMixerSnapshot snapTo)
    {
        snapTo.TransitionTo(1.5f);
    }
}
