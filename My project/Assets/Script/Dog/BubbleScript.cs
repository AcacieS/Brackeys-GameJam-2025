using UnityEngine;

public class BubbleScript : MonoBehaviour
{

    private Transform targetPos;
    private Transform targetGood;
    private Transform targetBad;
    [SerializeField] private int[] totalBeats;
    private int totalBeat = 5; // beats to reach target
    public Vector3 velocityPerBeat;
    private Vector3 spawnPos;

    [Header("Property")]
    [SerializeField] private SpriteRenderer obj_renderer;
    private BubbleSO bubble;

    private bool hasClick = false;
    private float spawnBeat;   // beat when spawned


    //private Vector3 velocityPerBeat;
    public void SetTargets(Transform pTargetPos, Transform pTargetGood, Transform pTargetBad)
    {
        targetPos = pTargetPos;
        targetGood = pTargetGood;
        targetBad = pTargetBad;
    }
    public bool getHasClick()
    {
        return hasClick;
    }
    public void setHasClick(bool pHasClick)
    {
        hasClick = pHasClick;
    }
    public void ResetGoal()
    {
        Debug.Log("Should have changed");
        if (bubble.type == BubbleType.Good)
        {
            targetPos = targetGood;
        }
        else
        {
            targetPos = targetBad;
        }
        totalBeat = totalBeat / 2;
        spawnPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();
        velocityPerBeat = (targetPos.position - spawnPos) / totalBeat;
    }
    public void SetBubble(BubbleSO pBubble)
    {
        bubble = pBubble;
    }

    private void Start()
    {
        obj_renderer.sprite = bubble.sprite;

        int index_beat = Random.Range(0, totalBeats.Length);
        totalBeat = totalBeats[index_beat];

        spawnPos = transform.position;
        spawnBeat = BeatGame.Current.getSampledTime();
        velocityPerBeat = (targetPos.position - spawnPos) / totalBeat;
    }

    private void Update()
    {
        float songPosInBeats = BeatGame.Current.getSampledTime();
        float beatsPassed = songPosInBeats - spawnBeat;

        transform.position = spawnPos + velocityPerBeat * beatsPassed;
    }
    public BubbleSO GetBubbleSO()
    {
        return bubble;
    }
    public void DestroyBubble()
    {
        Destroy(gameObject);
    }
}
