using UnityEngine;

public class FishGame : BeatGame
{
    [Header("Fish Game")]
    [SerializeField] private Animator[] anim_fishes;
    [SerializeField] private MirrorScript mirrorScript;
    private NoteState currentNoteState;
    private int anim_index = 0;
    private bool currentAlreadyHit = false;
    private bool canHit = false;
    private bool isLeft = true;

    public override void StartOverride()
    {
        base.StartOverride();
    }
    public void AnimChoosenMask()
    {
        if (anim_index <= 3)
        {
            mirrorScript.ResetIndex();
            MaskAnim();
        }
        else
        {
            SetNoteState();
            if (anim_index == 6) //sho
            {
                ShowMirror();
                Debug.Log("----------------");
            }
            else if (anim_index == 9)
            {
                canHit = false;
                if (!currentAlreadyHit)
                {
                    NoteMissed();
                    mirrorScript.Anim_Side(isLeft);
                    
                }
                else
                {
                    mirrorScript.Anim_Front();
                }
                currentAlreadyHit = false;
            }
        }
        anim_index = (anim_index + 1) % 10;
    }
    private void MaskAnim()
    {
        if (anim_index <= 3)
        {
            anim_fishes[anim_index].Play("mask_beat_anim");
            mirrorScript.Anim_Front();
        }
    }
    private void SetNoteState()
    {
        canHit = true;
        if (anim_index == 4)
        {
            int isLeftRand = Random.Range(0, 2);
            isLeft = isLeftRand == 0 ? true : false;
        }
        if (anim_index == 4 || anim_index == 8)
        {
            NoteMissed();
            Debug.Log("Normal");
            currentNoteState = NoteState.Normal;
        }
        else if (anim_index == 5 || anim_index == 7)
        {
            Debug.Log("Good");
            currentNoteState = NoteState.Good;
        }
        else if (anim_index == 6)
        { //index = 6
            Debug.Log("Perfect");
            currentNoteState = NoteState.Perfect;
        }
    }
    private void ShowMirror()
    {
        mirrorScript.Anim_Side(isLeft);
    }
    public override void UpdateOverride()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !currentAlreadyHit)
        {
            if (canHit)
            {
                Debug.Log("Got it good");
                NoteHit(currentNoteState);
            }
            else
            {
                Debug.Log("Missed");
                NoteMissed();
            }
            mirrorScript.Anim_Hit();
            currentAlreadyHit = true;
        }

        base.UpdateOverride(); // only if needed
    }
}
