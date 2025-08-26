using UnityEngine;

public class PulseToBeat : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string beat_anim = "beat_anim";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pulse()
    {
        Debug.Log("Pulse");
        anim.Play(beat_anim);
    }
}
