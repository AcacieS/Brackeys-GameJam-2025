using TMPro;
using UnityEngine;

public class CentipedeGame : MonoBehaviour
{
    public static CentipedeGame Instance { get; private set; }
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip audioHole;
    [SerializeField] private GameObject hole;
    [SerializeField] private Vector3 place;
    [SerializeField] private float beatTempo;

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


    void Start()
    {
        beatTempo = beatTempo / 60f;
        scoreUI.text = "Score: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(keyToPress))
        // {
        //     FillHole();
        // }
    }

    private void SpawnHole()
    {
        Instantiate(hole, place, Quaternion.identity);
        audio.Play();
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
    
}
