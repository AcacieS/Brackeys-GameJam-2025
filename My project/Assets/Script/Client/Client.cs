using UnityEngine;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    [Header("Client")]
    [SerializeField] private ClientSO[] clientsSO;
    [SerializeField] private ClientSO currentClientSO;


    [SerializeField] private bool isFinish = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnClient();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnClient()
    {
        if (currentClientSO == null)
        {
            int index_client = Random.Range(0, clientsSO.Length);
            currentClientSO = clientsSO[index_client];
        }

        Debug.Log("Current Client" + currentClientSO.clientName);
        Restart();
    }
    private void Restart()
    {

    }
    public void LoadMiniGame()
    {
        currentClientSO.nbTimeVisited++;
        string sceneName = currentClientSO.scene;
        Debug.Log("sceneName: " + sceneName);
        SceneManager.LoadScene("Scenes/" + sceneName);
    }
    public ClientSO getCurrentClientSO()
    {
        return currentClientSO;
    }
    
    
}
