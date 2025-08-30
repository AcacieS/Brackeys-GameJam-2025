using UnityEngine;

public class TriggerImageChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer imageRenderer; // The image to change
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;
    [SerializeField] private Sprite sprite4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect which trigger was hit by checking the tag or name
        if (other.CompareTag("Trigger1"))
        {
            imageRenderer.sprite = sprite1;
        }
        else if (other.CompareTag("Trigger2"))
        {
            imageRenderer.sprite = sprite2;
        }
        else if (other.CompareTag("Trigger3"))
        {
            imageRenderer.sprite = sprite3;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trigger3"))
        {
            imageRenderer.sprite = sprite4;
        }
    }
}