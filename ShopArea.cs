using UnityEngine;

public class ShopArea : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;

    public void ShopMenuSetActive(bool active) 
    {
        _shopMenu.SetActive(active);
    }
}
