using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnDelay = 1.0f; // Increased initial minimum spawn delay
    [SerializeField] private float maxSpawnDelay = 2.0f; // Increased initial maximum spawn delay
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float speedIncreaseFactor = 0.5f; // Speed increase per minute
    [SerializeField] private float spawnRateIncreaseFactor = 0.1f; // Spawn rate increase per minute
    [SerializeField] private float maxSpeedLimit = 15f; // Maximum speed limit
    [SerializeField] private float minSpawnDelayLimit = 0.05f; // Minimum spawn delay limit
                                                               // Minimum spawn delay limit

    private float elapsedTime = 0f;

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

            UpdateDifficulty(); // Update the game difficulty over time
        }
    }

    private void UpdateDifficulty()
    {
        elapsedTime += Time.deltaTime;

        // Increase speed every minute
        if (elapsedTime >= 60f)
        {
            maxSpeed = Mathf.Min(maxSpeed + speedIncreaseFactor, maxSpeedLimit);
            maxSpawnDelay = Mathf.Max(maxSpawnDelay - spawnRateIncreaseFactor, minSpawnDelayLimit);
            elapsedTime = 0f;
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
            npcScript.SetSpeed(Random.Range(1f, maxSpeed));

            // Determine the direction based on the spawn point's x-coordinate
            bool shouldMoveRight = spawnPoint.position.x < 0;
            npcScript.SetDirection(shouldMoveRight);

            // Flip the sprite if spawned on the left
            if (shouldMoveRight)
            {
                spawnedEnemy.transform.localScale = new Vector3(-1 * Mathf.Abs(spawnedEnemy.transform.localScale.x), spawnedEnemy.transform.localScale.y, spawnedEnemy.transform.localScale.z);
            }
        }
    }

    private bool IsSpawnPointClear(Transform spawnPoint)
    {
        Collider[] hitColliders = Physics.OverlapSphere(spawnPoint.position, 0.5f);
        return hitColliders.Length == 0;
    }
}
