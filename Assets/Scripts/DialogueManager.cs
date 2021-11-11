using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	public static DialogueManager instance;

	void Awake ()
	{
		instance = this;
	}

	public Animator dialogueAnimator;
	public Text dialogueText;

	public static bool doIntroDialogue = true;

	[TextArea(3, 10)]
	public string[] gameStartConversation;

	[TextArea(3, 10)]
	public string[] artifactConversation;

	[TextArea(3, 10)]
	public string[] artifactPickupConversation;

	[TextArea(3, 10)]
	public string[] diamondPickupConversation;

	private Queue queue;

	void Start ()
	{
		queue = new Queue();

		if (doIntroDialogue)
		{
			LoadConversation(gameStartConversation);
			doIntroDialogue = false;
		}
	}

	public void LoadConversation (string[] conversation)
	{
		if (!GameManager.IsDialoguePaused)
		{
			dialogueAnimator.SetTrigger("Open");
			GameManager.IsDialoguePaused = true;
		}

		queue.Clear();

		foreach(string dialogue in conversation)
		{
			queue.Enqueue(dialogue);
        }

		DisplayNextDialogue();
	}

	void Update ()
	{
		if (GameManager.IsPaused)
			return;

		if (!GameManager.IsDialoguePaused)
		{
			return;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			DisplayNextDialogue();
		}
		if (Input.GetButtonDown("Submit"))
		{
			StartCoroutine(EndConversation());
        }
	}

	IEnumerator EndConversation ()
	{
		dialogueAnimator.SetTrigger("Close");

		yield return null;

		GameManager.IsDialoguePaused = false;

		queue.Clear();
	}

	private void DisplayNextDialogue ()
	{

		if (queue.Count == 0)
		{
			StartCoroutine(EndConversation());
			return;
		}

		string dialogue = (string)queue.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeDialogue(dialogue));
    }

	IEnumerator TypeDialogue (string dialogue)
	{
		bool playSound = true;
		dialogueText.text = "";
		foreach (char letter in dialogue.ToCharArray())
		{
			dialogueText.text += letter;

			if (playSound)
				AudioManager.instance.Play("KeyPress");

			playSound = !playSound;

			yield return null;
		}
	}

}
