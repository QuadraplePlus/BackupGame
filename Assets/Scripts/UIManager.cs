using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
    public Text goldDisplayer;
   
    public Text goldPerSecDisplayer;

    private void Update()
    {
        string[] uiGold = DataController.GetInstance().GetGold().Split('#');
        string[] uiGoldPerSec = DataController.GetInstance().GetGoldPerSec().Split('#');
        goldDisplayer.text = "GOLD : " + Math.Round(double.Parse(uiGold[0]),3) + uiGold[1];
        goldPerSecDisplayer.text = "GOLD PER SEC : " + Math.Round(double.Parse(uiGoldPerSec[0])) + uiGoldPerSec[1];
    }
}
