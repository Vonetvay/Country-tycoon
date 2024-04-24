using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using YG;




[Serializable]
public class PlayerData 
{
    public int Money = 0;
    public List<int> CompletedMissionIDs = new List<int>();
    /*public List<int> AgriReward = new List<int>() { 100, 250 };
    public List<int> AgriTime = new List<int>() { 15, 30 };
    public int CaveIncome = 0;
    public List<bool> ShopItemCompleted = new List<bool>();*/
}

public class DataLoader
{
    private static XmlSerializer _xmlSerializer = new XmlSerializer(typeof(PlayerData));

    private static PlayerData _loadedData = null;


    public static void Save(PlayerData dataToSave)
    {
        string xmlString = "";
        using (MemoryStream s = new MemoryStream())
        {
            _xmlSerializer.Serialize(s, dataToSave);
            xmlString = Encoding.ASCII.GetString(s.ToArray());
        }

        Debug.Log(xmlString);

        
        YandexGame.savesData.xmlString = xmlString;
        YandexGame.SaveCloud();
        YandexGame.SaveProgress();
        //PlayerPrefs.SetString("save", xmlString);
        //PlayerPrefs.Save();
    }

    public static PlayerData Load()
    {
        if (_loadedData != null) { return _loadedData; }

        try
        {
            YandexGame.LoadCloud();
            YandexGame.LoadProgress();
            if (string.IsNullOrEmpty(YandexGame.savesData.xmlString)) { return new PlayerData(); }
            string xmlString = YandexGame.savesData.xmlString;
            using (MemoryStream s = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                return _xmlSerializer.Deserialize(s) as PlayerData;
            }

        } catch (Exception ex)
        {
            Debug.LogError(ex);
            return new PlayerData();
        }

    }

    public static void Erase()
    {
        _loadedData = null;
        YandexGame.savesData.xmlString = "";
        YandexGame.SaveProgress();
        //PlayerPrefs.SetString("save", "");
        //PlayerPrefs.Save();
    }
}
