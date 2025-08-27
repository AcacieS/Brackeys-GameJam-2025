using TMPro;
using UnityEngine;

public class ShopCoinsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        GameManager.Instance.OnCoinsChanged += UpdateCoinsUI;
        UpdateCoinsUI(); // also update immediately in case coins changed before scene loaded
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCoinsChanged -= UpdateCoinsUI;
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = GameManager.Instance.GetCoins().ToString();
    }
}
