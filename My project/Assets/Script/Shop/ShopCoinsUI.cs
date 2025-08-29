using TMPro;
using UnityEngine;

public class ShopCoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        GameManager.OnCoinsChanged += UpdateCoinsUI;
        UpdateCoinsUI(); // also update immediately in case coins changed before scene loaded
    }

    private void OnDisable()
    {
        GameManager.OnCoinsChanged -= UpdateCoinsUI;
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = GameManager.Instance.GetCoins().ToString();
    }
}
