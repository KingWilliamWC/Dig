using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

	public Text moneyEarned;

	void OnEnable ()
	{
		StartCoroutine(CountMoneyEarned());
	}

	IEnumerator CountMoneyEarned ()
	{
		int money = GameManager.Money;

		yield return new WaitForSeconds(1f);

		int counted = 0;

		while (counted < money)
		{
			AudioManager.instance.Play("MoneyEarned");
			counted += (int)(money / 1 * Time.deltaTime);
			moneyEarned.text = "$" + counted;
            yield return null;
		}

		moneyEarned.text = "$" + money;
	}

	public void Retry ()
	{
		GameManager.IsPaused = false;
		GameManager.instance.RestartLevel();
	}

	public void Menu ()
	{
		GameManager.IsPaused = false;
		GameManager.instance.GoToMenu();
	}

}
