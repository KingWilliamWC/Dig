using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public void Continue ()
	{
		GameManager.instance.TogglePause();
	}

	public void Restart ()
	{
		GameManager.instance.TogglePause();
		GameManager.instance.RestartLevel();
	}

	public void Menu ()
	{
		GameManager.instance.TogglePause();
		GameManager.instance.GoToMenu();
	}

}
