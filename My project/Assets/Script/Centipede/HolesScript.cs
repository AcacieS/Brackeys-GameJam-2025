using UnityEngine;

public class HolesScript : MonoBehaviour
{
    [SerializeField] private SpawnNote spawn;

    private Vector3 velocityPerBeat;
    private Vector3 startPos;
    private float spawnBeat;

    private void Start()
    {
        velocityPerBeat = spawn.Speed(); 
        startPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();
    }
    public void SetScript(SpawnNote pSpawn)
    {
        spawn = pSpawn;
    }

    private void Update()
    {
        float songPosInBeats = BeatGame.Current.getSampledTime();
        float beatsPassed = songPosInBeats - spawnBeat;

        transform.position = startPos + velocityPerBeat * beatsPassed;
    }

}
