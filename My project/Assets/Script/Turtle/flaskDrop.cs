using UnityEngine;

public class flaskDrop : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] private FlaskSO[] currentFlaskSOs;
    private FlaskSO currentFlaskSO;

    [Header("Position")]
    [SerializeField] private Transform targetPos;
    private Vector3 spawnPos;

    public Vector3 VelocityPerBeat { get; private set; }

    private float spawnBeat;   // beat when spawned
    [SerializeField] private int totalBeats; // beats to reach target
    [SerializeField] private int[] dropBeats;
    private bool isCatched = false;


    
    public FlaskSO GetFlaskSO()
    {
        return currentFlaskSO;
    }
    

    //private Vector3 velocityPerBeat;
    public void SetTransform(Transform pTargetPos)
    {
        targetPos = pTargetPos;
    }
    public void SetSpawn(Vector3 pSpawnPos)
    {
        spawnPos = pSpawnPos;
        spawnBeat = BeatGame.Current.getSampledTime();
    }
    public void SetFlaskSO(FlaskSO flaskSO)
    {
        currentFlaskSO = flaskSO;
    }

    private void Start()
    {
        //get random come speed;
        GetComponent<SpriteRenderer>().sprite = currentFlaskSO.flaskImg;

        // totalBeats = dropBeats[index];
        totalBeats = currentFlaskSO.lengths_drop;

        spawnPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();

        // constant velocity = distance / time (in beats)
        VelocityPerBeat = (targetPos.position - spawnPos) / totalBeats;
    }

    private void Update()
    {
        if (!isCatched)
        {
            float songPosInBeats = BeatGame.Current.getSampledTime();
            float beatsPassed = songPosInBeats - spawnBeat;

            // position = start + velocity * beatsPassed
            transform.position = spawnPos + VelocityPerBeat * beatsPassed;
        }

    }
    public void SetIsCatched(bool pIsCatched)
    {
        isCatched = pIsCatched;
    }

}
