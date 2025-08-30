using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note_Coming
{
    [SerializeField] public GameObject note;
    [SerializeField] public bool isPressed = false;
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
    [SerializeReference] public List<List<Note_Coming>> currentPatternNote = new List<List<Note_Coming>>();
    [SerializeField] private PressArea_Centipede pressArea;
    [SerializeField] private Transform target;
    private bool firstSetNoteCome = true;
    private int add_index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>(); //Don't forget add audio
    }
    public Vector3 Speed()
    {
        return (target.position - transform.position) / 16;
    }

    public void ResetCurrentPatternNote()
    {
        currentPatternNote.Add(new List<Note_Coming>());
        add_index = currentPatternNote.Count - 1;
        firstSetNoteCome = true;
    }

    public void SpawnNoteInstance()
    {
        GameObject Note = Instantiate(notePrefab, SpawnPos.position, Quaternion.identity);
        Note.GetComponent<HoleScript>().SetTransform(target);
        if (currentPatternNote.Count == 0)
        {
            currentPatternNote.Add(new List<Note_Coming>());
        }
        Debug.Log("add_index " + add_index);
        currentPatternNote[add_index].Add(new Note_Coming(Note));
        audio.clip = SpawnSound;
        audio.Play();
    }
    public bool GetFirstElementState()
    {
        return currentPatternNote[0][0].GetIsPressed();
    }
    public void SetCurrentElementState()
    {
        currentPatternNote[0][0].SetIsPressed();
    }
   
    public GameObject GetFirstElement()
    {
        if (currentPatternNote[0].Count == 0)
        {
            return null;
        }
        return currentPatternNote[0][0].GetNote();
    }
    public void RemoveElement()
    {
        Debug.Log("Remove");
        currentPatternNote[0].RemoveAt(0);
        if (currentPatternNote[0].Count == 0)
        {
            Debug.Log("should delete");
            currentPatternNote.RemoveAt(0);
            add_index = 0;
        }
    }
}
