using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Scriptable Objects/ClientSO")]
public class ClientSO : ScriptableObject
{
    public string clientName;
    public string scene;
    public string[] dialogue;
    public SpriteRenderer sprite;
}
