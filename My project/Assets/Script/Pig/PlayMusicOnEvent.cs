using UnityEngine;

public class PlayMusicOnEvent : MonoBehaviour

{
    public AudioSource myAudioSource; // This will hold our AudioSource
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMyMusic() {
        if (myAudioSource != null && !myAudioSource.isPlaying) {
            myAudioSource.Play(); // Or use PlayOneShot() for sound effects
        }
    }
    void OnTriggerEnter(Collider other) {
        // This function is called when a trigger collider enters another collider
        if (other.gameObject.CompareTag("StartMusicFood")) // Optional: check for a specific tag
        {
            PlayMyMusic();
        }
    }

}

