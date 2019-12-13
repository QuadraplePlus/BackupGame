using BayatGames.SaveGameFree.Serializers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UranusControll : MonoBehaviour
{
    [SerializeField] double Price;
    [SerializeField] Button OpenButton;
    [SerializeField] GameObject planet;
    [SerializeField] Text StateText;

    public static bool isOpenSun = false;

    [SerializeField] string prefsName;
    int i;

    void Start()
    {
        CheckPrefs(prefsName);
    }
    protected virtual void Update()
    {
        if (OpenButton != null)
        {
            if (Price <= double.Parse(DataController.GetInstance().GetGold()) && OpenButton != null)
            {
                OpenButton.interactable = true;
            }
            else
            {
                OpenButton.interactable = false;
            }
        }
    }

    public void PlanetOpen()
    {
        i++;
        Debug.Log("클릭");
        DataController.GetInstance().SubGold(Price.ToString());
        CreatePlanet();
        Destroy(OpenButton.gameObject);
        PlayerPrefs.SetInt(prefsName, i);
    }
    private void CreatePlanet()
    {
        isOpenSun = true;
        planet.SetActive(true);
        OpenButton.interactable = false;
        StateText.text = "해 금 완 료";
    }
    private void CheckPrefs(string prefsName)
    {
        if (PlayerPrefs.GetInt(prefsName) > 0)
        {
            CreatePlanet();
            if (OpenButton != null)
            {
                Destroy(OpenButton.gameObject);
            }
        }
    }
}
