using UnityEngine;

[System.Serializable]
public class SpawnItem {

	public bool collected = false;

	public GameObject prefab;
	public Vector3 pos;
	public Vector3 scale = new Vector3(1f,1f,1f);

	public GameObject spawnedObject;

	public void SetupSpawnedObject ()
	{
		spawnedObject.transform.localScale = scale;
		Item item = spawnedObject.GetComponent<Item>();
		if (item != null)
			item.onCollect += Collect;
	}

	public void Collect ()
	{
		collected = true;
	}

}
