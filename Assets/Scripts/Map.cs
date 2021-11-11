using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Map : MonoBehaviour {

	public float width = 100f;
	public float height = 100f;

	public float hookSpacing = 7f;
	public float itemSpacing = 15f;

	public float minGoldSize = .5f;
	public float maxGoldSize = 4f;

	[Header("Unity Stuff")]
	public GameObject hookPrefab;
	public GameObject goldPrefab;
	public GameObject diamondPrefab;

	public GameObject TVPrefab;
	public GameObject phonePrefab;
	public GameObject tapePrefab;

	public List<SpawnItem> spawnItemList;

	void Start ()
	{
		GenerateMap();

		StartCoroutine(SpawnPrefabsNearby());
	}

	IEnumerator SpawnPrefabsNearby ()
	{
		while (true)
		{
			foreach (SpawnItem item in spawnItemList)
			{
				if (Mathf.Abs(item.pos.y - Player.Position.y) < 15f)
				{
					if (item.spawnedObject == null && !item.collected)
					{
						item.spawnedObject = (GameObject)Instantiate(item.prefab, item.pos, Quaternion.identity, transform);
						item.SetupSpawnedObject();
					}
				}
				else
				{
					if (item.spawnedObject != null)
					{
						Destroy(item.spawnedObject);
					}
				}
			}

			yield return new WaitForSeconds(2f);
		}
	}

	public void GenerateMap ()
	{
		spawnItemList = new List<SpawnItem>();

		GenerateHooks();
		GenerateItems();
	}

	void GenerateItems ()
	{
		float x = 0f;
		while (x < width)
		{
			float y = 0f;
			int timeout = 0;
			while (y < height)
			{
				float multiplier = ((height - (y - 1f)) / height) * 2f;
				float realSpacing;

				if (y < height - 15f)
					realSpacing = itemSpacing * multiplier;
				else
					realSpacing = itemSpacing;

				float xOffset = Random.Range(-itemSpacing / 2f, itemSpacing / 2f);
				float yOffset = Random.Range(-realSpacing / 2f, realSpacing / 2f);

				Vector3 pos = new Vector3(x + xOffset - width / 2f, Mathf.Clamp(-y - yOffset, -Mathf.Infinity, 1f), 0f);
				RegisterSpawnItem(pos, y);

				y += realSpacing / 2f;

				timeout++;
				if (timeout > 10000)
				{
					Debug.LogError("TIMEOUT");
					return;
				}
			}

			x += itemSpacing / 2f;
		}
	}

	void RegisterSpawnItem (Vector3 pos, float depth)
	{
		SpawnItem spawnItem = new SpawnItem();
		spawnItem.pos = pos;

		float roll = Random.Range(0f, 1f);

		if (roll > .915f)
		{

			if (depth < 10f)
				spawnItem.prefab = goldPrefab;
			else
				spawnItem.prefab = GetRandomArtifact();

		} else if (roll > .8f)
		{
			spawnItem.prefab = diamondPrefab;
		} else
		{
			spawnItem.prefab = goldPrefab;
		}

		//GameObject item = (GameObject)Instantiate(prefabToSpawn, pos, Quaternion.identity, transform);

		if (spawnItem.prefab == goldPrefab)
		{
			spawnItem.scale = Vector3.one * Random.Range(minGoldSize, maxGoldSize);
		}

		spawnItemList.Add(spawnItem);
	}

	GameObject GetRandomArtifact ()
	{
		int number = Random.Range(0, 3);
		switch (number)
		{
			case 0:
				return TVPrefab;
			case 1:
				return phonePrefab;
			case 2:
				return tapePrefab;
		}

		return null;
	}

	void GenerateHooks()
	{
		float x = 0f;
		while (x < width)
		{
			float y = 0f;
			while (y < height)
			{
				float xOffset = Random.Range(-hookSpacing / 2f, hookSpacing / 2f);
				float yOffset = Random.Range(-hookSpacing / 2f, hookSpacing / 2f);

				Vector3 pos = new Vector3(x + xOffset - width / 2f, Mathf.Clamp(-y - yOffset, -Mathf.Infinity, 1f), 0f);

				bool tooClose = false;
				foreach (SpawnItem item in spawnItemList)
				{
					if (Vector2.Distance(pos, item.pos) < 1f)
						tooClose = true;
				}

				if (!tooClose)
					RegisterHookAsSpawnItem(pos);

				y += hookSpacing / 2f;
			}

			x += hookSpacing / 2f;
		}
	}

	void RegisterHookAsSpawnItem (Vector3 pos)
	{
		SpawnItem item = new SpawnItem();
		item.prefab = hookPrefab;
		item.pos = pos;
		item.scale = Vector3.one * 1.2f;

		spawnItemList.Add(item);
	}

}
