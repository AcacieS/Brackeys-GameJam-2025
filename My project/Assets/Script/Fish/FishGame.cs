using UnityEngine;

public class FishGame : BeatGame
{
    [Header("Fish Game")]
    [SerializeField] private Animator[] anim_fishes;
    [SerializeField] private MirrorScript mirrorScript;
    private int anim_index = 0;
    //private bool toHit;
    private bool isStart = true;
    private bool currentAlreadyHit = false;

    public override void StartOverride()
    {
        base.StartOverride();
    }
    public void AnimChoosenMask()
    {
        //Mirror Animation
        if (anim_index == anim_fishes.Length - 1)
        {
            ShowMirror();
        }
        else //Mask Animation
        {
            if (anim_index == 0 && !isStart && !currentAlreadyHit)
            {
                //Show not good
                NoteMissed();
            }
            else if (anim_index == 0 && !isStart)
            {
                anim_fishes[anim_index].Play("mirror_normal_anim");
            }

            if (anim_index == 0)
            {
                currentAlreadyHit = false;
            }
            else
            {

                anim_fishes[anim_index].Play("mirror_normal_anim");
            }

            if (isStart)
            {
                isStart = false;
            }

            anim_fishes[anim_index].Play("mask_beat_anim");
            anim_index = (anim_index + 1) % anim_fishes.Length;

        }
    }
    private void ShowMirror()
    {
        anim_fishes[anim_index].Play("mirror_showFishBlue_anim");
        anim_index = (anim_index + 1) % anim_fishes.Length;

    }
     public override void UpdateOverride()
    {
       if (Input.GetKeyDown(KeyCode.Space) && !currentAlreadyHit)
        {
            if (mirrorScript.getCanHit())
            {
                Debug.Log("Got it good");
                NoteHit();
            }
            else
            {
                NoteMissed();
            }
            anim_fishes[anim_fishes.Length - 1].Play("mirror_hit_anim");
            currentAlreadyHit = true;
        }

        base.UpdateOverride(); // only if needed
    }
}
