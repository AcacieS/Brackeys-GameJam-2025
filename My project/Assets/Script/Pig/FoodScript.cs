using UnityEngine;

public class FoodScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float beatTempo;

    private void Start() {
        beatTempo = BeatGame.Current.getBeat();
    }
    private void Update() {
        transform.position += new Vector3(-1 * beatTempo * Time.deltaTime, 0f, 0f);
    }
}
