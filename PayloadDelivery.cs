using UnityEngine;


public enum PayloadType
{
    HAY,
    BARRELS
}



[CreateAssetMenu]
public class PayloadDelivery : Mission
{
    public PayloadType Type
    {
        get
        {
            return _type;
        }
    }
    public string StartZoneName
    {
        get
        {
            return _startZoneName;
        }
    }
    public string PickupZoneName
    {
        get
        {
            return _pickupZoneName;
        }
    }
    public string DeliveryZoneName
    {
        get
        {
            return _deliveryZoneName;
        }
    }

    [SerializeField] private PayloadType _type;
    [SerializeField] private string _startZoneName = "";
    [SerializeField] private string _pickupZoneName = "";
    [SerializeField] private string _deliveryZoneName = "";


    private InteractibleZone _startZone = null;
    private InteractibleZone _pickupZone = null;
    private InteractibleZone _deliveryZone = null;


    // ¬озвращает true, если после начала миссии нам ещЄ нужно ехать получать груз
    // на другой точке и false, если груз у нас по€вл€етс€ сразу
    //public bool isPickupZoneNeeded()
    //{
    //    return _pickupZoneName != "";
    //}

    public override void Initialize()
    {
        MissionManager.Instance.InteractibleZones.TryGetValue(_startZoneName, out _startZone);
        MissionManager.Instance.InteractibleZones.TryGetValue(_pickupZoneName, out _pickupZone);
        MissionManager.Instance.InteractibleZones.TryGetValue(_deliveryZoneName, out _deliveryZone);

        if (_startZone == null)
        {
            Debug.LogWarning("Mission does not contains start zone!");
        }
        MissionManager.Instance.OnMissionUpdate += onMissionUpdate;

        _pickupZone?.Hide();
        _deliveryZone?.Hide();
        _startZone?.SetNormal();

        base.Initialize();
    }

    private void onMissionUpdate()
    {
        if (MissionManager.Instance.ActiveMission == this)
        {
            // ≈сли наша мисси€ выбрана => мен€ем цвет градиента

            if (this.Progress == 0)
            {
                // «начит, нам нужно ещЄ заехать забрать груз => подсвечиваем только точку pickup'a
                _pickupZone?.SetTarget();
                _deliveryZone?.Hide();
                _startZone?.Hide();
            }
            else if (this.Progress == 50)
            {
                // «начит, груз у нас есть и нужно его доставить => подсвечиваем только точку delivery
                _pickupZone?.Hide();
                _deliveryZone?.SetTarget();
                _startZone?.Hide();
            }
            else if (this.Progress == 100)
            {
                // «начит, мисси€ завершена
                _pickupZone?.Hide();
                _deliveryZone?.Hide();
                _startZone?.Hide();
            }
        }
        else
        {
            _pickupZone?.Hide();
            _deliveryZone?.Hide();
            _startZone?.SetNormal();
        }
    }

    public override void StartMission()
    {
        base.StartMission();

        if (MissionManager.Instance.SetMission(this))
        {
            if (_startZone == _pickupZone || _pickupZone == null)
            {
                PickupProduct();
            }
        }
        else
        {
            Debug.LogWarning("Unable to start mission!");
        }
    }

    public override void PickupProduct()
    {
        base.PickupProduct();

        VehicleController.Instance.ApplyNewSkin(VehicleController.Instance.Skins.Find(x => x.Type == Type));

        Progress = 50;
        MissionManager.Instance.UpdateMission();
    }

    public override void CompleteMission()
    {
        base.CompleteMission();

        VehicleController.Instance.ApplyNewSkin(null);

        _deliveryZone.Hide();

        var buff = DataLoader.Load();

        if (Repeating == true)
        {
            _startZone.SetNormal();
        }
        else
        {
            if (!buff.CompletedMissionIDs.Contains(this.ID))
            {
                buff.CompletedMissionIDs.Add(this.ID);
            }

            Destroy();
        }

        buff.Money = (int)PlayerWallet.GetMoney();

        DataLoader.Save(buff);
    }

    public override void Destroy()
    {
        base.Destroy();

        Destroy(_startZone?.gameObject);
        Destroy(_pickupZone?.gameObject);
        Destroy(_deliveryZone?.gameObject);
    }

    public override void FireInteraction(string zoneName)
    {
        base.FireInteraction(zoneName);

        var zone = MissionManager.Instance.InteractibleZones[zoneName];

        if (zone == null) { return; }

        if (zone == _pickupZone)
        {
            if (MissionManager.Instance.ActiveMission == this)
            {
                PickupProduct();
            }
        }
        else if (zone == _deliveryZone) 
        {
            if (MissionManager.Instance.ActiveMission == this)
            {
                if (MissionManager.Instance.CompleteMission())
                {
                    CompleteMission();
                }
            }
        }else if (zone == _startZone)
        {
            if (MissionManager.Instance.ActiveMission == null)
            {
                MissionStartDialogue.Instance.Show(this);
            }
        }
    }

}
