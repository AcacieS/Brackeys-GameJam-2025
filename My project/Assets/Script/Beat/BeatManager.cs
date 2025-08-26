
using System.Runtime.CompilerServices;
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
    //private float lastSampledTime = 0f;
    private float songLengthSec;     // length of the clip in seconds
    private float songLengthBeats;   // length of the clip in beats
    private float loopOffsetBeats;   // how many beats have passed across loops

    private float lastSongTime;
    private void Start()
    {
        // songLengthSec = _audioSource.clip.length;
        // songLengthBeats = songLengthSec * (_bpm / 60f);

        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        // lastSongTime = 0;
        // loopOffsetBeats = 0;

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
        // float songTime =_audioSource.time;
        // if (songTime < lastSongTime)
        // {
        //     // we looped back
        //     loopOffsetBeats += songLengthBeats;

        //     // restart playback manually
        //     _audioSource.Stop();
        //     _audioSource.Play();
        // }


        // lastSongTime = songTime;

        UpdateOverride();

        if (_intervals[index_interval].getIsFinish()) //if current is Finish
        {
            NextInterval();
        }

        for (int i = 0; i < _intervals[index_interval].Size() && !isFinish; i++)
        {
            float sampledTime = getSampledTime();
            _intervals[index_interval].getIntervals(i).CheckForNewInterval(sampledTime);
        }
        
        
    }
    // private void ResetAllIntervals(float startBeat)
    // {
    //     foreach (var pattern in _intervals)
    //     {
    //         for (int i = 0; i < pattern.Size(); i++)
    //         {
    //             pattern.getIntervals(i).Reset(startBeat);
    //         }
    //     }

    //     if (wait != null)
    //         wait.Reset(startBeat);
    // }
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
    public float getSampledTime()
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
        if (isFinish) return;


        if (_patternIndex < _stepsSO.steps.Length && songPositionInBeats >= _nextBeat)
        {
            Debug.Log("next beat: " + _stepsSO.steps[_patternIndex] + " ---- ");
            OnEachBeat?.Invoke();
            _trigger.Invoke();

            // Move to next step in the pattern
            _nextBeat += _stepsSO.steps[_patternIndex];
            _patternIndex++;
            if (_patternIndex >= _stepsSO.steps.Length && !_stepsSO.isLooping)
            {
                if (songPositionInBeats >= _nextBeat)
                {
                    isFinish = true;
                }

            }

        } else if (_patternIndex >= _stepsSO.steps.Length && !_stepsSO.isLooping) {
            if (songPositionInBeats >= _nextBeat)
            {
                isFinish = true;
            }
        }
        if (_patternIndex >= _stepsSO.steps.Length && _stepsSO.isLooping)
        {
            Debug.Log("hey looping");
            _patternIndex = 0;
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