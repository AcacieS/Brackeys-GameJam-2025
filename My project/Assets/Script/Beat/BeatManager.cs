using UnityEngine;
using UnityEngine.Events;

public abstract class BeatManager : MonoBehaviour
{
    [Header("Beat Manager")]
    [SerializeField] protected float _bpm;
    private AudioSource _audioSource;
    [SerializeField] protected Intervals[] _intervals;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartOverride();
    }
    public virtual void StartOverride()
    {

    }


    private void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (float)_audioSource.clip.frequency * (_bpm / 60f)); //interval.GetIntervalLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
        UpdateOverride();
    }
    public virtual void UpdateOverride()
    {
        
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


    public void Reset()
    {
        _patternIndex = 0;
        _nextBeat = 0f;
        _lastInterval = -1;
    }

    public void CheckForNewInterval(float songPositionInBeats)
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