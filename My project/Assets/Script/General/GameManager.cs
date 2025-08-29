using System;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int coins = 0;
    private float maxArmHealth = 100f;
    public static float armHealth = 100f;
    public static Difficulty currentDifficulty = Difficulty.Easy;
    public static event Action OnCoinsChanged;

    [Header("UI")]
    [SerializeField] private GameObject coinsUI;
    [SerializeField] private HealthBar healthBar;

    [Header("Client")]
    [SerializeField] private Client clientScript;

    //--------------------------- Difficulty -------------------------------------
    public Difficulty getDifficulty()
    {
        return currentDifficulty;
    }
    public void SetMiniGameDiff()
    {
        
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
        if (armHealth <= 0.01)
        {
            Lost();
        }
    }
    
    private void Lost()
    {
        SceneManager.LoadScene("Scenes/Lost");
    }
    private IEnumerator RemoveArmHealthRate()
    {
        while (armHealth > 0)
        {
            Debug.Log("health?");
            yield return new WaitForSeconds(1);
            RemoveArmHealth(0.5f);
        }
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
        if (coins - RemoveCoins < 0)
        {
            coinsUI.GetComponent<Animator>().Play("coins_red");
            return;
        }
        coins -= RemoveCoins;
        OnCoinsChanged?.Invoke();
        //coinsUI.text = coins.ToString();
    }
    //------------------------- Client Spawn -------------------------
    void Start()
    {
        healthBar.SetMaxHealth(maxArmHealth);
        StartCoroutine(RemoveArmHealthRate());
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

    void Update()
    {
    }
    
    
}
