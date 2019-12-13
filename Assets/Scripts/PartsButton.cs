using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클릭시 파편이 증가할 버튼
public class PartsButton : MonoBehaviour
{
    public void PartOnClick()
    {
        int random = Random.Range(1,5);
        DataController.haveParts += random;

        Debug.Log(DataController.haveParts);
        DataController.GetInstance().SaveParts();
    }
}
