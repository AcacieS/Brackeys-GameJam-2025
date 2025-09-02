using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PayButtonScript : MonoBehaviour
{
    [SerializeField] private ShopItemSO[] shopsSO;
    [SerializeField] private TextMeshProUGUI textPay;
    private int payAmount = 0;
    private ShopItemSO currentShopSO;
    public void SetCurrentPay(ShopItemSO shopSO)
    {
        currentShopSO = shopSO;
        payAmount = shopSO.cost;
        textPay.text = shopSO.isBought ? "Sold out" : "Pay";
        //textPay
    }
    public void Pay()
    {
        if (!currentShopSO.isBought)
        {
            bool hasBought = GameManager.Instance.RemoveCoins(payAmount);
            if (hasBought)
            {
                currentShopSO.isBought = true;
                textPay.text = "Sold out";
                GameManager.Instance.AddArmHealth(currentShopSO.addHealth);
                CheckIfAllBought();
            }
        }
    }
    private void CheckIfAllBought()
    {
        for (int i = 0; i < shopsSO.Length; i++)
        {
            if (!shopsSO[i].isBought)
            {
                return;
            }
        }
        SceneManager.LoadScene("Scenes/Win");
    }
}
