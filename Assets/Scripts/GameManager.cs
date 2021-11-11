using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	void Awake ()
	{
		instance = this;

		Timer = levelLength;
		Money = 0;

		IsDialoguePaused = false;
	}

	public static int Money = 0;
	public static float Timer = 999999f;

	public static bool IsPaused = false;
	public static bool IsDialoguePaused = false;

	public float levelLength = 30f;

	public delegate void OnAddMoneyDelegate(int amount);
	public OnAddMoneyDelegate onAddMoney;

	public delegate void OnAddTimeDelegate(int amount);
	public OnAddTimeDelegate onAddTime;

	public GameObject pauseMenu;
	public GameObject endGame;

	public string menuSceneName = "MainMenu";

	private bool isEndGame = false;

	void Update ()
	{
		if (isEndGame)
			return;

		if (Input.GetButtonDown("Cancel"))
			TogglePause();

		if (IsPaused || IsDialoguePaused)
			return;

		Timer -= Time.deltaTime;
		if (Timer <= 0f)
		{
			Timer = 0f;
			EndGame();
		}
	}

	void EndGame ()
	{
		//AudioManager.instance.Play("OutOfTime");

		isEndGame = true;
		endGame.SetActive(true);
		IsPaused = true;
	}

	public void AddMoney (int amount)
	{
		Money += amount;
		if (onAddMoney != null)
			onAddMoney.Invoke(amount);
	}

	public void AddTime (int amount)
	{
		Timer += amount;
		if (onAddTime != null)
			onAddTime.Invoke(amount);
	}

	public void TogglePause ()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		if (pauseMenu.activeSelf)
		{
			Time.timeScale = 0f;
		} else
		{
			Time.timeScale = 1f;
		}
		IsPaused = pauseMenu.activeSelf;
	}

	public void RestartLevel ()
	{
		FadeManager.instance.TransitionToScene(SceneManager.GetActiveScene().name);
	}

	public void GoToMenu ()
	{
		FadeManager.instance.TransitionToScene(menuSceneName);
	}

}
