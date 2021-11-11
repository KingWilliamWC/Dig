using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyChangeUI : MonoBehaviour {

	public Text amountText;
	public Animator animator;

	// Use this for initialization
	void Start () {
		GameManager.instance.onAddMoney += OnAddMoney;
	}
	
	void OnAddMoney (int amount) {
		amountText.text = "+" + amount.ToString();
		animator.SetTrigger("AddMoney");
	}
}
