using UnityEngine;

public class DestroyNote : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note"||other.tag == "Hole")
        {
            Destroy(other.gameObject);
        }
        
    }
}
