using UnityEngine;

public class Frog_Script : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Stamp()
    {
        anim.Play("frog_stamp");
    }
}
