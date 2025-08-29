using System.Data;
using UnityEngine;

public class PressArea_Centipede : PressArea
{
    [Header("Centipede Press")]
    [SerializeField] private GameObject hole;
    [SerializeField] private Animator centipedeAnim;

    [Header("Miss in Advance")]
    [SerializeField] private SpawnNote spawnScript;
    public GameObject CurrentNoteCome = null;
    int index = 0;

    public override void SpawnThing()
    {
        if (!spawnScript.GetFirstElementState()) //if already pressed
        {
            centipedeAnim.Play("centipede_press");
            Instantiate(hole, transform.position, Quaternion.identity);
        }
    }
    public override bool OtherCondition()
    {
        return !spawnScript.GetFirstElementState();
    }
    public void SetCurrentNoteCome(GameObject noteCome)
    {
        CurrentNoteCome = noteCome;
    }
    public override void SpecialPropertyWaitCome()
    {
        spawnScript.SetCurrentElementState();
    }
    public override void PressAtArea()
    {
        if (CurrentNoteCome == null)
        {
            CurrentNoteDetected = null;
            canBePressed = false;
            return;
        }

        base.PressAtArea();
    }
    public override void OntriggerProperty()
    {
        spawnScript.RemoveElement();
        
        if (CurrentNoteCome != null)
        {
            Debug.Log("Haven't press before: Get currentNote: " + CurrentNoteCome);
            CurrentNoteCome = spawnScript.GetFirstElement();
        }
        Debug.Log("Current come by coming" + CurrentNoteCome);
        base.OntriggerProperty();
    }    
    public override void OnTriggerExitProperty()
    {
        CurrentNoteCome = spawnScript.GetFirstElement();
        Debug.Log("Current come by exit " + CurrentNoteCome);
    }
    
    
}
