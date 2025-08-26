
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [Header("Beat Manager")]
    [SerializeField] protected float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] protected Interval_Pattern[] _intervals;
    [SerializeField] private Intervals wait;
    [SerializeField] protected int index_interval = 0;
    private bool isFinish = false;
    //private bool isWait = true;
    private bool waitStarted = false;
    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        StartOverride();
    }
    public virtual void StartOverride()
    {

    }
    public float getBeat()
    {
        return _bpm/60f;
    }

    private void Update()
    {
        UpdateOverride();

        if (_intervals[index_interval].getIsFinish()) //if current is Finish
        {
            NextInterval();
        }

        for (int i = 0; i < _intervals[index_interval].Size() && !isFinish; i++)
        {
            float sampledTime = (_audioSource.timeSamples / (float)_audioSource.clip.frequency * (_bpm / 60f)); //interval.GetIntervalLength(_bpm)));
            _intervals[index_interval].getIntervals(i).CheckForNewInterval(sampledTime);
        }
        
        
    }
    private void NextInterval()
    {
        if (index_interval + 1 < _intervals.Length)
        {
            Debug.Log("isWait statement: " + _intervals[index_interval].getIsWait());
            if (wait != null && _intervals[index_interval].getIsWait()) //current wait = true;
            {
                Wait();
            }
        }
        else
        {
            isFinish = true;
        }
        
    }
    private void Wait()
    {
        //Wait Start the pattern Reset
        if (!waitStarted)
        {
            Debug.Log("Wait Started Reset");
            float sampledTime = getSampledTime();
            wait.Reset(sampledTime);
            waitStarted = true;
        }

        // Keep ticking wait
        float sampledTime2 = getSampledTime();
        wait.CheckForNewInterval(sampledTime2);

        if (wait.getIsFinish())
        {
            
            // wait done → move to next interval
            index_interval++;
            waitStarted = false; // reset for next time
            Debug.Log("finish wait, moving to next interval: " + index_interval);
            // Reset next interval’s steps
            float sampledTime3 = getSampledTime();
            for (int i = 0; i < _intervals[index_interval].Size(); i++)
                _intervals[index_interval].getIntervals(i).Reset(sampledTime3);
        }

        return; // stop here until wait is done
    }
    private float getSampledTime()
    {
        return _audioSource.timeSamples / (float) _audioSource.clip.frequency * (_bpm / 60f);
        
    }
    public virtual void UpdateOverride()
    {


    }

}
[System.Serializable]
public class Interval_Pattern
{
    [SerializeField] private Intervals[] sameTime_interval;
    private bool isWait = true;

    public int Size()
    {
        return sameTime_interval.Length;
    }
    public Intervals getIntervals(int index)
    {
        return sameTime_interval[index];
    }
    public bool getIsFinish()
    {
        return sameTime_interval[0].getIsFinish();
    }
    

    public bool getIsWait()
    {
        return isWait;
    }
    public void setIsWait(bool pIsWait)
    {
        isWait = pIsWait;
    }

}



[System.Serializable]
public class Intervals
{
    [SerializeField] private BeatPatternSO _stepsSO;   // pattern like [1, 2, 2, 0.5, 0.5]
    [SerializeField] private UnityEvent _trigger;
    public System.Action OnEachBeat;
    private int _lastInterval;
    private int _patternIndex = 0;
    private float _nextBeat = 0f;
    private bool isFinish = false;

    public void Reset(float songPositionInBeats)
    {
        _patternIndex = 0;
        _nextBeat = songPositionInBeats;
        _lastInterval = -1;
        isFinish = false;
    }

    public void CheckForNewInterval(float songPositionInBeats)
    {
        Debug.Log("maybe is Finish" + isFinish);
        if (!isFinish)
        {
            if (_patternIndex < _stepsSO.steps.Length && songPositionInBeats >= _nextBeat)
            {
                Debug.Log("next beat: " + _stepsSO.steps[_patternIndex] + " ---- ");
                OnEachBeat?.Invoke();
                // Trigger
                _trigger.Invoke();

                // Move to next step in the pattern
                _nextBeat += _stepsSO.steps[_patternIndex];
                _patternIndex++;
            }
            if (_patternIndex >= _stepsSO.steps.Length && _stepsSO.isLooping)
            {
                Debug.Log("hey looping");
                _patternIndex = 0;
            }
            if (_patternIndex >= _stepsSO.steps.Length && !_stepsSO.isLooping)
            {
                isFinish = true;
            }
        }
        
    }
    public bool getIsFinish()
    {
        return isFinish;
    }
    public virtual void EachBeatPattern()
    {

    }
}

// [System.Serializable]
// public class Intervals
// {
//     [SerializeField] private float _steps;
//     [SerializeField] private UnityEvent _trigger;
//     private int _lastInterval;
//     public float GetIntervalLength(float bpm)
//     {
//         return 60f / (bpm * _steps);
//     }

//     public void CheckForNewInterval(float interval)
//     {
//         if (Mathf.FloorToInt(interval) != _lastInterval)
//         {
//             _lastInterval = Mathf.FloorToInt(interval);
//             _trigger.Invoke();
//         }
//     }
//  }