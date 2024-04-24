using UnityEngine;

public class SellProcess : MonoBehaviour
{
    private SellShopItem _sellShopItem;

    private void Awake()
    {
        _sellShopItem = GetComponent<DisplaySellShopItem>()._shopItem;
    }

    public void Sell(int count) 
    {
        DisplayPlayerItems.Instants.DiscardItem(_sellShopItem.itemId, count);
        PlayerWallet.ChangeMoneyOnValue(count * _sellShopItem.price);
    }

    public void SellAll() 
    {
        int productCount = DisplayPlayerItems.Instants.itemsCount[_sellShopItem.itemId];
        DisplayPlayerItems.Instants.DiscardItem(_sellShopItem.itemId, productCount);
        PlayerWallet.ChangeMoneyOnValue(_sellShopItem.price * productCount);
    }
}
