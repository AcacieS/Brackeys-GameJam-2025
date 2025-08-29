using UnityEngine;
using UnityEngine.SceneManagement;

public class endTransition : MonoBehaviour
{
   private int sceneIndex;
   void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void DisableEndtTransition()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
