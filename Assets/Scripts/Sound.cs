using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f,1f)]
	public float baseVolume = .7f;
	[Range(0f, 1f)]
	public float volumeRandomness = .1f;

	[Range(0f, 10f)]
	public float basePitch = 1f;
	[Range(0f, 1f)]
	public float pitchRandomness = 0.1f;

	public bool loop = false;

	[HideInInspector]
	public AudioSource source;

}
