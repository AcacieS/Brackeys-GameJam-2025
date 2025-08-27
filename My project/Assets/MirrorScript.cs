using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    private Animator anim;
    private bool canHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void AnimMirror()
    {

    }
    public void SetNotHit()
    {
        canHit = false;
    }
    public void SetHit()
    {
        canHit = true;
    }
    public bool getCanHit()
    {
        return canHit;
    }
}
