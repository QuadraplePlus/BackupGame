using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DataController : MonoBehaviour
{
    private static DataController instance;

    public static DataController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DataController>();

            if (instance == null)
            {
                GameObject container = new GameObject("DataController");
                instance = container.AddComponent<DataController>();
            }
        }
        return instance;
    }

    private ItemButton[] itemButtons;
    private SatelliteCell[] satelliteCells;

    private string m_gold = "0"; //문자열
    string[] mgoldSplit; // 쪼갠 문자열

    private double m_goldPerClick = 0;

    public static string[] ShortScaleSymbolReference = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "l", "M", "N",
        "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

    //현재 가지고 있는 파편 수
    public static int haveParts = 0;

    private void Awake()
    {
        m_gold = PlayerPrefs.GetString("Gold","0#");
        mgoldSplit = m_gold.Split('#'); //숫자와 문자로 분리 0: 숫자, 1:문자 123/abc
        m_goldPerClick = PlayerPrefs.GetInt("GoldPerClick", 1);

        itemButtons = FindObjectsOfType<ItemButton>();
        satelliteCells = FindObjectsOfType<SatelliteCell>();

        LoadParts();
    }

    public void SetGold(string newGold, string words)
    {
        m_gold = newGold + '#' + words;
        PlayerPrefs.SetString("Gold", m_gold.ToString());
    }

    //인덱스도 같이 더하기 해줘야함
    public void AddGold(string newGold)
    {
        // m_gold = Math.Round(m_gold, 3);

        int index = Array.IndexOf(ShortScaleSymbolReference, mgoldSplit[1]);

        //if (index > ShortScaleSymbolReference.Length - 1)
        //{
        //    //배열 마지막단위까지 왔을때 해줄일
        //    //mgoldSplit[0] = mgoldSplit[0];
        //    mgoldSplit[1] = ShortScaleSymbolReference[index + 1];
        //}

        if (double.Parse(mgoldSplit[0]) >= 1000)
        {
            while (double.Parse(mgoldSplit[0]) > 1000)
            {
                double mgold = double.Parse(mgoldSplit[0]) / 1000;
                mgoldSplit[0] = mgold.ToString();
                ++index;
            }
            mgoldSplit[1] = ShortScaleSymbolReference[index];
        }

        StartCoroutine(CountingAddGold(newGold));

        SetGold(mgoldSplit[0], mgoldSplit[1]);
    }

    //현재 숫자부분 골드만 신경씀 인덱스 신경을 안써서 오류가있음
    IEnumerator CountingAddGold(string newGold)
    {
        string[] newGoldSplit = newGold.Split('#');
        int newGoldIndex = Array.IndexOf(ShortScaleSymbolReference, newGoldSplit[1]);
        int index = Array.IndexOf(ShortScaleSymbolReference, mgoldSplit[1]);
        double mgold = double.Parse(mgoldSplit[0]);
        double disGold=0;
        double duration = 3f;

        int ten = 1;
        for (int i = 0; i < index - newGoldIndex; i++)
        {
            ten *= 10;
        }
        //인덱스가 낮은 경우, 같은경우, 높은경우
        if (index < newGoldIndex)
        {
            disGold = (mgold/ten) + double.Parse(newGoldSplit[0]);
            mgoldSplit[1] = newGoldSplit[1];
        }
        else if (index == newGoldIndex)
        {
            disGold = mgold + double.Parse(newGoldSplit[0]);
        }
        else
        {
            disGold = mgold + (double.Parse(newGoldSplit[0]) / ten);
        }

        double offSet = disGold - mgold / duration;

        while (mgold < disGold)
        {
            mgold += offSet * Time.deltaTime;
            mgoldSplit[0] = mgold.ToString();
            yield return null;
        }
    }

    public void SubGold(string newGold)
    {
        string[] newGoldSplit = newGold.Split('#');
        double mgold = double.Parse(mgoldSplit[0]) - double.Parse(newGoldSplit[0]);
        int index = Array.IndexOf(ShortScaleSymbolReference, mgoldSplit[1]) - Array.IndexOf(ShortScaleSymbolReference, newGoldSplit[1]);
        //m_gold가 0아래로 내려가면 숫자,문자가 바뀜
        if (mgold <= 0)
        {
            mgold = mgold + 1000; //골드를 초기화시켜주고
        }
        mgoldSplit[0] = mgold.ToString();
        mgoldSplit[1] = ShortScaleSymbolReference[index];

        SetGold(mgoldSplit[0], mgoldSplit[1]);
    }

    public string GetGold()
    {
        return m_gold;
    }

    public double GetGoldPerClick()
    {
        return m_goldPerClick;
    }

    public void SetGoldPerClick(double newGoldPerClick)
    {
        m_goldPerClick = newGoldPerClick;
    }

    public void AddGoldPerClick(double newGoldPerClick)
    {
        m_goldPerClick += newGoldPerClick;
        SetGoldPerClick(m_goldPerClick);
    }

    public void LoadItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        itemButton.level = PlayerPrefs.GetInt(key + "_level");
        itemButton.currentCost = PlayerPrefs.GetString(key + "_cost", itemButton.startCurrentCost+'#');
        itemButton.goldPerSec = PlayerPrefs.GetString(key + "_goldPerSec", "0#");

        if (PlayerPrefs.GetInt(key + "_isPurchased") == 1)
        {
            itemButton.isPurchased = true;
        }
        else
        {
            itemButton.isPurchased = false;
        }
    }

    public void SaveItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key + "_level", itemButton.level);
        PlayerPrefs.SetString(key + "_cost", itemButton.currentCost);
        PlayerPrefs.SetString(key + "_goldPerSec", itemButton.goldPerSec);

        if (itemButton.isPurchased == true)
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 0);
        }
    }

    //모든 아이템들의 GoldPerSec
    public string GetGoldPerSec()
    {
        //1000이 넘어가면 인덱스 변경
        string goldPerSec = "0#";

        string[] goldPerSecSplit = goldPerSec.Split('#');

        int goldPerSecindex = Array.IndexOf(ShortScaleSymbolReference, goldPerSecSplit[1]);

        for (int i = 0; i < itemButtons.Length; i++)
        {
            string[] itemButtonSplit = itemButtons[i].goldPerSec.Split('#');
            //여기서 좀 헷갈리네
            //숫자부분에 저장
            goldPerSecSplit[0] = (int.Parse(goldPerSecSplit[0]) + float.Parse(itemButtonSplit[0])).ToString();
        }

        if (float.Parse(goldPerSecSplit[0]) >= 1000)
        {
            while(float.Parse(goldPerSecSplit[0]) > 1000)
            {
                goldPerSecSplit[0] = (float.Parse(goldPerSecSplit[0]) / 1000).ToString();
                ++goldPerSecindex;
            }
        }
        goldPerSec = goldPerSecSplit[0].ToString() + '#' + ShortScaleSymbolReference[goldPerSecindex];

        return goldPerSec;
    }

    public void SaveBuySatellite(SatelliteCell satelliteCell)
    {
        string key = satelliteCell.satelliteName;

        if (satelliteCell.isBuyStatellite == true)
        {
            PlayerPrefs.SetInt(key + "_isBuyStatellite", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isBuyStatellite", 0);
        }
    }

    public void LoadSaveBuySatellite(SatelliteCell satelliteCell)
    {
        string key = satelliteCell.satelliteName;

        if (PlayerPrefs.GetInt(key + "_isBuyStatellite") == 1)
        {
            satelliteCell.isBuyStatellite = true;
        }
        else
        {
            satelliteCell.isBuyStatellite = false;
        }
    }

    public void SaveParts()
    {
        PlayerPrefs.SetInt("haveParts", haveParts);
    }

    public void LoadParts()
    {
        haveParts = PlayerPrefs.GetInt("haveParts");
    }
    public void ClearKey()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("완료");
    }
}