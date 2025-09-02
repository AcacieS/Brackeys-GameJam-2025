using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bubble")
        {
            Destroy(other.gameObject);
        }
        
    }
}
