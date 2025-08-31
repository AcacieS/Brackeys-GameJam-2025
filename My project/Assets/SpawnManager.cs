using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Memory Prefabs")]
    public GameObject goodMemoryPrefab;
    public GameObject badMemoryPrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint;     // Memory initial position (-7,2,-0.1)
    public float spawnInterval = 1.5f;  // Time between spawns
    public float minInterval = 0.5f;    // Optional: randomize spawn interval
    public float maxInterval = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnMemory();
            timer = Random.Range(minInterval, maxInterval); // random next spawn
        }
    }

    void SpawnMemory()
    {
        if (goodMemoryPrefab == null || badMemoryPrefab == null || spawnPoint == null)
            return;

        // Randomly choose Good or Bad Memory
        GameObject prefabToSpawn = Random.value < 0.5f ? goodMemoryPrefab : badMemoryPrefab;

        // Instantiate at spawnPoint
        Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }
}