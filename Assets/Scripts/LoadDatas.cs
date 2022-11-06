using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadDatas : MonoBehaviour
{
    public static LoadDatas loadDatas;
    public CigaretteData cigaretteData;
    public GameObject dataHolder, buttonPlaces, count, day, date;
    public ScrollRect scrollRect;
    public GameObject selectedOne;

    private void Awake()
    {
        if (!loadDatas)
            loadDatas = this;
        else
            Destroy(this.gameObject);
        LoadCigaretteData();
    }

    public void LoadCigaretteData()
    {
        if (File.Exists(Application.persistentDataPath + "/sigaraSayisi.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sigaraSayisi.dat", FileMode.Open);
            cigaretteData = (CigaretteData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            cigaretteData = new CigaretteData();
            var dailyData = new DailyData(0, DateTime.Now.ToString("dd/MM/yyyy"), 1);
            cigaretteData.dailyCigaretteDataList = new List<DailyData>();
            cigaretteData.dailyCigaretteDataList.Add(dailyData);
        }
        CreateDataList();
    }

    public void CreateDataList()
    {
        var dataList = cigaretteData.dailyCigaretteDataList;

        while (dataList[dataList.Count - 1].date != DateTime.Now.ToString("dd/MM/yyyy"))
        {
            var a = dataList[dataList.Count - 1].date.Split('.');
            var dailyData = new DailyData(0, new DateTime(Int32.Parse(a[2]), Int32.Parse(a[1]), Int32.Parse(a[0])).AddDays(1).ToString("dd/MM/yyyy"), dataList[dataList.Count - 1].day + 1);
            dataList.Add(dailyData);
        }

        for (int i = 0; i < dataList.Count; i++)
        {
            var dataHolderGO = Instantiate(dataHolder, buttonPlaces.transform);
            dataHolderGO.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "" + dataList[i].day;
            dataHolderGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + dataList[i].date;
            dataHolderGO.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + dataList[i].cigaretteCount;
            if (i == dataList.Count - 1)
            {
                SelectedOne(dataHolderGO);
                scrollRect.verticalNormalizedPosition = 0;
            }
        }
    }
    public void SelectedOne(GameObject dataHolderGO)
    {
        if (selectedOne)
        {
            selectedOne.transform.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
        dataHolderGO.transform.GetComponent<Image>().color = new Color32(0, 142, 39, 255);
        selectedOne = dataHolderGO;
        var index = selectedOne.transform.GetSiblingIndex();
        count.GetComponent<TextMeshProUGUI>().text = "" + cigaretteData.dailyCigaretteDataList[index].cigaretteCount;
        day.GetComponent<TextMeshProUGUI>().text = cigaretteData.dailyCigaretteDataList[index].day + ".Day";
        date.GetComponent<TextMeshProUGUI>().text = "" + cigaretteData.dailyCigaretteDataList[index].date;

    }

    public void SaveData()
    {
        BinaryFormatter Ibf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/sigaraSayisi.dat");
        Ibf.Serialize(file, cigaretteData);
        file.Close();
    }
    private void OnApplicationPause()
    {
        SaveData();
    }
}

