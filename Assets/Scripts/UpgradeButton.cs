using BayatGames.SaveGameFree.Serializers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text upgradeDisplayer;

    public string upgradeName;

    [HideInInspector]
    public int goldByUpgrade;
    public int startGoldByUpgrade = 1;
    [HideInInspector]
    public int currentCost = 1;
    public int startCurrentCost = 1;
    [HideInInspector]
    public int level;
    
    public float upgradePow = 1.08f;
    public float costPow = 3.15f;

    private void Start()
    {
        //DataController.GetInstance().LoadUpgradeButton(this);
        UpdateUI();
    }

    public void PurchaseUpgrade()
    {
        if (int.Parse(DataController.GetInstance().GetGold()) >= currentCost)
        {
            DataController.GetInstance().SubGold(currentCost.ToString());
            level++;
            DataController.GetInstance().AddGoldPerClick(goldByUpgrade);

            UpdateUpgrade();
            UpdateUI();
            //DataController.GetInstance().SaveUpgradeButton(this);
        }
    }

    public  void UpdateUpgrade()
    {
        goldByUpgrade = startGoldByUpgrade * (int) Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, level);
    }
    
    public void UpdateUI()
    {
        upgradeDisplayer.text = upgradeName + "\nCost: " + currentCost + "\nLevel: " + level + "\nNext New GoldPerClick: " + goldByUpgrade;
    }
}
