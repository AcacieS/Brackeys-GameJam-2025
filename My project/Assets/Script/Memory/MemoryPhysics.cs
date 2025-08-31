using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public class MemoryPhysics : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField] private float attribute = 0f;

    [Header("Sprites")]
    [SerializeField] private Sprite negativeSprite;
    [SerializeField] private Sprite positiveSprite;
    [SerializeField] private Sprite neutralSprite;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    // Properties so manager can access
    public float Attribute => attribute;
    public Rigidbody2D Rigidbody => rb;
    public Collider2D Collider => col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    private void Update()
    {
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        if (attribute < 0) sr.sprite = negativeSprite;
        else if (attribute > 0) sr.sprite = positiveSprite;
        else sr.sprite = neutralSprite;
    }
}
