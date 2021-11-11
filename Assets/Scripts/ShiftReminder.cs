using UnityEngine;
using System.Collections;

public class ShiftReminder : MonoBehaviour {

	[TextArea(3, 10)]
	public string[] conversation;

	private bool hasUsedShift = false;

	void Start ()
	{
		StartCoroutine(Remind());
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			hasUsedShift = true;
		}
	}

	IEnumerator Remind ()
	{
		
		while (GameManager.IsDialoguePaused || GameManager.Timer > 105)
		{
			yield return new WaitForSeconds(1f);
		}

		if (!hasUsedShift)
		{
			DialogueManager.instance.LoadConversation(conversation);
		}
	}

}
