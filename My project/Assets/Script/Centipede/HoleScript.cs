using UnityEngine;

public class HoleScript : MonoBehaviour
{

    private Vector3 spawnPos;
    private Vector3 targetPos;
    private float songPosInBeats;
    private float spawnBeat;   // the beat when this hole spawns
    //private float targetBeat;   // the beat when it should reach the target

    // how many beats it takes to move from spawn to target
    [SerializeField] private int totalBeats = 16;

    private void Start()
    {
        spawnPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();
        //targetBeat = BeatGame.Current.getSampledTime() + 16;
        // set target relative to spawn (example: move right by 16 units)
        targetPos = spawnPos + new Vector3(totalBeats, 0f, 0f);

    }

    private void Update()
    {
        // current song position in beats
        songPosInBeats = BeatGame.Current.getSampledTime();

        // // normalize to 0..1 progress across totalBeats
        // float progress = (songPosInBeats - spawnBeat) / (targetBeat - spawnBeat);//Mathf.Clamp01((songPosInBeats - spawnBeat) / (targetBeat - spawnBeat));

        // Debug.Log("progress: "+progress);
        // // move hole between spawn and target
        // transform.position = Vector3.Lerp(spawnPos, targetPos, progress);
        float beatsPassed = songPosInBeats - spawnBeat;

        // move directly relative to beats
        transform.position = spawnPos + new Vector3(beatsPassed, 0f, 0f);
    }
    
  
}
