using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PressArea : MonoBehaviour
{
    [SerializeField] protected bool canBePressed = false;
    [SerializeField] protected KeyCode keyToPress;
    [SerializeField] protected AudioClip audioClip;
    private GameObject CurrentNoteDetected = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    //---------------------------------------------------------------------------------- Press Area -----------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                PressAtArea();
            }

        }
    }
    private void PressAtArea()
    {
        //CentipedeGame.Instance.NoteHit();
        if (CondNormal())
        {
            Debug.Log("Normal Hit");
            BeatGame.Instance.NormalHit();
        }
        else if (CondGood())
        {
            Debug.Log("Good Hit");
            BeatGame.Instance.GoodHit();
        }
        else
        {
            Debug.Log("Perfect Hit");
            BeatGame.Instance.PerfectHit();

        }
        Destroy(CurrentNoteDetected);
        CurrentNoteDetected = null;
        canBePressed = false;
    }
    public virtual bool CondNormal()
    {
        float offset = transform.localScale.x * 0.25f;
        return Mathf.Abs(CurrentNoteDetected.transform.position.x) > transform.position.x + offset;
    }
    public virtual bool CondGood()
    {
        float offset = transform.localScale.x * 0.10f;
        return Mathf.Abs(CurrentNoteDetected.transform.position.x) > transform.position.x + offset;
    }

    //---------------------------------------------------------------------------------- Detect -----------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            canBePressed = true;
            Debug.Log("Detect Note");
            CurrentNoteDetected = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (!CurrentNoteDetected)
            {
                canBePressed = false;
                BeatGame.Instance.NoteMissed();
                CurrentNoteDetected = null;
            }
            
        }
    }
    
}
