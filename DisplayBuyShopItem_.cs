using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBuyShopItem_ : DisplayShopItem
{
    [Header("States")]
    [SerializeField] public Button _mainButton;
    [SerializeField] private TextMeshProUGUI _mainButtonText;
    [SerializeField] private string _defaultMainButtonText;
    [SerializeField] private string _completeMainButtonText;
    public bool bought;

    [SerializeField] public Button _useButton;
    [SerializeField] private TextMeshProUGUI _useButtonText;
    [SerializeField] private string _defaultUseButtonText;
    [SerializeField] private string _completeUseButtonText;
    public bool used;

    public int _useId;

    private void OnEnable()
    {
        UseEvent.Use.AddListener(CheckUsed);
    }

    private void OnDisable()
    {
        UseEvent.Use.RemoveListener(CheckUsed);
    }

    public void CheckUsed(int id) 
    {
        used = id == _useId;
        UpdateState();
    }

    public void UpdateState()
    {
        if (bought)
        {
            _mainButtonText.text = _completeMainButtonText;
            _mainButton.interactable = false;
            _useButton.interactable = true;
            if (used)
            {
                _useButton.interactable = false;
                _useButtonText.text = _completeUseButtonText;
            }
            else
            {
                _useButton.interactable = true;
                _useButtonText.text = _defaultUseButtonText;
            }
        }
        else
        {
            _mainButtonText.text = _defaultMainButtonText;
            _mainButton.interactable = true;
            _useButton.interactable = false;
        } 
    }

    private void Awake()
    {
        used = _shopItem.usedOnStart;
        bought = used ? true : _shopItem.purchasedOnStart;
        UpdateState();
    }

    public void OnUse() 
    {
        UseEvent.Use?.Invoke(_useId);
    }
}