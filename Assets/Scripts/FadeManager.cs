using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

	public Image black;

	public static FadeManager instance;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void TransitionToScene (string sceneName)
	{
		StartCoroutine(FadeOut(sceneName));
    }

	IEnumerator FadeOut (string sceneName)
	{
		float a = 0f;
		while (a < 1f)
		{
			a += Time.deltaTime;
			black.color = new Color(0f, 0f, 0f, a);

			yield return null;
		}

		SceneManager.LoadScene(sceneName);
	}

	IEnumerator FadeIn()
	{
		float a = 1f;
		while (a > 0f)
		{
			a -= Time.deltaTime;
			black.color = new Color(0f, 0f, 0f, a);

			yield return null;
		}
	}

}
