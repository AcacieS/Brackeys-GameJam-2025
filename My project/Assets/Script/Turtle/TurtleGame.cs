using UnityEngine;

public class TurtleGame : BeatGame
{
    [Header("TurtleGame -- Script")]
    [SerializeField] private DetectFlask detectFlask;
    [SerializeField] private pinceScript pinceScript;
    [SerializeField] private int requiredHoldTime = 4;
    [SerializeField] private Sprite sprite_normal;
    private int holdStartTime = 0;
    
    private bool isHolding = false;

    private bool catchOneTime = false;
    private string currentState = "Miss";


    public void getCurrentState(string pCurrentState)
    {
        currentState = pCurrentState;
    }
    public override void UpdateOverride()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (detectFlask.getCanCatch())
            {
                Debug.Log("Caught the object");

                CatchedFlask();
            }
            else
            {
                Debug.Log("Not Caught the object");
            }
            pinceScript.SetIsOpen(false);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isHolding)
            {
                switch (currentState)
                {
                    case "Miss":
                        Debug.Log("Miss All");
                        NoteMissed();

                        break;
                    case "Normal":
                        Debug.Log("normal Hit");
                        NormalHit();
                        break;
                    case "Good":
                        Debug.Log("Good Hit");
                        GoodHit();
                        break;
                    case "Perfect":
                        Debug.Log("Perfect");
                        PerfectHit();
                        break;
                }
                currentState = "Miss";
                GetComponent<SpriteRenderer>().sprite = sprite_normal;

                // float heldFor = Time.time - holdStartTime;

                // if (holdStartTime == requiredHoldTime)
                // {
                //     Debug.Log("Good Hold! Success after " + holdStartTime + " seconds");
                //     // do success action
                // }
                // else if (holdStartTime > requiredHoldTime)
                // {
                //     Debug.Log("Too Long" + holdStartTime + "seconds");
                //     // do fail action
                // }
                // else
                // {
                //     Debug.Log("Too Short! Held only " + holdStartTime + " seconds");
                //     // do fail action
                // }

            }
            isHolding = false;
            catchOneTime = false;
            pinceScript.SetIsOpen(true);
            Debug.Log("should open as key up");
        }
        base.UpdateOverride();
    }
    public bool GetIsHolding()
    {
        return isHolding;
    }
    private void CatchedFlask()
    {
        detectFlask.seHasCatch(true);
        if (!catchOneTime)
        {
            holdStartTime = 0;
            catchOneTime = true;
        }
        isHolding = true;
        pinceScript.AddFlask();

        ApprochePince();

    }
    private void ApprochePince()
    {
        
    }

    public void AddHoldTime()
    {
        if (isHolding)
        {
            holdStartTime++;
            Debug.Log("add holdstartTime" + holdStartTime);
        }
    }
    
}
