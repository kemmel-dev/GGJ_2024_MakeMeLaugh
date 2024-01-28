using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnDelay = 2.0f; // Increased initial minimum spawn delay
    [SerializeField] private float maxSpawnDelay = 3.0f; // Increased initial maximum spawn delay
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float speedIncreaseFactor = 0.3f; // Speed increase per minute
    [SerializeField] private float spawnRateIncreaseFactor = 0.2f; // Spawn rate increase per minute
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

        // Increase speed and decrease spawn delay every 7 seconds
        if (elapsedTime >= 7f)
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

        // Generate a random threshold for prefab selection between 65 and 85
        int spawnThreshold = Random.Range(63, 76); // 86 is exclusive, so the range is effectively 65-85

        // Generate a random number for determining which prefab to spawn
        int randomChance = Random.Range(0, 100);
        GameObject prefabToSpawn;

        // Compare the randomChance with the dynamic threshold
        if (randomChance <= spawnThreshold)
        {
            prefabToSpawn = prefabsToSpawn[0];
        }
        else
        {
            // Make sure there is a second prefab to spawn
            if (prefabsToSpawn.Length > 1)
            {
                prefabToSpawn = prefabsToSpawn[1];
            }
            else
            {
                Debug.LogError("Second prefab not defined.");
                return;
            }
        }

        GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        NPCScript npcScript = spawnedEnemy.GetComponent<NPCScript>();
        if (npcScript != null)
        {
            npcScript.SetSpeed(Random.Range(1.8f, maxSpeed));

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
