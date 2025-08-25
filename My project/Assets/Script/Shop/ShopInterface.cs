
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInterface : MonoBehaviour
{
    [SerializeField] private ShopItemSO shopSO;
    private Image img;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI itemName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Image>().sprite = shopSO.itemImg;
        cost.text = shopSO.cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShopInterfaceDescription()
    {
        description.text = shopSO.description;
        itemName.text = shopSO.itemName;
    }
}
