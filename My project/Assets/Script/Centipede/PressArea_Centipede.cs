using UnityEngine;

public class PressArea_Centipede : PressArea
{
    [Header("Centipede Press")]
    [SerializeField] private GameObject hole;
    [SerializeField] private Animator centipedeAnim;

    [Header("Miss in Advance")]
    [SerializeField] private SpawnNote spawnScript;
    int index = 0;
    [SerializeField] private Transform target;
    public override void SpawnThing()
    {
        //Debug.Log("spawnScript.GetFirstElementState(): " + spawnScript.GetFirstElementState());
        if (!spawnScript.GetFirstElementState()) //if already pressed
        {
            centipedeAnim.Play("centipede_press");
            GameObject hole_obj = Instantiate(hole, transform.position, Quaternion.identity);
            hole_obj.GetComponent<HolesScript>().SetScript(spawnScript);
        }
    }
    public override bool OtherCondition()
    {
        return !spawnScript.GetFirstElementState();
    }
    public override void SpecialPropertyWaitCome()
    {
        if (!spawnScript.GetFirstElementState())
        {
            audio.clip = audioClip;
            audio.Play();
        }
        SpawnThing();
        spawnScript.SetCurrentElementState();
        
    }
    public override void PressAtArea()
    {
        base.PressAtArea();
    }
    public override void OntriggerProperty()
    {
        //spawnScript.RemoveElement();

        // if (CurrentNoteCome != null)
        // {
        //     Debug.Log("Haven't press before: Get currentNote: " + CurrentNoteCome);
        //     CurrentNoteCome = spawnScript.GetFirstElement();
        // }
        // Debug.Log("Current come by coming" + CurrentNoteCome);
        base.OntriggerProperty();
    }
    public override void OnTriggerExitProperty()
    {
        spawnScript.RemoveElement();
        // CurrentNoteCome = spawnScript.GetFirstElement();
        // Debug.Log("Current come by exit " + CurrentNoteCome);
    }
    
    
}
