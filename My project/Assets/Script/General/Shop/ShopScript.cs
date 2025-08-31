using TMPro;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject shopInterface;
    [SerializeField] private TextMeshProUGUI description_title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject payButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (shopInterface.activeSelf)
            {
                shopInterface.SetActive(false);
                payButton.SetActive(false);
            }
            else
            {
                shopInterface.SetActive(true);
                description_title.text = "Description";
                description.text = "";
            }
            
        }
    }
}
