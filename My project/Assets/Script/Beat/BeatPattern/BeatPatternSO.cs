using UnityEngine;

[CreateAssetMenu(fileName = "BeatPatternSO", menuName = "Scriptable Objects/BeatPatternSO")]
public class BeatPatternSO : ScriptableObject
{
    public int max_pattern;
    public float[] steps;
    public bool isLooping;
}
