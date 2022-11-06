using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class IncreaseCount : MonoBehaviour
{
    public void IncreaseCigaretteCount(bool increase)
    {

        var dataList = LoadDatas.loadDatas.cigaretteData.dailyCigaretteDataList;
        var selectedOne = LoadDatas.loadDatas.selectedOne.transform;
        var countText = LoadDatas.loadDatas.count.GetComponent<TextMeshProUGUI>();
        if (increase)
            countText.text = "" + (Int32.Parse(countText.text) + 1);
        else
            countText.text = "" + (Int32.Parse(countText.text) - 1);
        selectedOne.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + countText.text;
        dataList[selectedOne.GetSiblingIndex()].cigaretteCount = Int32.Parse(countText.text);
    }
}
