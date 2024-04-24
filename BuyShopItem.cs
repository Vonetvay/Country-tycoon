using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopItem/Buy", fileName = "Shop/ShopItem/Sell")]
public class BuyShopItem : SellShopItem
{
    public bool purchasedOnStart = false;
    public bool usedOnStart = false;
}
