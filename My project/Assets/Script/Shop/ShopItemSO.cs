using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "Scriptable Objects/ShopItemSO")]
public class ShopItemSO : ScriptableObject
{
    public String itemName;
    public int cost;
    public Sprite itemImg;
    [TextArea]
    public string description; 
    
}
