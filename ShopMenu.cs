using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopMenu", fileName = "new ShopMenu")]
public class ShopMenu : ScriptableObject
{
    public string itemName;
    [TextArea(4, 4)]  public string description;
}
