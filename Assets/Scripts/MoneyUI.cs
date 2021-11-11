using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

	public Text text;

	void Update ()
	{
		text.text = "$" + GameManager.Money.ToString();
    }

}
