using UnityEngine;

public class HoleScript : MonoBehaviour
{
        
    private float beatTempo;

    private void Start()
    {
        beatTempo = BeatGame.Instance.getBeat();
    }
    private void Update()
    {
        transform.position += new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
    }
  
}
