using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PressArea : MonoBehaviour
{
    [Header("Press Area")]
    [SerializeField] protected bool canBePressed = false;
    [SerializeField] protected KeyCode keyToPress;
    [SerializeField] protected AudioClip audioClip;
    [SerializeField] protected AudioSource audio;

    protected GameObject CurrentNoteDetected = null;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    //---------------------------------------------------------------------------------- Press Area -----------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            //Audio
            audio.clip = audioClip;
            audio.Play();

            if (canBePressed && OtherCondition())
            {
                
                SpawnThing();
                PressAtArea();
            }
            else
            {
                SpecialPropertyWaitCome();
                BeatGame.Current.NoteMissed();
                Debug.Log("cannot spawn");
            }
            // Set it false?


        }
    }
    public virtual bool OtherCondition()
    {
        return true;
    }
    public virtual void SpecialPropertyWaitCome()
    {

    }
    public virtual void SpawnThing()
    {
        Destroy(CurrentNoteDetected);
    }

    public virtual void PressAtArea()
    {

        //CentipedeGame.Instance.NoteHit();
        if (CondNormal())
        {
            Debug.Log("Normal Hit");
            BeatGame.Current.NormalHit();
        }
        else if (CondGood())
        {
            Debug.Log("Good Hit");
            BeatGame.Current.GoodHit();
        }
        else
        {
            Debug.Log("Perfect Hit");
            BeatGame.Current.PerfectHit();

        }
        //Destroy(CurrentNoteDetected);
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
            OntriggerProperty();
        }
    }
    public virtual void OntriggerProperty()
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            if (CurrentNoteDetected)
            {
                BeatGame.Current.NoteMissed();
                CurrentNoteDetected = null;
            }
            OnTriggerExitProperty();
            canBePressed = false;
        }
    }
    public virtual void OnTriggerExitProperty()
    {

    }
    
    
}
