using System.Collections.Generic;
using UnityEngine;
public interface ILoader<key,value> 
{
    Dictionary<key, value> MakeDic(); 
}
public class DataManager
{
     public Dictionary<int, Stat> StatDioct { get; private set; } = new Dictionary<int, Stat>();
    
    public void Init()
    {
        StatDioct = LoadJson<SkillData, int, Stat>("StatData").MakeDic();
    }
    Loader LoadJson<Loader,Key,Value>(string path)  where Loader : ILoader<Key,Value> 
    {
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/{path}");
        return  JsonUtility.FromJson<Loader>(textAsset.text); 
    }
}
