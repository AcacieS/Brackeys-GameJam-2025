using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform SpawnPos;
    private AudioSource audio;
    [SerializeField] private AudioClip SpawnSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>(); //Don't forget add audio
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnNoteInstance()
    {
        Instantiate(notePrefab, SpawnPos.position, Quaternion.identity);
        Debug.Log("Instantiate Note");
        //audio.clip = SpawnSound; 
        //audio.Play();
    }
}
