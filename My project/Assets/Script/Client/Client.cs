
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [Header("Client")]
    [SerializeField] private ClientSO[] clientsSO;
    [SerializeField] private ClientSO testClient;
    [SerializeField] public static ClientSO currentClientSO;

    [Header("General")]
    [SerializeField] private SceneManagement sceneManagement;
    [SerializeField] private endTransition endTrans;
    [SerializeField] private bool isFinish = false;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI RuleUI;
    [SerializeField] private TextMeshProUGUI NameRule;
    [SerializeField] private GameObject play;
    private Animator anim;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        QuitShop();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void QuitShop()
    {
        if (currentClientSO != null)
        {
            if (GameManager.Instance.getDifficulty() == Difficulty.Easy)
            {
                play.SetActive(false);
                anim.Play("Client Quit");
            }
            else
            {
                if (GameManager.Instance.GetWinHard()) //you win -> client die
                {
                    play.SetActive(false);
                    anim.Play("Client Die");
                }
                else //you lost -> lost health
                {
                    Debug.Log("losttt");
                    GameManager.Instance.RemoveArmHealth(10);
                    play.SetActive(false);
                    anim.Play("Client Quit");
                    GameManager.Instance.WinMiniGame(true);
                }
            }
            
            
        }
        else
        {
            SpawnClient();
        }
    }
    public void SpawnClient()
    {
        if (testClient == null)
        {
            int index_client = Random.Range(0, clientsSO.Length);
            currentClientSO = clientsSO[index_client];
            Debug.Log("which client? " + currentClientSO.name);
        }
        else
        {
            currentClientSO = testClient;
            
        }
        GetComponent<SpriteRenderer>().sprite = currentClientSO.sprite;
        anim.Play("Client Enter");
        ShowGameRule();
        play.SetActive(true);

        Debug.Log("Current Client" + currentClientSO.clientName);
        Restart();
    }
    private void Restart()
    {

    }
    public void LoadMiniGame()
    {
        endTrans.ChangeSprite(currentClientSO.sprite);
        sceneManagement.Play();
        currentClientSO.nbTimeVisited++;
    }
    public void WhichScene()
    {
        string sceneName = currentClientSO.scene;
        Debug.Log("sceneName: " + sceneName);
        SceneManager.LoadScene("Scenes/" + sceneName);
    }
    public ClientSO getCurrentClientSO()
    {
        return currentClientSO;
    }
    public void ShowGameRule()
    {
        NameRule.text = currentClientSO.gameName;
        RuleUI.text = currentClientSO.rule;
        play.SetActive(true);
    }
    
    
}
