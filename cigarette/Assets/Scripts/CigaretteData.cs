using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CigaretteData
{
    public List<DailyData> dailyCigaretteDataList;
    
}

[System.Serializable]
public class DailyData
{
    public int cigaretteCount;
    public string date;
    public int day;
    public DailyData(int cigaretteCount, string date, int day) {
        this.cigaretteCount = cigaretteCount;
        this.date = date;
        this.day = day;
    }
}

