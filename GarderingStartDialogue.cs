using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GarderingStartDialogue : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown _cultureTypes;
    [SerializeField] private TMPro.TextMeshProUGUI _statsText;
    [SerializeField] private Button _plantButton;
    [SerializeField] private Button _harvestButton;
    [SerializeField] private TextMeshProUGUI _harvestButtonText;
    [SerializeField] private Button _rewardButton;

    [SerializeField] private GameObject _parentObject;

    public static GarderingStartDialogue Instance { get; private set; }

    private GardeningMission _mission;


    private int plantedCultureIndex = 0;

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

        _cultureTypes.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(enabled => {
            AgriCulture founded = _mission.AvailableCultures.First(x => x.Name == _cultureTypes.options[_cultureTypes.value].text);
        }));

        YandexGame.RewardVideoEvent += onAdComplete;
    }

    private void Update()
    {
        if (_mission != null)
        {
            updateScreen();
        }
    }

    private void updateScreen()
    {
        if (_mission == null) { return; }

        _cultureTypes.interactable = false;
        _plantButton.gameObject.SetActive(true);
        _rewardButton.gameObject.SetActive(false);
        _plantButton.interactable = false;
        _harvestButton.gameObject.SetActive(false);


        if (_mission.TimeUntilHarvest == 0)
        {
            if (_mission.FieldState == FieldState.EMPTY)
            {
                _cultureTypes.interactable = true;
                _plantButton.interactable = true;
            }
            else if (_mission.FieldState == FieldState.HAS_GROWN)
            {
                _statsText.text = "Урожай взошёл! Можно собирать!";
                _plantButton.gameObject.SetActive(false);
                _harvestButton.gameObject.SetActive(true);
                _rewardButton.gameObject.SetActive(true);
                _harvestButtonText.text = $"Собрать урожай ({_mission.AttachedField.HarvestingPrice}₽)";
            }
        }
        else
        {
            _statsText.text = "Осталось до всхода урожая:\n" + TimeSpan.FromSeconds(_mission.TimeUntilHarvest).ToString("mm\\:ss");
            _cultureTypes.SetValueWithoutNotify(plantedCultureIndex);
        }
    }


    public void Show(GardeningMission mission)
    {
        if (_mission == null)
        {
            int index = 0;

            _cultureTypes.options.Clear();

            foreach (AgriCulture culture in mission.AvailableCultures) 
            {
                _cultureTypes.options.Add(new TMP_Dropdown.OptionData() { text = culture.Name });
                if (mission.CurrentCulture == culture)
                {
                    plantedCultureIndex = index;
                }
                index++;
            }

            _cultureTypes.RefreshShownValue();

            updateScreen();


            _parentObject.SetActive(true);
        }




        _mission = mission;
    }


    public void StartMissionButton()
    {
        _mission.PlantFieldWith(_mission.AvailableCultures.First(x => x.Name == _cultureTypes.options[_cultureTypes.value].text));
        _mission = null;
        _parentObject.SetActive(false);
    }

    public void CancelMissionButton()
    {
        _mission = null;
        _parentObject.SetActive(false);
    }

    public void Harvest()
    {
        MissionManager.Instance.CompleteGardeningMission(_mission, _mission.CurrentCulture.Reward - _mission.AttachedField.HarvestingPrice);
        _mission = null;
        _parentObject.SetActive(false);
        _statsText.text = "Засадите поле культурой из списка";
    }

    public void HarvestWithAd()
    {
        YandexGame.RewVideoShow(0);
        _parentObject.SetActive(false);

    }

    private void onAdComplete(int a)
    {
        if (a == 0)
        {
            MissionManager.Instance.CompleteGardeningMission(_mission, _mission.CurrentCulture.Reward * 2);
            _mission = null;
            _parentObject.SetActive(false);
            _statsText.text = "Засадите поле культурой из списка";
        }
    }
}
