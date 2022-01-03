using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class RankManager : MonoBehaviour
{

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static RankManager _instance;
    public static RankManager instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "RankManager";
                _instance = _container.AddComponent(typeof(RankManager)) as RankManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string RankDataFileName = ".json";

    public RankData _rankData;
    public RankData rankData
    {
        get
        {
            if (_rankData == null)
            {
                LoadRankData();
                SaveRankData();
            }
            return _rankData;
        }
    }

    public void LoadRankData()
    {
        string filePath = Application.persistentDataPath + RankDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("로드 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _rankData = JsonUtility.FromJson<RankData>(FromJsonData);
        }
        else
        {
            Debug.Log("세 데이터 생성");
            _rankData = new RankData();
        }
    }

    public void SaveRankData()
    {
        string ToJsonData = JsonUtility.ToJson(rankData);
        string filePath = Application.persistentDataPath + RankDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 성공");

    }

    private void OnApplicationQuit()
    {
        SaveRankData();
    }
  
}
