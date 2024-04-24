using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AgriCulture : ScriptableObject
{
    public string Name
    {
        get
        {
            return _name;
        }
    }
/*    public int Reward // видоизменно vonetvay
    {
        get
        {
            return _reward;
        }
        set { _reward = value; }
    }
    public int HarvestTime // видоизменно vonetvay
    {
        get
        {
            return _harvestTime;
        }
        set { _harvestTime = value; }
    }*/

    [SerializeField] private string _name;
    [SerializeField] public int Reward;
    [SerializeField] public int HarvestTime;
}
