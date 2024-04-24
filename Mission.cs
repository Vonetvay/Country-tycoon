using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using YG;



public class Mission : ScriptableObject
{
    public int ID
    {
        get
        {
            return _id;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public string Description
    {
        get
        {
            return _description;
        }
    }
    public int Reward
    {
        get
        {
            return _reward;
        }
    }

    [HideInInspector] public int Progress;

    public bool Repeating
    {
        get
        {
            return _repeating;
        }
    }

    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _reward;
    [SerializeField] private bool _repeating;

    // Вызывается, когда мы подъехали и забираем груз/человека
    // Нужно, чтобы видоизменить машину (отобразить сено в кузове)
    public virtual void PickupProduct() { }
    public virtual void CompleteMission() 
    { 
        //YandexGame.FullscreenShow();
    }
    public virtual void StartMission() { }
    public virtual void Initialize() 
    {
        var buff = DataLoader.Load();
        if (buff != null)
        {
            if (buff.CompletedMissionIDs.Contains(this.ID))
            {
                Destroy();
            }
        }
    }

    public virtual void Destroy() { }
    public virtual void FireInteraction(string zoneName) { }

}
