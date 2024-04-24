using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySellShopItem : MonoBehaviour
{
    [SerializeField] public SellShopItem _shopItem;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _speedStatText;
    [SerializeField] private TextMeshProUGUI _steerAngleStatText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _sellAllButton;

    public static DisplaySellShopItem Instance;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _icon.sprite = _shopItem.icon;
        _nameText.text = _shopItem.itemName;
        _descriptionText.text = _shopItem.description;
        _speedStatText.text = _shopItem.speedStat.ToString();
        _steerAngleStatText.text = _shopItem.steerAngleStat.ToString();
        _priceText.text = _shopItem.price.ToString();
    }

    public void ButtonSetActive(bool active) 
    {
        _sellButton.interactable = active;
        _sellAllButton.interactable = active;
    }
}
