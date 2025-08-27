using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int coins = 0;
    private float maxArmHealth = 100f;
    public static float armHealth = 100f;
    public static Difficulty currentDifficulty = Difficulty.Easy;
    public event Action OnCoinsChanged;

    [Header("UI")]
    //[SerializeField] private TextMeshProUGUI coinsUI;
    [SerializeField] private HealthBar healthBar;

    [Header("Client")]
    [SerializeField] private Client clientScript;

    //--------------------------- Difficulty -------------------------------------
    public Difficulty getDifficulty()
    {
        return currentDifficulty;
    }
    public void SetMiniGameEasy()
    {
        currentDifficulty = Difficulty.Easy;
        clientScript.LoadMiniGame();
    }
    public void SetMiniGameHard()
    {
        currentDifficulty = Difficulty.Hard;
        clientScript.LoadMiniGame();
    }

    //--------------------------- Arm Health ---------------------------------------------
    public float GetArmHealth()
    {
        return armHealth;
    }
    public void AddArmHealth(float AddArmHealth)
    {
        armHealth += AddArmHealth;
        healthBar.SetHealth(armHealth);
        
    }
    public void RemoveArmHealth(float RemoveArmHealth)
    {
        armHealth -= RemoveArmHealth;
        healthBar.SetHealth(armHealth);
    }

    //--------------------------- Coins ---------------------------------------------
    public int GetCoins()
    {
        return coins;
    }

    public void AddCoins(int AddCoins)
    {
        coins += AddCoins;
        OnCoinsChanged?.Invoke();
        // coinsUI.text = coins.ToString();
        // Debug.Log("coins: "+coins);

    }
    public void RemoveCoins(int RemoveCoins)
    {
        coins -= RemoveCoins;
        OnCoinsChanged?.Invoke();
        //coinsUI.text = coins.ToString();
    }
    //------------------------- Client Spawn -------------------------
    void Start()
    {
        healthBar.SetMaxHealth(maxArmHealth);
    }
    
    //-------------------------- General ------------------------------------

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager created and will persist");
        }
        else if (Instance != this)
        {
            Debug.Log("Duplicate GameManager destroyed");
            Destroy(gameObject);

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveArmHealth(5);
        }
    }
    private void UIElement()
    {
        
    }
}
