using UnityEngine;

public class GardenUpdates : MonoBehaviour
{
    [SerializeField] private AgriCulture[] _allCulture;

    [SerializeField] private int[] _cultureStartPrice;
    [SerializeField] private int[] _cultureStartTime;

    private void Start()
    {
        PlayerData data = DataLoader.Load();
        for (int i = 0; i < _allCulture.Length; i++) 
        {
            _allCulture[i].Reward = _cultureStartPrice[i];
            _allCulture[i].HarvestTime = _cultureStartTime[i];
        }
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
            case 300:
                Price(10);
                break;
            case 301:
                Price(5);
                break;
            case 302:
                Time(2);
                break;
        }
        /*PlayerData data = DataLoader.Load();
        for (int i = 0; i < _allCulture.Length; i++) 
        {
            data.AgriReward[i] = _allCulture[i].Reward;
            data.AgriTime[i] = _allCulture[i].HarvestTime;
        }
        DataLoader.Save(data);*/
    }

    private void Time(int x) 
    {
        foreach (var item in _allCulture) 
        {
            item.HarvestTime /= x;
        }
    }

    private void Price(int x) 
    {
        foreach (var item in _allCulture)
        {
            item.Reward += item.Reward / x;
        }
    }
}
