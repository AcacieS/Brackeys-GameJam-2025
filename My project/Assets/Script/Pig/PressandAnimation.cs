using System.Collections;
using UnityEngine;

public class PressandAnimation : MonoBehaviour
{
    [SerializeField] protected bool canBePressed = false;
    [SerializeField] protected KeyCode keyToPress;
    [SerializeField] protected AudioClip audioClip;
    private GameObject CurrentNoteDetected = null;
    private bool isDelaying = false;

    [Header("Sprite Renderers")]
    public SpriteRenderer spriteRenderer1;  
    public SpriteRenderer spriteRenderer2;  


    [Header("Sprites")]
    public Sprite pigIdle;
    public Sprite pigEating1;
    public Sprite pigEating2;
    public Sprite pigJumping;

    public static class AnimationState
    {
        public static int stateOfGame = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer1 = GetComponent<SpriteRenderer>();
        if (spriteRenderer1 == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
        }

        // Set initial sprite
        if (spriteRenderer1 != null)
            spriteRenderer1.sprite = pigIdle;
    }

    //---------------------------------------------------------------------------------- Press Area -----------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        switch (AnimationState.stateOfGame)
        {
            case 0:
                spriteRenderer1.enabled = true;
                spriteRenderer1.sprite = pigIdle;
                spriteRenderer2.enabled = false;
                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 1;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 3;
                }
                else
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    break;
                }
                break;
            case 1:
                spriteRenderer1.enabled = true;
                spriteRenderer1.sprite = pigEating1;
                spriteRenderer2.enabled = false;
                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 3;
                }
                else
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 2;
                }
                break;
            case 2:
                spriteRenderer1.enabled = true;
                spriteRenderer1.sprite = pigEating2;
                spriteRenderer2.enabled = false;
                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 1;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 3;
                }
                else
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 0;
                }
                break;
            case 3:
                spriteRenderer1.enabled = true;
                spriteRenderer1.sprite = pigJumping;
                spriteRenderer2.enabled = false;
                AnimationState.stateOfGame = 4;
                StartCoroutine(DelayStateChange(1, 0.5f));
                break;
            case 4:
                spriteRenderer1.enabled = false;
                spriteRenderer2.enabled = true;
                spriteRenderer2.sprite = pigEating1;
                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 1;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 4;
                }
                else
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 5;
                }
                break;
            case 5:
                spriteRenderer2.enabled = true;
                spriteRenderer1.enabled = false;
                spriteRenderer2.sprite = pigEating2;
                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 1;
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 4;
                }
                else
                {
                    StartCoroutine(DelayStateChange(1, 0.5f));
                    AnimationState.stateOfGame = 0;
                }
                break;
        }
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                
                PressAtArea();
            }

        }
    }

    IEnumerator DelayStateChange(int nextState, float delay)
    {
        isDelaying = true;
        yield return new WaitForSeconds(delay);
        AnimationState.stateOfGame = nextState;
        isDelaying = false;
    }
    private void PressAtArea()
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
                BeatGame.Current.NoteMissed();
                CurrentNoteDetected = null;
            }

        }
    }

}
