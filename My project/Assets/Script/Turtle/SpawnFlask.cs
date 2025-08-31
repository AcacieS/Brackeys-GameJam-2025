using UnityEngine;


public class SpawnFlask : MonoBehaviour
{
    [SerializeField] private FlaskSO[] flasks;
    [SerializeField] private Transform pince; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SpawnFlaskObj()
    {
        Debug.Log("instantiate flask");
        int index = Random.Range(0, flasks.Length);
        GameObject newFlask = Instantiate(flasks[index].flaskPrefab, transform.position, Quaternion.identity);
        newFlask.GetComponent<flaskDrop>().SetFlaskSO(flasks[index]);
        newFlask.GetComponent<flaskDrop>().SetTransform(pince);
    }
}
