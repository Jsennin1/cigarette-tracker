using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSelectedOne : MonoBehaviour
{
  public void ChooseSelectedOneButton()
  {
        LoadDatas.loadDatas.SelectedOne(this.gameObject);
  }
}
