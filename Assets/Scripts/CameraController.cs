using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform player;
	public Transform harpoon;
	public float smoothSpeed = 20f;

	private Vector3 previousPos;

	void Update ()
	{
		Transform target;

		if (Player.isAiming)
			target = player;
		else
			target = harpoon;

		Vector3 targetPos = Vector3.Lerp(previousPos, new Vector3(target.position.x, target.position.y, -10), Time.deltaTime * smoothSpeed);
		transform.position = targetPos;

		previousPos = targetPos;
	}

}
