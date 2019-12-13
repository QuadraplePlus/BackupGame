
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStick : MonoBehaviour
{
    public GameObject Player; //player 카메라 (메인카메라)
    Transform Playertransform;
    public Transform Stick;
    [Range(10,100)] public float   speed;


    private Vector3 fPos; //조이스틱 처음위치
    private Vector3 jVec; //조이스틱벡터
    private Vector3 rVec;

    private float Radius; //조이스틱 배경 반지름
    [HideInInspector] public bool MoveFlag;
   
    void Start()
    {

   //creates that script data type
    

        Playertransform = Player.GetComponent<Transform>();
        
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        fPos = Stick.transform.position;

        float Can = GameObject.Find("JoyStick1").transform.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
        MoveFlag = false;



      

    }

    private void Drag(BaseEventData eventData)
    {
        MoveFlag = true;
         

        PointerEventData Data = eventData as PointerEventData;
        Vector3 Pos = Data.position;

        jVec = (Pos - fPos).normalized;
        rVec = jVec;
        float Distance = Vector3.Distance(Pos, fPos);

        if (Distance < Radius)
            Stick.position = fPos + jVec * Distance;
        else
            Stick.position = fPos + jVec * Radius;


        //조이스틱 동작시 카메라 드래그로 이동이 되지 않게 함

        PanZoomON(false);

    }

    private void DragEnd()
    {

        Stick.position = fPos;
        jVec = Vector3.zero;
        MoveFlag = false; 
        PanZoomON(true);
    }

    private void PanZoomON(bool isbool)
    {
        //카메라의 panzoom 스크립트 온 오프
        Player.GetComponent<Dossamer.PanZoom.PanZoomBehavior>().enabled = isbool;

    }

    private void PlayerRotate(bool MoveFlag)
    {
        if (MoveFlag)
        {
            Playertransform.transform.Rotate(new Vector3(-jVec.y, jVec.x, 0) * Time.smoothDeltaTime * speed);


        }

    }


    // Update is called once per frame
    void Update()
    {

        PlayerRotate(MoveFlag);

    }
}
