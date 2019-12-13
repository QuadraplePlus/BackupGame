using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class OffLineReword : MonoBehaviour
{
    public int adsPluse = 10;       //올려줄 % 양

    public string[] allGetMoney;       //지금 획득하고 있는 골드 총합

    public Button OffLineRewardbutton; //오프라인보상 획득 버튼
    public Text OffLineRewardbuttonText; //버튼 텍스트 변경

    public int adsTotalMints = 480; //버프아이템 오프라인 상한선 60*8=480분

    public int limitMinuts = 240; //일반 오프라인 상한선 60*4 =240분

    System.TimeSpan conpareTime; //접속 종료후 시간이 얼마나 흘렀는지

    bool isGetOffReword =false; //보상받기 버튼을 눌렀는지 안 눌렀는지

    float loadConpareTime; //로드된 conpareTime

    private void Start()
    {
        offCheckLoad();
        AppTime();

        if (loadConpareTime >= 2)
            OffLineRewardbutton.gameObject.SetActive(true);
        else
            OffLineRewardbutton.gameObject.SetActive(false);
    }

    private void Update()
    {
        allGetMoney = DataController.GetInstance().GetGoldPerSec().Split('#'); //획득하고 있는 골드 총합을 가져와서
        //몇시간이 흘렀는지 체크해서 * 시간
    }

    //오프라인 보상 클릭식
    public void OffReward()
    {
        double adsPluseGetMoney; //계산값
        string adsPluseGetMoneyString; //문자열
        //버프를 샀다면
        if (BuffItemCommon.adsPhrchased)
        {
            if (loadConpareTime <= adsTotalMints) //버프효과로 상한선 증가
            {
                //전체값 x 퍼센트 /100
                adsPluseGetMoney = (double.Parse(allGetMoney[0]) + (double.Parse(allGetMoney[0]) * adsPluse / 100)) * (int)loadConpareTime;
                adsPluseGetMoneyString = adsPluseGetMoney.ToString() + '#' + allGetMoney[1]; // 어떤 인덱스를 어떻게 넣어줘야 하지??
                OffLineRewardbuttonText.text = adsPluseGetMoneyString;
                DataController.GetInstance().AddGold(adsPluseGetMoneyString);
            }

            else if (loadConpareTime > adsTotalMints)
            {
                adsPluseGetMoney = (double.Parse(allGetMoney[0]) + (double.Parse(allGetMoney[0]) * adsPluse / 100)) * adsTotalMints;
                adsPluseGetMoneyString = adsPluseGetMoney.ToString() + '#' + allGetMoney[1];
                OffLineRewardbuttonText.text = adsPluseGetMoneyString;
                DataController.GetInstance().AddGold(adsPluseGetMoneyString);
            }
        }
        //안샀다면
        else
        {
            string allGetMoneyString;
            if (loadConpareTime <= limitMinuts)
            {
                allGetMoneyString = (double.Parse(allGetMoney[0]) * (int)loadConpareTime).ToString() + '#' + allGetMoney[1];
                OffLineRewardbuttonText.text = allGetMoneyString;
                DataController.GetInstance().AddGold(allGetMoneyString);
            }
            else if (loadConpareTime > limitMinuts)
            {
                allGetMoneyString = (double.Parse(allGetMoney[0]) * limitMinuts).ToString();
                OffLineRewardbuttonText.text = allGetMoneyString;
                DataController.GetInstance().AddGold(allGetMoneyString);
            }
        }
        isGetOffReword = true;
        OffCheck();
        OffLineRewardbutton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
        Debug.Log("종료 시간 : " + System.DateTime.Now.ToString());
    }

    public void AppTime()
    {
        string lastTime = PlayerPrefs.GetString("SaveLastTime");
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        conpareTime = System.DateTime.Now - lastDateTime;

        //conpareTime.TotalMinutes += PlayerPrefs.GetFloat("conpareTime");

        Debug.Log("실행 시간 : " + System.DateTime.Now.ToString());
        Debug.LogFormat("게임 종료 후, {0}분 지났습니다.", conpareTime.TotalMinutes);

        loadConpareTime = (float)(conpareTime.TotalMinutes) + loadConpareTime;
        Debug.LogFormat("누적 시간(분)은, {0}분 입니다.", loadConpareTime);
    }
    public void OffCheck()
    {
        if (isGetOffReword == true)
        {
            PlayerPrefs.SetFloat("conpareTime", 0f);
            PlayerPrefs.SetInt("isGetOffReword", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("conpareTime", (float)conpareTime.TotalMinutes);
            PlayerPrefs.SetInt("isGetOffReword", 0);
        }
    }

    public void offCheckLoad()
    {
        if (PlayerPrefs.GetInt("isGetOffReword") == 1)
        {
            loadConpareTime = PlayerPrefs.GetFloat("conpareTime");
            isGetOffReword = true;
        }
        else
        {
            loadConpareTime = PlayerPrefs.GetFloat("conpareTime");
            isGetOffReword = false;
        }
    }
}
