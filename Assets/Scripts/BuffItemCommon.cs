using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffItemCommon : MonoBehaviour
{
    public static bool adsPhrchased = false;

    float playGameTime; //게임플레이 시간

    [HideInInspector] public float buffItemTime; //버프아이템 총 시간

    private void Start() {
        adsPhrchased = Convert.ToBoolean(PlayerPrefs.GetInt("BuffItemON"));
        buffItemTime = PlayerPrefs.GetFloat("BuffTime");

        if(adsPhrchased)
        {
            playGameTime = PlayerPrefs.GetFloat("PlayTime");
        } 
    }

    private void Update() {
        if(adsPhrchased)
        {
            playGameTime += Time.deltaTime;   
            //playGameTime = Time.time; //값이 오름

            BuffTime(buffItemTime);
        }
    }
    private void OnApplicationQuit() {
        if(adsPhrchased)
        {
            PlayerPrefs.SetFloat("PlayTime", playGameTime);
        }
    }

    //구입한 버프 시간에 따라 조건 
    public void BuffTime(float time)
    {
        if(playGameTime > time) //버프시간보다 플레이시간이더 크면
        {
            RemoveBuffItem();
        }
    }

    public void BuyBuffItem()
    {
        adsPhrchased = true;
        PlayerPrefs.SetInt("BuffItemON", 1);
        buffItemTime = PlayerPrefs.GetFloat("BuffTime");
        
        Debug.Log("구입완료");
    }

    public void RemoveBuffItem()
    {
        adsPhrchased = false;
        PlayerPrefs.SetInt("BuffItemON", 0);
        playGameTime = 0;
        PlayerPrefs.SetFloat("BuffTime", 0);

        Debug.Log("버프제거완료");
    }
}
