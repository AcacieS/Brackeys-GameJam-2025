using System.Collections;
using TMPro;
using UnityEngine;

public class BeatGame : BeatManager
{
    public static BeatGame Instance { get; private set; }

    //[SerializeField] private Animator animBeat;
    [SerializeField] private AudioClip audioHole;
    [SerializeField] private Vector3 place;
    [SerializeField] private float beatTempo;
    private float eachBeat;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private int currentScore;
    [SerializeField] private int scorePerNote = 100;
    [SerializeField] private int scorePerGoodNote = 125;
    [SerializeField] private int scorePerPerfectNote = 150;

    [Header("Score")]

    [SerializeField] private TextMeshProUGUI multiplierUI;
    [SerializeField] private int currentMultiplier;
    [SerializeField] private int multiplierTracker;
    [SerializeField] private int[] multiplierThresholds;


    public override void StartOverride()
    {
        beatTempo = beatTempo / 60f;
        eachBeat = beatTempo / 4f;
        scoreUI.text = "Score: 0";
        currentMultiplier = 1;
        StartCoroutine(BeatRoutine());
        // foreach (var interval in _intervals)
        // {
        //     interval.OnEachBeat = () =>
        //     {
        //         SpawnHole(); // call your custom function
        //     };
        // }
    }
    private void SpawnHole()
    {
        Debug.Log("Spawn a hole at this beat");
        // Instantiate your hole prefab here
    }
    public float getBeat()
    {
        return beatTempo;
    }
    private IEnumerator BeatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(eachBeat);
            //animBeat.Play("beat_anim");
        }


    }

    

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                currentMultiplier++;
            }
        }

        multiplierUI.text = "Multiplier: x" + currentMultiplier;
        scoreUI.text = "Score: " + currentScore.ToString();
    }
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        multiplierTracker = 0;
        currentMultiplier = 1;
        multiplierUI.text = "Multiplier: x" + currentMultiplier;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager created and will persist");
        }
        else if (Instance != this)
        {
            Debug.Log("Duplicate GameManager destroyed");
            Destroy(gameObject);

        }
    }
    // public override void EachBeatPattern()
    // {
        
    // }
    
}
