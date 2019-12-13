using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject Popup;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject CashShop;
    [SerializeField] GameObject Collection;
    [SerializeField] GameObject Setting;

    bool isOpen;

    private void Start()
    {
        Application.targetFrameRate = 60;
        isOpen = false;
    }
    public void OnClickClose()
    {
        Popup.SetActive(false);
    }
    public void OnClickMenu()
    {
        isOpen = !isOpen;

        if (!isOpen)
        {
            MainMenu.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }
    public void OnClickOpenTabMenu()
    {
        Popup.SetActive(true);
    }
    public void OnClickOpenCashShop()
    {
        CashShop.SetActive(true);
    }
    public void OnClickOpenCollection()
    {
        Collection.SetActive(true);
    }
    public void OnClickCloseCashShop()
    {
        CashShop.SetActive(false);
    }
    public void OnClickCloseCollection()
    {
        Collection.SetActive(false);
    }
    public void OnClickOpenSetting()
    {
        Setting.SetActive(true);
    }
    public void OnClickCloseSetting()
    {
        Setting.SetActive(false);
    }
}
