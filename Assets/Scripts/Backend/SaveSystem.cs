using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [HideInInspector] DataSC dataCtr;
    string path;
    void Awake()
    {
        path = Application.persistentDataPath + "/data.json";
        dataCtr = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
    }
    private void Start()
    {
        dataCtr.OnLoadInventory();
    }
    #region Handle Save JSON
    public void OnSaveResourceData(int[] array)
    {
        InventoryData data = Load();
        data.resourceDatas = array;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
    public void OnReset()
    {
        InventoryData data = Load();
        int[] b = { 0, 0, 0, 0, 0, 0 };
        data.resourceDatas = b;
    }
    #endregion
    public InventoryData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<InventoryData>(json);
        }
        else
        {
            Debug.Log("No save file found");
            return new InventoryData();
        }
    }
}
