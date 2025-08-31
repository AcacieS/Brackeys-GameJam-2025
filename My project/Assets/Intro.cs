using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] story;
    [SerializeField] private GameObject[] button;
    [SerializeField] private int maxStory = 3;
    [SerializeField] private SceneManagement scene;
    private int currentStory = 0;
   
    public void Next()
    {
        if (currentStory + 1 < maxStory)
        {
            if (currentStory == 0)
            {
                button[0].SetActive(false);
                button[1].SetActive(true);
            }
            story[currentStory].SetActive(false);
            currentStory++;
            story[currentStory].SetActive(true);
            
        }else
        {
            scene.Play();
            // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // int nextSceneIndex = currentSceneIndex + 1;
            // SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
}

