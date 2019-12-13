using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemButton : MonoBehaviour
{
    public string itemName;

    public Text itemDisplayer;
    [SerializeField] int ws;

    public int level = 0;
    [HideInInspector]
    public string currentCost = "1"; //Datacontroller에서 LoadItemButton실행할때 startCurrentCost가 할당됨
    public string startCurrentCost = "0";
    [HideInInspector]
    public string goldPerSec="0#";
    public string startGoldPerSec = "1";

    public double costPow = 3.14f;
    public double upgradPow = 2.05f;
    [HideInInspector]
    public bool isPurchased = false;

    string[] currCostSplit;

    //int index = 0;
    private void Start()
    {
        DataController.GetInstance().LoadItemButton(this);
        StartCoroutine(AddGoldLoop());
        //currCostSplit = currentCost.Split('#');
        UpdateUI();
    }
    public void PurchasedItem()
    {
        string[] mgoldSplit = DataController.GetInstance().GetGold().Split('#');
        currCostSplit = currentCost.Split('#');
        //인덱스 비교 a인지 b인지 뭔지
        if (Array.IndexOf(DataController.ShortScaleSymbolReference, mgoldSplit[1]) == Array.IndexOf(DataController.ShortScaleSymbolReference, currCostSplit[1]))
        {
            //인덱스로 문자열비교가 끝나면 숫자 비교
            if (double.Parse(mgoldSplit[0]) >= double.Parse(currCostSplit[0]))
            {
                isPurchased = true;
                DataController.GetInstance().SubGold(currentCost); //아이템을 사면 subgold함수에서 뺴기가 됨
                level++;

                UpdateItem();
                UpdateUI();
                DataController.GetInstance().SaveItemButton(this);
            }
        }
        if (Array.IndexOf(DataController.ShortScaleSymbolReference, mgoldSplit[1]) > Array.IndexOf(DataController.ShortScaleSymbolReference, currCostSplit[1])) 
        {
            isPurchased = true;
            DataController.GetInstance().SubGold(currentCost); //아이템을 사면 subgold함수에서 뺴기가 됨
            level++;

            UpdateItem();
            UpdateUI();
            DataController.GetInstance().SaveItemButton(this);
        }
    }

    IEnumerator AddGoldLoop()
    {
        while (true)
        {
            if (isPurchased)
            {
                DataController.GetInstance().AddGold(goldPerSec);
            }
            yield return new WaitForSeconds(ws);
        }
    }

    public void UpdateItem()
    {
        string[] goldPerSecSplit = goldPerSec.Split('#');
        string[] currentCostSplit = currentCost.Split('#');
        //startGoldPerSec, startCurrentCost가 문자열이기 때문에 변환 필요
        string[] startGoldPerSplit = startGoldPerSec.Split('#');
        string[] startCurrentCostSplit = startCurrentCost.Split('#');

        int goldPS = Array.IndexOf(DataController.ShortScaleSymbolReference, goldPerSecSplit[1]);
        int currentC = Array.IndexOf(DataController.ShortScaleSymbolReference, currentCostSplit[1]);

        //시간남으면 정리가 가능한코드
        goldPerSec = (double.Parse(startGoldPerSplit[0]) * (int)Mathf.Pow((float)upgradPow, level)*0.05).ToString() +'#'+DataController.ShortScaleSymbolReference[goldPS];
        currentCost = (double.Parse(startCurrentCostSplit[0]) * (int)Mathf.Pow((float)costPow, level)* 0.05).ToString() +'#'+DataController.ShortScaleSymbolReference[currentC];

        goldPerSecSplit = goldPerSec.Split('#');
        currentCostSplit = currentCost.Split('#');

        //1000단위가 되면 배열인덱스 증가
        if (double.Parse(goldPerSecSplit[0]) >= 1000)
        {
            while(double.Parse(goldPerSecSplit[0]) >= 1000)
            {
                ++goldPS;
                goldPerSecSplit[0] = (double.Parse(goldPerSecSplit[0]) / 1000).ToString();
            }
            goldPerSec = goldPerSecSplit[0] + '#' + DataController.ShortScaleSymbolReference[goldPS];
        }
        if (double.Parse(currentCostSplit[0]) >= 1000)
        {
            while(double.Parse(currentCostSplit[0]) >= 1000)
            {
                ++currentC;
                currentCostSplit[0] = (double.Parse(currentCostSplit[0]) / 1000).ToString();
            }
            currentCost = currentCostSplit[0] + '#' + DataController.ShortScaleSymbolReference[currentC];
        }
    }

    public void UpdateUI()
    {
        string[] goldPerSecSplit = goldPerSec.Split('#');
        string[] currentCostSplit = currentCost.Split('#');

        itemDisplayer.text = itemName + "\nLevel: " + level + "\nCost: " + currentCostSplit[0]+ currentCostSplit[1] + "\nGold Per Sec: " + goldPerSecSplit[0] + goldPerSecSplit[1] + "\nIsPurchased: " + isPurchased;

        Debug.Log("currentCost : " + currentCost);
        Debug.Log("Gold Per Sec : " + goldPerSec);
    }
}
