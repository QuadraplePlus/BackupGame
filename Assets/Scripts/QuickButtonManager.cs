using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickButtonManager : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void QuickUpgrade()
    {
        ItemButton itemButton = GetComponent<ItemButton>();
        itemButton.PurchasedItem();
        button.interactable = false;
    }
}
