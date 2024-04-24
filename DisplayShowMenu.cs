using TMPro;
using UnityEngine;

public class DisplayShowMenu : MonoBehaviour
{
    [SerializeField] private ShopMenu _shopMenu;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    private void Start()
    {
        _name.text = _shopMenu.itemName;
        _description.text = _shopMenu.description;
    }
}
