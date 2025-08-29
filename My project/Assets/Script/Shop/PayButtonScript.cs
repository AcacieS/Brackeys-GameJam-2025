using UnityEngine;

public class PayButtonScript : MonoBehaviour
{
    private int payAmount = 0;
    public void SetCurrentPay(int cost)
    {
        payAmount = cost;
    }
    public void Pay()
    {
        GameManager.Instance.RemoveCoins(payAmount);
    }
}
