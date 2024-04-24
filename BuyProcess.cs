using UnityEngine;

public class BuyProcess : MonoBehaviour
{
    private BuyShopItem _shopItem;
    private DisplayBuyShopItem_ _displayBuyShopItem;

    [SerializeField] private int _buyId;

    private void Awake()
    {
        _displayBuyShopItem = GetComponent<DisplayBuyShopItem_>();
        _shopItem = _displayBuyShopItem._shopItem;
    }

    public void StartBuy() 
    {
        if (_shopItem.price <= PlayerWallet.GetMoney()) 
        {
            PlayerWallet.ChangeMoneyOnValue(-_shopItem.price);
            _displayBuyShopItem.bought = true;
            UseEvent.Use?.Invoke(_buyId);
            _displayBuyShopItem.UpdateState();
        }
        else 
        {
            //
        }
    }
}
