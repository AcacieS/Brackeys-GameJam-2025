using Mono.Cecil.Cil;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo stateInfo;
    private bool canHit = false;
    private NoteState currentNoteState = NoteState.Missed;
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
        Debug.Log("canHIt false");
    }
    public void SetHit()
    {
        canHit = true;
        Debug.Log("canHIt true");

    }
    public bool getCanHit()
    {
        Debug.Log("canHit get" + canHit);
        return canHit;
    }
    private void Update()
    {
        if (canHit)
        {
            setNoteState();

        } else {
            setNoteStateMiss();
        }
    }
    public NoteState getNoteState()
    {
        return currentNoteState;
    }
    private void setNoteState()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float clipLength = stateInfo.length;
        Debug.Log("Current anim length: " + clipLength);

        float normalizedTime = stateInfo.normalizedTime; //0 start 1 end

        if (normalizedTime < 0.25)
        {
            currentNoteState = NoteState.Perfect;
        }
        else if (normalizedTime < 0.50)
        {
            currentNoteState = NoteState.Good;
        }
        else
        {
            currentNoteState = NoteState.Normal;
        }
        
    }
    private void setNoteStateMiss()
    {
        currentNoteState = NoteState.Missed;
    }
}
