using UnityEngine;

public class DetectFlask : MonoBehaviour
{
    private bool canPress = false;
    private bool hasCatch = false;
    private GameObject currentObj = null;

    public bool getCanCatch()
    {
        return canPress;
    }
    public GameObject getCurrentFlask()
    {
        return currentObj;
    }
    public void seHasCatch(bool pHasCatch)
    {
        hasCatch = pHasCatch;
    }
    public void setCurrentFlaskNull()
    {
        currentObj = null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "flask")
        {
            currentObj = other.gameObject;//.GetComponent<flaskDrop>().GetFlaskSO();
            canPress = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "flask")
        {
            if (!hasCatch)
            {
                canPress = false;
                currentObj = null;
                BeatGame.Current.NoteMissed();
            }
        }
        
    }
}
