using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoryManager : MonoBehaviour
{
    [Header("Prefab & Object Tracking")]
    [SerializeField] private PhysicsObject physicsObjectPrefab;
    public List<PhysicsObject> activeObjects = new List<PhysicsObject>();

    [Header("Spawn & Despawn")]
    [SerializeField] private float spawnXOffset = 0f;
    [SerializeField] private float despawnXThreshold = 20f;

    [Header("Selection Settings")]
    [SerializeField] private float closestXOffset = 0f;

    [Header("Initial Velocity")]
    [SerializeField] private Vector2 initialVelocity = Vector2.zero;
    [SerializeField] private float randomYVelocityRange = 0f;

    [Header("Input Settings")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private float forceStrength = 5f;

    [Header("Triggers")]
    [SerializeField] private Collider2D negativeTrigger;
    [SerializeField] private Collider2D positiveTrigger;

    [Header("Events")]
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    [Header("Spawn Events")]
    [Tooltip("Call this UnityEvent from UI buttons or other scripts to spawn objects.")]
    public UnityEvent onSpawnRequest;

    [Header("Debug / Demo")]
    [SerializeField] private bool enableDemoSpawn = true;
    [SerializeField] private KeyCode spawnKey = KeyCode.Space;

    private void Awake()
    {
        // Hook the spawn request event to SpawnObject
        if (onSpawnRequest == null)
            onSpawnRequest = new UnityEvent();

        onSpawnRequest.AddListener(SpawnObject);
    }

    // --- PUBLIC METHODS ---
    public void SpawnObject()
    {
        Vector3 spawnPos = transform.position + new Vector3(spawnXOffset, 0f, 0f);
        PhysicsObject newObj = Instantiate(physicsObjectPrefab, spawnPos, Quaternion.identity);

        // Assign velocity
        float randY = Random.Range(-randomYVelocityRange, randomYVelocityRange);
        newObj.Rigidbody.linearVelocity = initialVelocity + new Vector2(0f, randY);

        activeObjects.Add(newObj);
    }

    // --- UPDATE LOOP ---
    private void Update()
    {
        // Demo spawn
        if (enableDemoSpawn && Input.GetKeyDown(spawnKey))
        {
            SpawnObject();
        }

        if (activeObjects.Count == 0) return;

        // 1. Find closest object
        float refX = transform.position.x + closestXOffset;
        PhysicsObject target = FindClosestToX(refX);

        // 2. Apply input forces
        if (target != null)
        {
            if (Input.GetKey(upKey))
                target.Rigidbody.AddForce(Vector2.up * forceStrength * Time.deltaTime, ForceMode2D.Force);
            if (Input.GetKey(downKey))
                target.Rigidbody.AddForce(Vector2.down * forceStrength * Time.deltaTime, ForceMode2D.Force);
        }

        // 3. Collision checks + despawn logic
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            PhysicsObject obj = activeObjects[i];
            if (obj == null)
            {
                activeObjects.RemoveAt(i);
                continue;
            }

            if (negativeTrigger != null && obj.Collider.IsTouching(negativeTrigger))
            {
                if (obj.Attribute < 0) onSuccess?.Invoke();
                else onFail?.Invoke();
                Despawn(obj);
                continue;
            }

            if (positiveTrigger != null && obj.Collider.IsTouching(positiveTrigger))
            {
                if (obj.Attribute > 0) onSuccess?.Invoke();
                else onFail?.Invoke();
                Despawn(obj);
                continue;
            }

            // 4. Despawn by X distance
            if (Mathf.Abs(obj.transform.position.x - transform.position.x) > despawnXThreshold)
            {
                Despawn(obj);
            }
        }
    }

    // --- HELPERS ---
    public PhysicsObject FindClosestToX(float refX)
    {
        float minDist = float.MaxValue;
        PhysicsObject closest = null;

        foreach (var obj in activeObjects)
        {
            if (obj == null) continue;
            float dist = Mathf.Abs(obj.transform.position.x - refX);
            if (dist < minDist)
            {
                minDist = dist;
                closest = obj;
            }
        }

        return closest;
    }

    private void Despawn(PhysicsObject obj)
    {
        activeObjects.Remove(obj);
        Destroy(obj.gameObject);
    }
}
 