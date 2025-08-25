
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopInterface : MonoBehaviour
{
    [SerializeField] private ShopItemSO shopSO;
    private Image img;
    private TextMeshProUGUI description;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Image>().sprite = shopSO.itemImg;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShopInterfaceDescription()
    {
        
    }
}
