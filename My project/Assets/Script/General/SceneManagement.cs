using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject _startSceneTransition;
    [SerializeField] private GameObject _endSceneTransition;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startSceneTransition.SetActive(true);
    }

    public void Play()
    {
        _endSceneTransition.SetActive(true);
    }
    
}
