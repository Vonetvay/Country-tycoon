using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopItem/Sell", fileName = "new SellShopItem")]
public class SellShopItem : ScriptableObject
{
    public int price;

    public Sprite icon;
    public string itemName;
    [TextArea(4, 4)] 
    public string description;

    [Header("Stats")]
    public float speedStat;
    public float steerAngleStat;

    public int itemId;
}
