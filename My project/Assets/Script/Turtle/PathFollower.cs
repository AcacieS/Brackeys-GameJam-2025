using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints;   
    public float speed = 2f;
    public float reachDistance = 0.1f; 
    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        
        Transform target = waypoints[currentWaypoint];
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        
        if (Vector2.Distance(transform.position, target.position) < reachDistance)
        {
            currentWaypoint++;

           
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
