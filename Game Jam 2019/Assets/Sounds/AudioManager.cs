using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioData
{
	public string Identifier;

	public bool UseSnapshot = false;

	public AudioClip AudioClip;
	public AudioMixerSnapshot MixerSnapshot;

	public AudioSource PlayAudio (bool play = true, bool destroyAfter = false)
	{
		if (!UseSnapshot)
		{
			AudioSource source = new GameObject ("Source").AddComponent<AudioSource> ();
			source.clip = AudioClip;
			if (play)
			{
				source.Play ();
			}
			if (destroyAfter)
			{
				GameObject.Destroy (source.gameObject, source.clip.length);
			}
			return source;
		}
		else
		{
			Debug.LogWarning ("Using PlayAudio() while Use Snapshot is true");
			return null;
		}
	}

	public void PlaySnapshot (float timeToReach)
	{
		if (UseSnapshot)
		{
			MixerSnapshot.TransitionTo (timeToReach);
		}
		else
		{
			Debug.LogWarning("Using PlaySnapshot() while Use Snapshot is false");
		}
	}
}

[CreateAssetMenu (fileName = "Audio Manager")]
public class AudioManager : ScriptableObject
{
	public List<AudioData> AudioData;

	public AudioData GetAudio (string identifier)
	{
		return AudioData.Find (x => x.Identifier.ToLower () == identifier.ToLower ()); 
	}
}

