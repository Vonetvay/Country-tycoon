using Gardening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public Mission ActiveMission { get; private set; }
    public List<GardeningMission> ActiveGarderingProcesses = new List<GardeningMission>();



    public Dictionary<string, InteractibleZone> InteractibleZones = new Dictionary<string, InteractibleZone>();
    public Dictionary<string, Field> Fields = new Dictionary<string, Field>();
    public delegate void MissionUpdateHandler();
    public event MissionUpdateHandler OnMissionUpdate;


    public static MissionManager Instance { get; private set; }

    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        var zones = FindObjectsByType<InteractibleZone>(FindObjectsSortMode.None);
        var fields = FindObjectsByType<Field>(FindObjectsSortMode.None);

        foreach (var item in zones)
        {
            // Регистрируем зоны
            if (!string.IsNullOrEmpty(item.ID))
            {
                if (!InteractibleZones.TryAdd(item.ID, item)) { Debug.LogError("Cannot register zone!"); }
            }
        }

        foreach (var item in fields)
        {
            // Регистрируем поля
            if (!string.IsNullOrEmpty(item.ID))
            {
                if (!Fields.TryAdd(item.ID, item)) { Debug.LogError("Cannot register field!"); }
            }
        }
    }

    private void Update()
    {
        foreach (var gProcess in ActiveGarderingProcesses)
        {
            gProcess.CountTime();
            if (gProcess.TimeUntilHarvest == 0)
            {
                ActiveGarderingProcesses.Remove(gProcess);
            }
        }
    }

    public bool SetMission(Mission mission)
    {
        if (ActiveMission != null) { Debug.LogError("We already have uncompleted mission!"); return false; }

        ActiveMission = mission;
        OnMissionUpdate?.Invoke();

        return true;
    }

    public void UpdateMission()
    {
        OnMissionUpdate?.Invoke();
    }

    public bool CompleteMission()
    {
        if (ActiveMission != null)
        {
            PlayerWallet.ChangeMoneyOnValue(ActiveMission.Reward);
            ActiveMission = null;

            Compass.Instance.SetTarget(null);

            OnMissionUpdate?.Invoke();



            return true;
        }

        return false;
    }

    public void CompleteGardeningMission(GardeningMission mission, int reward)
    {
        int totalReward = reward;
        PlayerWallet.ChangeMoneyOnValue(reward);

        mission.CompleteMission();
    }
}
