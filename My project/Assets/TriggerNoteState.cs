using UnityEngine;

public class TriggerNoteState : MonoBehaviour
{
    [SerializeField] private SpriteRenderer imageRenderer;
    [SerializeField] private Sprite sprite_turtle;
    [SerializeField] private Sprite normal_sprite;
    [SerializeField] private string state;
    [SerializeField] private TurtleGame turtle;
    [SerializeField] private bool isMiss = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect which trigger was hit by checking the tag or name
        if (other.CompareTag("flask"))
        {
            imageRenderer.sprite = sprite_turtle;
            turtle.getCurrentState(state);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isMiss && other.CompareTag("flask"))
        {
            imageRenderer.sprite = sprite_turtle;
            turtle.getCurrentState("Miss");
        }
    }
}
