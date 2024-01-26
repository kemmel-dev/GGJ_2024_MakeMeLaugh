using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnDelay = 0.1f;
    [SerializeField] private float maxSpawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnPrefabsRoutine());
    }

    private IEnumerator SpawnPrefabsRoutine()
    {
        while (true)
        {
            SpawnPrefab(); // Spawn an enemy immediately

            // Then wait for a random time interval before spawning the next enemy
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private void SpawnPrefab()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        if (!IsSpawnPointClear(spawnPoint))
        {
            return;
        }

        GameObject spawnedEnemy = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)], spawnPoint.position, spawnPoint.rotation);

        NPCScript npcScript = spawnedEnemy.GetComponent<NPCScript>();
        if (npcScript != null)
        {
            npcScript.SetSpeed(Random.Range(1f, 8f));
        }
    }

    private bool IsSpawnPointClear(Transform spawnPoint)
    {
        Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.position, 0.5f);
        return hitColliders.Length == 0;
    }
}
