using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{

    public NotificationIDList notificationIDList;
    void Start()
    {

        if (File.Exists(Application.persistentDataPath + "/NotiTime.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/NotiTime.dat", FileMode.Open);
            notificationIDList = (NotificationIDList)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            notificationIDList = new NotificationIDList();
            notificationIDList.notificationIDlist = new List<int>();
        }
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        for (int i = notificationIDList.notificationIDlist.Count - 1; i >= 0; i--) {

            var takenID = notificationIDList.notificationIDlist[i];
            notificationIDList.notificationIDlist.RemoveAt(i);
            AndroidNotificationCenter.CancelNotification(takenID);


        }
        if (DateTime.Now.Hour <= 6 || DateTime.Now.Hour >= 22)
        {

            SetNotificationSpecific(8);
            SetNotificationSpecific(9);
        }
        else
        {
            SetNotification(60);
            SetNotification(120);
        }
        SaveData();

    }

    void SetNotification(int min)
    {
        var notification = new AndroidNotification();
        notification.Title = "Cigaratte Count";
        notification.Text = "How many cigarattes you have smoke so far";
        notification.FireTime = System.DateTime.Now.AddMinutes(min);
        notification.SmallIcon = "icon_id";
        notification.LargeIcon = "large_icon_id";

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");
        notificationIDList.notificationIDlist.Add(id);
    }
    void SetNotificationSpecific(int hour)
    {

        DateTime newDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day<6 ? DateTime.Now.Day: DateTime.Now.AddDays(1).Day, hour,0,0);
        var notification = new AndroidNotification();
        notification.Title = "Cigaratte Count";
        notification.Text = "How many cigarattes you have smoke so far";
        notification.FireTime = newDate;
        notification.SmallIcon = "icon_id";
        notification.LargeIcon = "large_icon_id";

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");
        notificationIDList.notificationIDlist.Add(id);
    }
    void SaveData()
    {
        BinaryFormatter Ibf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NotiTime.dat");
        Ibf.Serialize(file, notificationIDList);
        file.Close();
    }

}
