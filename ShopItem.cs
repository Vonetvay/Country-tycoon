using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopItem", fileName = "new ShopItem")]
public class ShopItem : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    [TextArea(4, 4)] 
    public string description;

    [Header("Stats")]
    public float speedStat;
    public float steerAngleStat;
}
