using UnityEngine;

public class PassiveIncome : MonoBehaviour
{
    [SerializeField] private GameObject[] _shopItems;

    private int _addMoney = 1;

    private void AddMoney()
    {
        PlayerWallet.ChangeMoneyOnValue(_addMoney);
    }

    private void OnEnable()
    {
        UseEvent.Use.AddListener(Check);
    }

    private void OnDisable()
    {
        UseEvent.Use.RemoveListener(Check);
    }

    private void Check(int id) 
    {
        switch (id) 
        {
            case 400:
                foreach (GameObject shop in _shopItems) 
                {
                    shop.SetActive(true);
                }
                InvokeRepeating(nameof(AddMoney), 0f, 1f);
                break;
            case 401:
                _addMoney += 2;
                break;
            case 402:
                _addMoney += 3;
                break;
            case 403:
                _addMoney += 4;
                break;
        }
    }
}
