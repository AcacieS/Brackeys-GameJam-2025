using UnityEngine;

public class CentipedeGame : BeatGame
{
    [SerializeField] private SpawnNote spawnNote;
    public override void setHard()
    {
        index_wait = 1;
        base.setHard();

    }
    public override void eachInterval() {
        spawnNote.ResetCurrentPatternNote();
    }
    
}
