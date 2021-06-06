using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetApp : MonoBehaviour
{

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ResetButton);
    }
    private void ResetButton()
    {
        RemoveSaves();
        ResetScene();
    }
    void RemoveSaves()
    {

        try
        {
            File.Delete(Application.persistentDataPath + "/NotiTime.dat");
            File.Delete(Application.persistentDataPath + "/sigaraSayisi.dat");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
