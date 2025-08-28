using UnityEngine;

public class Deletemissedbeat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Note") {
            Destroy(other.gameObject);
        }
    }
}
