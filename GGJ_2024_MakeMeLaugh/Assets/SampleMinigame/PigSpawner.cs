using System.Collections;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
	public static PigSpawner Instance { get; private set; }

	public PigBehaviour PigPrefab;
	public Transform SpawnPosition;
	public float SpawnDelay = 0.5f;
	public int PigAmount = 0;
	public int MaxPigAmount = 10;
	public float XRange = 3;
	public float YRange = 3;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;
	}


	private void Start()
	{
		for (int i = 0; i < MaxPigAmount; i++)
		{
			CreatePig();
		}

		StartCoroutine(SpawnPig());
	}

	private void CreatePig()
	{
		var pig = Instantiate(PigPrefab, SpawnPosition.position + new Vector3(Random.Range(-XRange / 2f, XRange / 2f), 0, Random.Range(-YRange / 2f, YRange / 2f)), Quaternion.identity);
		pig.pigMiniGameController = GetComponent<PigMiniGameController>();
		PigAmount++;
	}

	private IEnumerator SpawnPig()
	{
		while (true)
		{
			if (PigAmount < MaxPigAmount)
			{
				CreatePig();
			}
			yield return new WaitForSeconds(SpawnDelay);
		}
	}

	public void RemovePig()
	{
		PigAmount--;
	}

	private void OnDestroy()
	{
		Instance = null;
	}
}
