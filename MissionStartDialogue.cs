using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStartDialogue : Dialogue
{
    [SerializeField] private TMPro.TextMeshProUGUI _missionName;
    [SerializeField] private TMPro.TextMeshProUGUI _missionDescription;
    [SerializeField] private TMPro.TextMeshProUGUI _missionReward;

    [SerializeField] private GameObject _parentObject;

    public static MissionStartDialogue Instance { get; private set; }

    private Mission _mission;

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
    }

    public void Show(Mission mission)
    {
        if (_mission == null)
        {
            _missionName.text = mission.Name;
            _missionDescription.text = mission.Description;
            _missionReward.text = mission.Reward.ToString();

            _parentObject.SetActive(true);
        }

        _mission = mission;
    }


    public void StartMissionButton()
    {
        _mission.StartMission();
        _mission = null;
        _parentObject.SetActive(false);
    }

    public void CancelMissionButton()
    {
        _mission = null;
        _parentObject.SetActive(false);
    }

}
