using UnityEngine;

public class MemoryMovemet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3f; // move speed
    private Vector3 targetPosition = new Vector3(4f, 2f, -0.1f); // where the player is

    public void Update()
    {
        // move from current position toward target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // if reached target (touch player area), destroy
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
