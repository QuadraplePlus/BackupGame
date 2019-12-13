using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggleButton : MonoBehaviour
{
    public int i;
    //버튼 순서대로 숫자 지정
    CameraManager cameraManager;

    public void toggle()
    {
       
        cameraManager = GetComponentInParent<CameraManager>();
        cameraManager.toggleCamera(i);
       


    }
}
