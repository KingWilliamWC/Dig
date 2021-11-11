using UnityEngine;
using UnityEngine.UI;

public class PlayerTimeChangeUI : MonoBehaviour {

	public Text amountText;
	public Animator animator;

	// Use this for initialization
	void Start () {
		GameManager.instance.onAddTime += OnAddTime;
	}
	
	void OnAddTime (int amount) {
		Debug.Log("TIME: " + amount);

		amountText.text = "+" + amount.ToString() + "<i>s</i>";
		animator.SetTrigger("AddTime");
	}
}
