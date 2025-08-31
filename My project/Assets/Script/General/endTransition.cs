
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endTransition : MonoBehaviour
{
    private int sceneIndex;
    [SerializeField] private Client clientScript;
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void DisableEndtTransition()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
    public void ChangeSprite(Sprite newSprite)
    {
        GetComponent<Image>().sprite = newSprite;
    }
    public void ClientNextScene()
    {
        if (clientScript == null)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            clientScript.WhichScene();
        }
            
    }
}
