using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    Transform maincamera;
   public void MainCameraReset()
    {
        maincamera = GameObject.Find("Main Camera").transform;

        maincamera.position = new Vector3(-140,74,-195);
        maincamera.rotation = Quaternion.Euler(6,11,-5);
        //GetComponentInParent<CameraManager>().b/
    }
}
