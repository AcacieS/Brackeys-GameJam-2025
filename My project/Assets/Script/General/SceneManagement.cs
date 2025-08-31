using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private GameObject _startSceneTransition;
    [SerializeField] private GameObject _endSceneTransition;
    [SerializeField] private bool wantShowStart = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (wantShowStart)
        {
            _startSceneTransition.SetActive(true);
        }

    }

    public void Play()
    {
        _endSceneTransition.SetActive(true);
    }
    
}
