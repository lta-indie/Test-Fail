using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.DesignPattern;
using LTA.Data;
using System;

public class DataManager : MapData<string,DataInfo[]>
{
    public override DataInfo[] this[string key]
    {
        get
        {
            if (!m_Data.ContainsKey(key))
            {
                m_Data.Add(key, GetDataInfos(key));
            }
            return m_Data[key];
        }
    }

    public DataInfo[] GetDataInfos(string dataName)
    {
        
        List<DataInfo> texts = new List<DataInfo>();

        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Data/" + dataName);

        foreach (TextAsset textAsset in textAssets)
        {
            texts.Add(new DataInfo(textAsset.name, textAsset.text));
        }
        return texts.ToArray();
    }
}

public class DataController : Singleton<DataController>,IAssetManager
{

    //public TestData testData;

    public void InitData()
    {
        DataHelper.DataManager = new DataManager();
        AssetHelper.AssetManager = this;
        //DataInfo[] dataInfos = DataHelper.DataManager.GetDataInfos("");
        //testData = new TestData();
        //testData.LoadData(dataInfos);


    }


    

    public T GetAsset<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    public T[] GetAllAsset<T>(string path) where T : UnityEngine.Object
    {
        return Resources.LoadAll<T>(path);
    }

    public ResourceRequest GetAssetAsync<T>(string path) where T : UnityEngine.Object
    {
        throw new NotImplementedException();
    }
}
