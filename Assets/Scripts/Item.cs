using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public float weight { get { return transform.localScale.magnitude; } }
	public int worth { get { return (int)(100f * (float)rarity * weight); } }

	public int rarity = 1;

	public int time = 4;

	public GameObject collectedParticles;

	public delegate void OnCollect();
	public OnCollect onCollect;

	private bool isVisible = false;

	private Collider2D col;

	private static bool hasPickedUpArtifact = false;
	private static bool hasPickedUpDiamond = false;
	private static bool hasSeenArtifact = false;

	void Start ()
	{
		col = GetComponent<Collider2D>();
        StartCoroutine(CheckForVisibility());
    }

	public void Collect ()
	{
		AudioManager.instance.Play("Cash");

		if (rarity > 15)
		{
			AudioManager.instance.Play("ArtifactPickup");
			if (!hasPickedUpArtifact)
			{
				DialogueManager.instance.LoadConversation(DialogueManager.instance.artifactPickupConversation);
				hasPickedUpArtifact = true;
			}
		} else if (rarity > 5)
		{
			if (!hasPickedUpDiamond)
			{
				DialogueManager.instance.LoadConversation(DialogueManager.instance.diamondPickupConversation);
				hasPickedUpDiamond = true;
			}
		}

		Debug.Log("ITEM GAINED! Worth: " + worth);
		GameManager.instance.AddMoney(worth);

		GameManager.instance.AddTime(time);

		if (onCollect != null)
			onCollect.Invoke();

		GameObject effect = (GameObject)Instantiate(collectedParticles, transform.position, Quaternion.identity);
		effect.transform.localScale = transform.localScale;
		Destroy(effect, 5f);

		Destroy(gameObject);
	}

	IEnumerator CheckForVisibility ()
	{
		while (true)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
			{
				if (isVisible == false)
				{
					isVisible = true;
					BecameVisible();
				}
			}

			yield return new WaitForSeconds(1f);
		}
	}

	void BecameVisible ()
	{
		if (rarity > 15)
		{
			if (!hasSeenArtifact)
			{
				DialogueManager.instance.LoadConversation(DialogueManager.instance.artifactConversation);
				hasSeenArtifact = true;
			}
		}
	}

}
