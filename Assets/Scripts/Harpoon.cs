using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Harpoon : MonoBehaviour {

	public float moveSpeed = 10f;

	public delegate void OnAttachDelegate(Transform obj);
	public OnAttachDelegate onAttach;

	private delegate void OnMoveCompleteDelegate();
	private delegate void OnReturnedDelegate();

	private Transform attachableObj;
	private Transform item;

	public Transform harpoonEnd;

	private LineRenderer lr;

	void Start ()
	{
		lr = GetComponent<LineRenderer>();
	}

	void Update ()
	{
		lr.SetPosition(0, Player.Position);
		lr.SetPosition(1, harpoonEnd.position);
	}

	public void MoveTo (Vector3 pos)
	{
		StartCoroutine(SmoothlyMoveTo(pos, Return));
	}

	public void MoveTo_Collectable(Vector3 pos, Transform item)
	{
		this.item = item;
		StartCoroutine(SmoothlyMoveTo(pos, ReturnWithItem));
	}

	public void MoveTo_Attachable (Vector3 pos, Transform obj)
	{
		attachableObj = obj;
		StartCoroutine(SmoothlyMoveTo(pos, AttachTo));
	}

	void AttachTo ()
	{
		AudioManager.instance.Play("HitSomething");

		if (onAttach != null)
			onAttach.Invoke(attachableObj);
    }

	IEnumerator SmoothlyMoveTo (Vector3 pos, OnMoveCompleteDelegate onMoveComplete)
	{
		while (Vector3.Distance(transform.position, pos) > 0.1f)
		{
			Vector3 dir = (pos - transform.position).normalized;
			transform.Translate(dir * Time.deltaTime * moveSpeed, Space.World);

			yield return null;
        }

		transform.position = pos;

		AudioManager.instance.Stop("FlyingRope");

		if (onMoveComplete != null)
			onMoveComplete.Invoke();
	}

	void ReturnWithItem()
	{
		AudioManager.instance.Play("HitSomethingGood");

		item.SetParent(transform);
		float speedReduction = item.GetComponent<Item>().weight;
		StartCoroutine(SmoothlyReturn(OnReturnedWithItem, speedReduction));
	}

	void OnReturnedWithItem ()
	{
		item.GetComponent<Item>().Collect();
	}

	public void Return ()
	{
		StartCoroutine(SmoothlyReturn(null, 1f));
		
	}

	IEnumerator SmoothlyReturn (OnReturnedDelegate onReturned, float speedReduction)
	{
		AudioManager.instance.Play("ReturningRope");

		Vector3 targetPos = Player.Position - transform.up * 0.4f;

		while (Vector3.Distance(transform.position, targetPos) > 0.1f)
		{
			Vector3 dir = (targetPos - transform.position).normalized;
			transform.Translate(dir * Time.deltaTime * moveSpeed / speedReduction, Space.World);

			yield return null;
		}

		transform.position = targetPos;

		AudioManager.instance.Stop("ReturningRope");

		if (onReturned != null)
			onReturned.Invoke();

		Player.isAiming = true;
	}

}
