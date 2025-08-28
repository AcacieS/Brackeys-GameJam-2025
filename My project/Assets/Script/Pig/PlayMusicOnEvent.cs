using UnityEngine;

public class PlayMusicOnEvent : MonoBehaviour

{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "StartMusicFood")
            audio.Play();
        
    }

}

