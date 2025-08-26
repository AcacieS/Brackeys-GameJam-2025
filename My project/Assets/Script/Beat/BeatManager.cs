using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public abstract class BeatManager : MonoBehaviour
{
    [Header("Beat Manager")]
    [SerializeField] protected float _bpm;
    private AudioSource _audioSource;
    [SerializeField] protected Interval_Pattern[] _intervals;
    [SerializeField] protected int index_interval = 0;
    private bool isFinish = false;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartOverride();
    }
    public virtual void StartOverride()
    {

    }
    public float getBeat()
    {
        return _bpm;
    }

    private void Update()
    {
        if (_intervals[index_interval].getIsFinish())
        {
            if (index_interval + 1 < _intervals.Length)
            {
                index_interval++;
            }
            else
            {
                isFinish = true;
            }
        }
    
        for (int i = 0; i < _intervals[index_interval].Size()&&!isFinish; i++)
        {
            float sampledTime = (_audioSource.timeSamples / (float)_audioSource.clip.frequency * (_bpm / 60f)); //interval.GetIntervalLength(_bpm)));
            _intervals[index_interval].getIntervals(i).CheckForNewInterval(sampledTime);
        }
        
        UpdateOverride();
    }
    public virtual void UpdateOverride()
    {
        

    }

}
[System.Serializable]
public class Interval_Pattern
{
    [SerializeField] private Intervals[] sameTime_interval;

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

    public void Reset()
    {
        _patternIndex = 0;
        _nextBeat = 0f;
        _lastInterval = -1;
    }

    public void CheckForNewInterval(float songPositionInBeats)
    {
        if (isFinish)
        {
            if (_patternIndex < _stepsSO.steps.Length && songPositionInBeats >= _nextBeat)
            {
                Debug.Log("next beat: " + _stepsSO.steps[_patternIndex]);
                OnEachBeat?.Invoke();
                // Trigger
                _trigger.Invoke();

                // Move to next step in the pattern
                _nextBeat += _stepsSO.steps[_patternIndex];
                _patternIndex++;
            }
            if (_patternIndex >= _stepsSO.steps.Length && _stepsSO.isLooping)
            {
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