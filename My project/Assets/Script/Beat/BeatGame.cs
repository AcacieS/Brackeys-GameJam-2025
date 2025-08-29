using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatGame : BeatManager
{
    public static BeatGame Current { get; private set; }

    //[SerializeField] private Animator animBeat;
    [SerializeField] private AudioClip audioHole;
    [SerializeField] private Vector3 place;

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

    [Header("Difficulty")]
    [SerializeField] private ClientSO gameClientSO;
    [SerializeField] private GameObject showDiffObj;
    [SerializeField] private bool isTestDiff = false;
    private bool showDiff = false;

    public override void StartOverride()
    {
        scoreUI.text = "Score: 0";
        currentMultiplier = 1;
        setDiff();
    }
    public override void UpdateOverride()
    {
        if (showDiff)
        {
            ShowIfWantHard();
        }
        base.UpdateOverride();
    }
    private void setDiff()
    {
        if (gameClientSO != null)
        {
            int timeVisited = gameClientSO.nbTimeVisited;
            if (timeVisited >= 3 || isTestDiff)
            {
                int isDifficult = Random.Range(0, 2);
                if (isDifficult == 0 ||isTestDiff) //true
                {
                    showDiff = true;

                }
            }
        }
    }
    public virtual void setHard()
    {
        currentDifficulty = Difficulty.Hard;
        scorePerNote *= 2;
        scorePerGoodNote *= 2;
        scorePerPerfectNote *= 2;
    }
    
    private void ShowIfWantHard()
    {
        float clipLength = _audioSource.clip.length;
        float currentTime = _audioSource.time;
        if (currentTime >= clipLength / 2)
        {
            showDiffObj.SetActive(true);
            showDiff = false;
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        Multiplier();
        multiplierUI.text = "Multiplier: x" + currentMultiplier;
        scoreUI.text = "Score: " + currentScore.ToString();
    }
    private void Multiplier()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                currentMultiplier++;
            }
        }

    }
    public void NoteHit(NoteState noteHitType)
    {
        Debug.Log("Hit On Time");
        switch (noteHitType)
        {
            case NoteState.Good:
                GoodHit();
                break;
            case NoteState.Perfect:
                PerfectHit();
                break;
            case NoteState.Normal:
                NormalHit();
                break;
            default:
                break;
        }
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
    public virtual void NoteMissed()
    {
        Debug.Log("Missed Note");
        multiplierTracker = 0;
        currentMultiplier = 1;
        multiplierUI.text = "Multiplier: x" + currentMultiplier;
    }
    public override void OnSongFinished()
    {
        Debug.Log("current Score: " + currentScore);
        base.OnSongFinished();
        GameManager.Instance.AddCoins(currentScore);
        SceneManager.LoadScene("Scenes/Shop");

    }

    void Awake()
    {
        Current = this;
        // if (Instance == null)
        // {
        //     Instance = this;
        //     Debug.Log("GameManager created and will persist");
        // }
        // else if (Instance != this)
        // {
        //     Debug.Log("Duplicate GameManager destroyed");
        //     Destroy(gameObject);

        // }
    }
    void OnDestroy()
    {
        // Clear reference if object is destroyed
        if (Current == this)
            Current = null;
    }
    
    
}
