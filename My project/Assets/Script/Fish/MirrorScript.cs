
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    [SerializeField] private Sprite[] leftSides;
    [SerializeField] private Sprite[] rightSides;
    [SerializeField] private Sprite[] frontSides;
    [SerializeField] private Sprite hit;

    int index_side = 0;
    int index_front = 0;
    private SpriteRenderer spriteR;
    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }
    public void ResetIndex()
    {
        index_side = 0;
    }
    public void Anim_Side(bool isLeft)
    {
        if (isLeft)
        {
            spriteR.sprite = leftSides[index_side];
        }
        else
        {
            spriteR.sprite = rightSides[index_side];
        }

        index_side = (index_side + 1) % leftSides.Length;
    }
    public void Anim_Front()
    {
        spriteR.sprite = frontSides[index_front];
        index_front = (index_front + 1) % frontSides.Length;
    }
    public void Anim_Hit()
    {
        spriteR.sprite = hit;
    }
}
