using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//위성 fill 스크립트
public class SatelliteCell : MonoBehaviour
{
    public string satelliteName;
    //StoneIBar 게임오브젝트
    [SerializeField] GameObject stoneINeed;
    [SerializeField] Text haveText;
    [SerializeField] Text maxText;
    [SerializeField] int maxParts;

    Image stonelImage;

    [HideInInspector] public bool isBuyStatellite;
    Button buttonONOFF;
    void Start()
    {
        DataController.GetInstance().LoadSaveBuySatellite(this);
        stonelImage = stoneINeed.GetComponent<Image>();
        buttonONOFF = GetComponent<Button>();

        maxText.text = maxParts.ToString();
        buttonONOFF.interactable = false;
    }

    void Update()
    {
        stonelImage.fillAmount = (float)DataController.haveParts / (float)maxParts;
        haveText.text = DataController.haveParts.ToString();
        if(isBuyStatellite==false)
        {
            if (DataController.haveParts >= maxParts)
            {
                buttonONOFF.interactable = true;
            }
        }
        else
        {
            buttonONOFF.interactable = false;
        }

        if (DataController.haveParts <= maxParts)
        {
            buttonONOFF.interactable = false;
        }
    }

    public void BuyStatellite()
    {
        if(DataController.haveParts >= maxParts)
        {
            DataController.haveParts -= maxParts;
            DataController.GetInstance().SaveParts();
        }
        //Destroy(this);
        isBuyStatellite = true;
        DataController.GetInstance().SaveBuySatellite(this);
    }
}
