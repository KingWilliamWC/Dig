using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static bool isAiming = true;
	public static Vector3 Position { get { return instance.transform.position; } }

	private static Player instance;

	public Transform harpoonTransform;
	private Harpoon harpoon;
	public float harpoonRotSpeed = 1f;
	public float harpoonSmoothSpeed = 5f;

	public float playerMoveSpeed = 5f;
	public float range = 7f;

	private Transform attachedObj;

	private float previousRot = 0f;

	void Awake ()
	{
		instance = this;
    }

	void Start ()
	{
		harpoon = harpoonTransform.GetComponent<Harpoon>();
		harpoon.onAttach = AttachTo;
	}

	void Update ()
	{
		if (GameManager.IsPaused || GameManager.IsDialoguePaused)
			return;

		if (isAiming)
		{
			AimHarpoon();
		}
	}

	void Shoot ()
	{
		isAiming = false;

		AudioManager.instance.Play("FlyingRope");

		RaycastHit2D hit;
		hit = Physics2D.Raycast(harpoonTransform.position, -harpoonTransform.up, range);
		if (hit.collider != null)
		{
			Debug.Log("We hit " + hit.collider.name);

			if (hit.collider.tag == "Attachable")
			{
				harpoon.MoveTo_Attachable(hit.point, hit.transform);
			} else if (hit.collider.tag == "Collectable")
			{
				harpoon.MoveTo_Collectable(hit.point, hit.transform);
			}
			else
			{
				harpoon.MoveTo(hit.point);
			}

		} else
		{
			harpoon.MoveTo(harpoonTransform.position - harpoonTransform.up * range);
		}

	}

	void AimHarpoon()
	{
		float detailedAimSlowEffect = 1f;

		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			detailedAimSlowEffect = 0.2f;
        }

		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - harpoonTransform.position;
		difference.Normalize();
		float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		//harpoonTransform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 90);
		harpoonTransform.rotation = Quaternion.Lerp(harpoonTransform.rotation, Quaternion.Euler(0f, 0f, rotation_z + 90), Time.deltaTime * detailedAimSlowEffect * harpoonRotSpeed);
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	public void AttachTo (Transform obj)
	{
		if (attachedObj != null)
			attachedObj.gameObject.layer = LayerMask.NameToLayer(LayerManager.attachableLayerName);

		attachedObj = obj;
		attachedObj.gameObject.layer = LayerMask.NameToLayer(LayerManager.ignoreRaycastLayerName);

		StartCoroutine(SmoothlyAttachTo(attachedObj));
	}

	IEnumerator SmoothlyAttachTo (Transform obj)
	{
		AudioManager.instance.Play("ReturningRope");

		Vector3 dir = (obj.position - transform.position).normalized;
		while (Vector3.Distance(obj.position, transform.position) > 0.1f)
		{
			transform.Translate(dir * Time.deltaTime * playerMoveSpeed);
			yield return null;
		}

		transform.position = obj.position;

		AudioManager.instance.Stop("ReturningRope");

		harpoon.Return();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, -harpoonTransform.up * range);
	}

}
