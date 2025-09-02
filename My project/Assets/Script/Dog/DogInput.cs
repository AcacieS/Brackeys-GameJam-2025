using UnityEngine;
using UnityEngine.Animations;

public class DogInput : MonoBehaviour
{
    [Header("Press Area")]
    [SerializeField] protected bool canBePressed = false;
    [SerializeField] protected KeyCode keyForGood;
    [SerializeField] protected KeyCode keyForBad;
    [SerializeField] private Animator animGalaloo;

    private GameObject bubbleDetected = null;


    //---------------------------------------------------------------------------------- Press Area -----------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(keyForGood))
        {

            if (canBePressed && bubbleDetected.GetComponent<BubbleScript>().GetBubbleSO().type == BubbleType.Good)
            {

                bubbleDetected.GetComponent<Animator>().SetBool("isHit", true);
                animGalaloo.Play("galaloo_poke");
                bubbleDetected.GetComponent<BubbleScript>().setHasClick(true);
                PressAtArea();
               
            }
            else
            {
                BeatGame.Current.NoteMissed();
                Debug.Log("miss not good button good");
            }
            
           
        }
        if (Input.GetKeyDown(keyForBad))
        {
            if (canBePressed && bubbleDetected.GetComponent<BubbleScript>().GetBubbleSO().type == BubbleType.Bad)
            {
                Debug.Log("PressBad");
                animGalaloo.Play("galaloo_tap");
                bubbleDetected.GetComponent<BubbleScript>().ResetGoal();
                bubbleDetected.GetComponent<BubbleScript>().setHasClick(true);
                PressAtArea();
                
            }
            else
            {
                BeatGame.Current.NoteMissed();
                Debug.Log("miss not good button bad");
                bubbleDetected = null;
            }
            bubbleDetected.GetComponent<BubbleScript>().setHasClick(true);
        }
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
        bubbleDetected = null;
        canBePressed = false;
    }

    public virtual bool CondNormal()
    {
        float offset = transform.localScale.x * 0.25f;
        Debug.Log("bubbleDetected.transform.position.x: " + bubbleDetected.transform.position.x + "transform.position.x + offset: " + (transform.position.x + offset));
        return Mathf.Abs(bubbleDetected.transform.position.x) > transform.position.x + offset;
    }
    public virtual bool CondGood()
    {
        float offset = transform.localScale.x * 0.10f;
        Debug.Log("bubbleDetected.transform.position.x: " + bubbleDetected.transform.position.x + "transform.position.x + offset: " + (transform.position.x + offset));
        return Mathf.Abs(bubbleDetected.transform.position.x) > transform.position.x + offset;
    }

    //---------------------------------------------------------------------------------- Detect -----------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bubble")
        {
            Debug.Log("miss? maybe reenter");
            canBePressed = true;
            bubbleDetected = other.gameObject;
        }
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bubble")
        {
            if (bubbleDetected && !bubbleDetected.GetComponent<BubbleScript>().getHasClick())
            {
                BeatGame.Current.NoteMissed();
                Debug.Log("miss by trigger exit");
                bubbleDetected = null;
            }
            canBePressed = false;
        }
    }
}
