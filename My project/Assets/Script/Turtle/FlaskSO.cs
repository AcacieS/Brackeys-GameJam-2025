using System;
using UnityEngine;
[CreateAssetMenu(fileName = "FlaskSO", menuName = "Scriptable Objects/FlaskSO")]
public class FlaskSO : ScriptableObject
{
    public string nameFlask;
    public GameObject flaskPrefab;
    public int lengths_drop;
    public int lengths_hold;
    public Sprite flaskImg;
    
    
}
