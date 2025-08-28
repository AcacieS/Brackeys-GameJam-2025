using UnityEngine;

public class FishGame : BeatGame
{
    [Header("Fish Game")]
    [SerializeField] private Animator[] anim_fishes;
    [SerializeField] private MirrorScript mirrorScript;
    private NoteState currentNoteState;
    private int anim_index = 0;
    private bool isStart = true;
    private bool currentAlreadyHit = false;
    private bool canHit = false;

    public override void StartOverride()
    {
        base.StartOverride();
    }
    public void AnimChoosenMask()
    {
        if (anim_index <= 3)
        {
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
                if (!currentAlreadyHit) NoteMissed();

                anim_fishes[4].Play("mirror_normal_anim");
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
        }
    }
    private void SetNoteState()
    {
        canHit = true;
        if (anim_index == 4 || anim_index == 8)
        {
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
        anim_fishes[4].Play("mirror_showFishBlue_anim");

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
            anim_fishes[anim_fishes.Length - 1].GetComponent<Animator>().Play("mirror_hit_anim");
            currentAlreadyHit = true;
        }

        base.UpdateOverride(); // only if needed
    }
     // //Mirror Animation
        // if (anim_index == anim_fishes.Length - 1)
        // {
        //     ShowMirror();
        // }
        // else //Mask Animation
        // {
        //     if (anim_index == 0 && !isStart && !currentAlreadyHit)
        //     {
        //         //Show not good
        //         NoteMissed();
        //     }
        //     else if (anim_index == 0 && !isStart)
        //     {
        //         anim_fishes[anim_index].GetComponent<Animator>().Play("mirror_normal_anim");
        //     }

        //     if (anim_index == 0)
        //     {
        //         currentAlreadyHit = false;
        //     }
        //     else
        //     {

        //         anim_fishes[anim_index].GetComponent<Animator>().Play("mirror_normal_anim");
        //     }

        //     if (isStart)
        //     {
        //         isStart = false;
        //     }

        //     anim_fishes[anim_index].GetComponent<Animator>().Play("mask_beat_anim");
        //     anim_index = (anim_index + 1) % anim_fishes.Length;

        // }
}
