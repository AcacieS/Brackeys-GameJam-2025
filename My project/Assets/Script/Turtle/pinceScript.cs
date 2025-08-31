
using Unity.Mathematics;
using UnityEngine;

public class pinceScript : MonoBehaviour
{
    private bool isOpen = true;
    [SerializeField] private DetectFlask detectFlask;
    [SerializeField] private Sprite openPince;
    [SerializeField] private Sprite closedPince;
    [SerializeField] Transform placeFlask;
    private SpriteRenderer spriteR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        ChangeSpriteOpenNot();
    }

    // -------------------------------------------------------- FIRST STATE CATCH ----------------------------------------------------------
    public void SetIsOpen(bool pIsOpen)
    {
        isOpen = pIsOpen;
        ChangeSpriteOpenNot();
    }

    public void ChangeSpriteOpenNot()
    {
        if (isOpen)
        {
            spriteR.sprite = openPince;
        }
        else
        {
            spriteR.sprite = closedPince;
        }

    }
    public void AddFlask()
    {

        GameObject currentFlask = detectFlask.getCurrentFlask();
        if (currentFlask != null)
        {
            currentFlask.GetComponent<flaskDrop>().SetIsCatched(true);
            currentFlask.transform.SetParent(this.transform);
            currentFlask.transform.localPosition = placeFlask.localPosition;
            StartHold();
        }
        else
        {
            Debug.Log("Shouldn't flask be null");
        }

    }

    // -------------------------------------------------------- SECOND STATE HOLD ----------------------------------------------------------

    [Header("SECOND STATE HOLD")]
    [SerializeField] private TurtleGame turtle;
    [Header("Position")]
    [SerializeField] private Transform targetPos;
    private Vector3 spawnPos;

    public Vector3 VelocityPerBeat { get; private set; }
    [SerializeField] private int returnBeat = 2;

    private float spawnBeat;   // beat when spawned
    [SerializeField] private int totalBeats; // beats to reach target
    private Vector3 originalPos;
    private Vector3 returnStartPos;
    private float returnStartBeat;      // beat time at start of return
    private bool returnStart = true;
    private bool holdingPince = false;
    [SerializeField] private Transform endTarget;

    private void StartHold()
    {
        totalBeats = detectFlask.getCurrentFlask().GetComponent<flaskDrop>().GetFlaskSO().lengths_hold;
        spawnPos = transform.position;
        originalPos = spawnPos;

        spawnBeat = BeatGame.Current.getSampledTime();

        // constant velocity = distance / time (in beats)
        VelocityPerBeat = (targetPos.position - spawnPos) / totalBeats;
        returnStart = true;
        holdingPince = true;
    }

    private void Update()
    {
        if (holdingPince)
        {
            if (turtle.GetIsHolding())
            {
                float songPosInBeats = BeatGame.Current.getSampledTime();
                float beatsPassed = songPosInBeats - spawnBeat;

                // position = start + velocity * beatsPassed
                transform.position = spawnPos + VelocityPerBeat * beatsPassed;
            }
            else
            {
                if (returnStart)
                {
                    returnStart = false;
                    returnStartPos = transform.position;
                    returnStartBeat = BeatGame.Current.getSampledTime();
                    detectFlask.getCurrentFlask().GetComponent<flaskDrop>().SetSpawn(returnStartPos);
                    Vector3 newPos = endTarget.position;
                    newPos.x = transform.position.x;
                    endTarget.position = newPos;
                    detectFlask.getCurrentFlask().GetComponent<Collider2D>().enabled = false;
                    detectFlask.getCurrentFlask().GetComponent<flaskDrop>().SetTransform(endTarget);
                    detectFlask.getCurrentFlask().GetComponent<flaskDrop>().SetIsCatched(false);
                    detectFlask.setCurrentFlaskNull();
                }
                DoReturnLerp();
            }
        }
        

    }
    private void DoReturnLerp()
    {
        float songPosInBeats = BeatGame.Current.getSampledTime();
        float beatsPassed = songPosInBeats - returnStartBeat;

        float t = Mathf.Clamp01(beatsPassed / returnBeat);

        // interpolate smoothly back
        transform.position = Vector3.Lerp(returnStartPos, originalPos, t);

        if (t >= 1f)
        {
            // snap & finish
            transform.position = originalPos;
            holdingPince = false;
            returnStart = true;
        }
    }
}
