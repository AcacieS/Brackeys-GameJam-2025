using UnityEngine;
public class HoleScript : MonoBehaviour
{
    public Vector3 VelocityPerBeat { get; private set; }
    private Vector3 spawnPos;
    [SerializeField] private Transform targetPos;

    private float spawnBeat;   // beat when spawned
    [SerializeField] private int totalBeats = 16; // beats to reach target

    //private Vector3 velocityPerBeat;
     public void SetTransform(Transform pTargetPos )
    {
        targetPos = pTargetPos;
    }

    private void Start()
    {
        spawnPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();

        // constant velocity = distance / time (in beats)
        VelocityPerBeat = (targetPos.position - spawnPos) / totalBeats;
    }
   
    private void Update()
    {
        float songPosInBeats = BeatGame.Current.getSampledTime();
        float beatsPassed = songPosInBeats - spawnBeat;

        // position = start + velocity * beatsPassed
        transform.position = spawnPos + VelocityPerBeat * beatsPassed;
    }

    // private Vector3 spawnPos;
    // [SerializeField] private Transform targetPos;

    // private float spawnBeat;   // beat when spawned
    // [SerializeField] private int totalBeats = 16; // how many beats it takes to travel

    // private void Start()
    // {
    //     spawnPos = transform.position;
    //     spawnBeat = BeatGame.Current.getSampledTime();
    // }
    // public void SetTransform(Transform pTargetPos )
    // {
    //     targetPos = pTargetPos;
    // }

    // private void Update()
    // {
    //     // current song position in beats
    //     float songPosInBeats = BeatGame.Current.getSampledTime();
    //     float beatsPassed = songPosInBeats - spawnBeat;

    //     // progress from 0 to 1 over totalBeats
    //     float t = beatsPassed / totalBeats;

    //     // clamp so it doesn’t overshoot
    //     t = Mathf.Clamp01(t);

    //     // move between spawnPos and targetPos
    //     transform.position = Vector3.Lerp(spawnPos, targetPos.position, t);
    // }
}
// public class HoleScript : MonoBehaviour
// {

//     private Vector3 spawnPos;
//     [SerializeField] private Vector3 targetPos;
//     private float songPosInBeats;
//     private float spawnBeat;   // the beat when this hole spawns
//     //private float targetBeat;   // the beat when it should reach the target

//     // how many beats it takes to move from spawn to target
//     [SerializeField] private int totalBeats = 16;

//     private void Start()
//     {
//         spawnPos = transform.position;
//         spawnBeat = BeatGame.Current.getSampledTime();
//         //targetBeat = BeatGame.Current.getSampledTime() + 16;
//         //set target relative to spawn (example: move right by 16 units)

//         //targetPos = spawnPos + new Vector3(totalBeats, 0f, 0f);

//     }

//     private void Update()
//     {
//         // current song position in beats
//         songPosInBeats = BeatGame.Current.getSampledTime();
//         float beatsPassed = songPosInBeats - spawnBeat;

//         // move directly relative to beats
//         transform.position = spawnPos + new Vector3(beatsPassed, 0f, 0f);
//     }
    
  
// }
