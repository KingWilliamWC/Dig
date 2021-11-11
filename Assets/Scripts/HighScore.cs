using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	public GameObject newRecord;
	public Text highscore;

	void Start ()
	{
		StoreHighscore(GameManager.Money);
	}

	void StoreHighscore(int score)
	{
		int prevHighscore = PlayerPrefs.GetInt("Highscore", 0);
		if (score > prevHighscore)
		{
			NewHighscore(score);
			AudioManager.instance.Play("NewRecordTheme");
			highscore.text = "$" + score;
        } else
		{
			AudioManager.instance.Play("GameOverTheme");
			highscore.text = "$" + prevHighscore;
		}

		AudioManager.instance.Stop("MainTheme");
	}

	void NewHighscore (int score)
	{
		newRecord.SetActive(true);
        PlayerPrefs.SetInt("Highscore", score);
	}

}
