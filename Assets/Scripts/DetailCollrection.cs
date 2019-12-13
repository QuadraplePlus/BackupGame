using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailCollrection : MonoBehaviour
{
    [SerializeField] Image image; //행성 이미지
    [SerializeField] Image icon; //아이콘 이미지
    [SerializeField] Text planetExplanationtext; // 행성 설명
    [SerializeField] Text planetName;
    [SerializeField] RectTransform textPanel; //행성설명판넬
    [SerializeField] Button button; // 돋보기 버튼

    public Sprite Image
    {
        get
        {
            return this.image.sprite;
        }
        set
        {
            this.image.sprite = value;
        }
    }

    public Sprite Icon
    {
        get
        {
            return this.icon.sprite;
        }
        set
        {
            this.icon.sprite = value;
        }
    }

    public string PlanetExplanationText
    {
        get
        {
            return this.planetExplanationtext.text;
        }
        set
        {
            this.planetExplanationtext.text = value;
        }
    }

    public string PlanetName
    {
        get
        {
            return this.planetName.text;
        }
        set
        {
            this.planetName.text = value;
        }
    }

    public float TextPanel
    {
        get
        {
            return this.textPanel.sizeDelta.y;
        }
        set
        {
            this.textPanel.sizeDelta = new Vector2(0, value);
        }
    }

    public void CancelClick()
    {
        Destroy(this.gameObject);
    }
}
