using UnityEngine;

public class ShopAreaActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShopArea>() is ShopArea otherShopArea)
        {
            otherShopArea.ShopMenuSetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ShopArea>() is ShopArea otherShopArea)
        {
            otherShopArea.ShopMenuSetActive(false);
        }
    }
}
