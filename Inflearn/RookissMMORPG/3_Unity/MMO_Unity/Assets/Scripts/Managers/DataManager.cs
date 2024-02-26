using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    /* 하드코딩 → 관리하기 용이하게 변경
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        TextAsset textAsset = Managers.resource.Load<TextAsset>($"Data/StatData");
        StatData data = JsonUtility.FromJson<StatData>(textAsset.text);

        /* Linq 기능 이용
         * IOS쪽에 Linq 버그가 많아 사용 안 하는 추세
        data.stats.ToDictionary();

        foreach (Stat stat in data.stats)
            StatDict.Add(stat.level, stat);
    }
    */

    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
