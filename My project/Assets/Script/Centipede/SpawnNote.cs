using System.Collections.Generic;
using UnityEngine;

public class Note_Coming
{
    public GameObject note;
    public bool isPressed;
    public Note_Coming(GameObject pNote)
    {
        note = pNote;
        isPressed = false;
    }
    public void SetIsPressed()
    {
        isPressed = true;
    }
    public bool GetIsPressed()
    {
        return isPressed;
    }
    public GameObject GetNote()
    {
        return note;
    }

    
}
public class SpawnNote : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform SpawnPos;
    private AudioSource audio;
    [SerializeField] private AudioClip SpawnSound;
    public List<Note_Coming> currentPatternNote = new List<Note_Coming>();
    [SerializeField] private PressArea_Centipede pressArea;
    private bool firstSetNoteCome = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>(); //Don't forget add audio
    }
    public void ResetCurrentPatternNote()
    {
        currentPatternNote = new List<Note_Coming>();
        firstSetNoteCome = true;
    }

    public void SpawnNoteInstance()
    {
        GameObject Note = Instantiate(notePrefab, SpawnPos.position, Quaternion.identity);
        currentPatternNote.Add(new Note_Coming(Note));

        if (firstSetNoteCome)
        {
            pressArea.SetCurrentNoteCome(GetFirstElement());
            firstSetNoteCome = false;
        }
        audio.clip = SpawnSound;
        audio.Play();
    }
    public bool GetFirstElementState()
    {
        return currentPatternNote[0].GetIsPressed();
    }
    public void SetCurrentElementState()
    {
        currentPatternNote[0].SetIsPressed();
    }
   
    public GameObject GetFirstElement()
    {
        if (currentPatternNote.Capacity == 0)
        {
            return null;
        }
        return currentPatternNote[0].GetNote();
    }
    public void RemoveElement()
    {
        currentPatternNote.RemoveAt(0);
    }
}
