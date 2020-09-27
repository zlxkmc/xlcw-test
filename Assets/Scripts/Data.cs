using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Data : MonoBehaviour
{
    public static Data Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Data>();
            }

            return _instance;
        }
    }

    private static Data _instance;

    public List<Item> Items { get; private set; }
    public int Money { get => _money; set => _money = value; }

    [SerializeField]
    private int _money = 100;

    public void Awake()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Items.json");
        // 发布后有问题
        //Items = JsonConvert.DeserializeObject<List<Item>>(json);
        //Items = JsonUtility.FromJson<List<Item>>(json);
        Items = JsonMapper.ToObject <List<Item>>(json);
    }
}

