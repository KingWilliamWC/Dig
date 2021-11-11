using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string mainLevelName = "MainLevel";

	public void Play ()
	{
		FadeManager.instance.TransitionToScene(mainLevelName);
	}

	public void Quit ()
	{
		Debug.Log("EXITING!");
		Application.Quit();
	}
	
}
