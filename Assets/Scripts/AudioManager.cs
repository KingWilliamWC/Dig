using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	void Awake ()
	{
		instance = this;

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i].source = audioPool.AddComponent<AudioSource>();
		}

		Play("MainTheme");
	}

	public GameObject audioPool;

	public AudioMixerGroup masterMixer;

	public Sound[] pool;

	public void Play (string soundName)
	{
		Sound sound = Array.Find(pool, element => element.name == soundName);

		if (sound == null)
		{
			Debug.LogError("Sound not found: " + soundName);
			return;
		}

		AudioSource source = sound.source;
		source.clip = sound.clip;
		source.volume = UnityEngine.Random.Range(sound.baseVolume - sound.volumeRandomness, sound.baseVolume + sound.volumeRandomness);
		source.pitch = UnityEngine.Random.Range(sound.basePitch - sound.pitchRandomness, sound.basePitch + sound.pitchRandomness);

		source.loop = sound.loop;

		source.outputAudioMixerGroup = masterMixer;

		source.Play();
	}

	public void Stop (string soundName)
	{
		Sound sound = Array.Find(pool, element => element.name == soundName);

		sound.source.Stop();
	}
	
}
