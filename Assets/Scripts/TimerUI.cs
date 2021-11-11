using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

	public Text text;

	void Update ()
	{
		text.text = GameManager.Timer.ToString("00") + " SECONDS\nREMAINING";
    }

}
