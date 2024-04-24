using Gardening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;


public enum FieldState
{
    EMPTY,
    PLANTED,
    HAS_GROWN
}

[CreateAssetMenu]
public class GardeningMission : Mission
{
    public float TimeUntilHarvest { get; private set; }
    public AgriCulture CurrentCulture { get; private set; }
    public float HarvestVolume { get; private set; }
    public FieldState FieldState { get; private set; }

    public AgriCulture[] AvailableCultures
    {
        get
        {
            return _availableCultures;
        }
    }

    public string FieldID
    {
        get
        {
            return _fieldID;
        }
    }

    [SerializeField] private string _fieldID = "";


    public Field AttachedField { get; private set; }
    [SerializeField] private AgriCulture[] _availableCultures;



    public override void Initialize()
    {
        base.Initialize();

        Field a = null;
        MissionManager.Instance.Fields.TryGetValue(_fieldID, out a);
        AttachedField = a;

        if (AttachedField == null)
        {
            Debug.LogWarning("Mission does not contains field!");
        }

        FieldState = FieldState.EMPTY;
    }


    public override void CompleteMission()
    {
        base.CompleteMission();

        CurrentCulture = null;
        AttachedField.ApplyNewSkin(null);
        FieldState = FieldState.EMPTY;

        var buff = DataLoader.Load();
        buff.Money = (int)PlayerWallet.GetMoney();

        DataLoader.Save(buff);
    }


    public override void FireInteraction(string zoneName)
    {
        base.FireInteraction(zoneName);

        GarderingStartDialogue.Instance.Show(this);
    }

    public bool PlantFieldWith(AgriCulture culture)
    {
        if (FieldState == FieldState.EMPTY)
        {
            FieldState = FieldState.PLANTED;
            AttachedField.ApplyNewSkin(AttachedField.Skins.Find(x => (x.Type == culture && x.Variant == FieldState)));
            CurrentCulture = culture;
            TimeUntilHarvest = culture.HarvestTime;

            MissionManager.Instance.ActiveGarderingProcesses.Add(this);

            return true;
        }

        return false;
    }


    public void CountTime()
    {
        if (TimeUntilHarvest > 0)
        {
            TimeUntilHarvest -= Time.deltaTime;
        }


        if (TimeUntilHarvest <= 0)
        {
            TimeUntilHarvest = 0;

            if (FieldState == FieldState.PLANTED)
            {
                FieldState = FieldState.HAS_GROWN;
                AttachedField.ApplyNewSkin(AttachedField.Skins.Find(x => (x.Type == CurrentCulture && x.Variant == FieldState)));
            }
        }
    }
}
