using UnityEngine;

public class HoleScript : MonoBehaviour
{
    
    [SerializeField] private bool canBePressed = false;
    public KeyCode keyToPress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                CentipedeGame.Instance.NoteHit();
                FillHole();
            }
            
        }
    }
    private void FillHole()
    {
        
        //transform.posiotion -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hole")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hole")
        {
            canBePressed = false;
            CentipedeGame.Instance.NoteMissed();
        }
    }
}
