using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    //public float itemTimeSecond = 3600; //1시간 
    public float itemTimeSecond = 3600;

    public BuffItemCommon BuffItemCommon;

    public void BuyItem()
    {
        if(PlayerPrefs.HasKey("BuffTime"))
            itemTimeSecond += PlayerPrefs.GetFloat("BuffTime");
        PlayerPrefs.SetFloat("BuffTime", itemTimeSecond);
        BuffItemCommon.BuyBuffItem();
    }
}
