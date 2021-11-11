using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

	public Image speakerIcon;

	public Sprite speakerOn;
	public Sprite speakerOff;

	public AudioMixer masterMixer;
	public AudioMixerSnapshot[] snapshots;

	private static bool isOn = true;

	void Start ()
	{
		if (!isOn)
			speakerIcon.sprite = speakerOff;
	}

	public void Toggle ()
	{
		float[] weights;
        if (isOn)
		{
			speakerIcon.sprite = speakerOff;
			weights = new float[] { 0f, 1f };
			isOn = false;
		} else
		{
			speakerIcon.sprite = speakerOn;
			weights = new float[] { 1f, 0f };
			isOn = true;
		}
		masterMixer.TransitionToSnapshots(snapshots, weights, 0f);
	}

}
