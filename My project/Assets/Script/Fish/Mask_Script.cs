using UnityEngine;

public class Mask_Script : MonoBehaviour
{
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Anim_Mask()
    {
        anim.Play("mask_beat_anim");
    }
}
