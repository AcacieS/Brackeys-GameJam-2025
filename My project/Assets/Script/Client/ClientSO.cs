using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Scriptable Objects/ClientSO")]
public class ClientSO : ScriptableObject
{
    public string clientName;
    public string scene;
    public Sprite sprite;
    public int nbTimeVisited;
    public Sprite transition;
    public Vector3 position;
    public Vector3 scale;
    public string[] dialogue;
    
}
