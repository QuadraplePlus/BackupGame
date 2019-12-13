using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class CameraManager : MonoBehaviour
{
    public GameObject SubCamera;
    public GameObject Maincamera;
    public GameObject[] target; 
    Transform firstForm;
    Transform secForm;
    bool toggle=false;

    int i;
    Vector3 vec;

 


    // Start is called before the first frame update
    void Start()
    {
        Maincamera =GameObject.Find("Main Camera");

        OnOff();
    }

    public void toggleCamera(int i)
    {
        this.i = i; 
        toggle = !toggle;

        OnOff();

        switch (i) //행성마다 카메라의 좌표를 다르게 설정
        {
            //배열 0번은 태양으로 해야 함

            case 0:
                vec = new Vector3(0, 0,-130);
                break;
            case 6:
                vec = new Vector3(0, 0, -50);
                break;


            default: vec = new Vector3(0, 1, -5);
                break;

        }
        if (toggle == false)
        {
          GameObject.Find("CameraResetButton").GetComponent<CameraReset>().MainCameraReset();
        }


    } 

   void OnOff()
    {

        SubCamera.GetComponent<Camera>().enabled = toggle;
        SubCamera.GetComponent<AudioListener>().enabled = toggle;
        Maincamera.GetComponent<Camera>().enabled = !toggle;
        Maincamera.GetComponent<AudioListener>().enabled = !toggle; 


    }


    public void followLookAt( )
    {
            
           SubCamera.transform.LookAt(target[i].transform.position);
       
    }


      void Update()
    {

        //타겟에 행성 오브젝트의 배열을 넣고 해당 버튼을 누르면 타겟배열의 행성으로 이동. 다시 누르면 최초 화면으로 복귀

        if (toggle)
        { 
            
            SubCamera.transform.position = target[i].transform.position+vec;
            followLookAt( );

           

        }
        
    }
}
