using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCamera : MonoBehaviour
{

    public GameObject[] target; 
    //타겟 배열의 행성을 일정시간마다 자동으로 돌아가면서 카메라가 보여줌


    // Start is called before the first frame update
    void Start()
    { 
        //배열 지정

        ////
        // target = GameObject.FindGameObjectsWithTag("Star");
        //Transform[] TargetTransforms;
         
        //foreach(GameObject gameObject in target)
        //{
        //    Debug.Log(gameObject.transform);
        //   TargetTransforms[i] =  gameObject.transform;
            
        //}
        
        //for (int i = 0; i < target.Length; i++)
        //{
        //    TargetTransforms[i] = gameObject.transform;
        //    Debug.Log(TargetTransforms[i]);
        //}
    }
    
    IEnumerator AutoFollow()
    {

        // 자동으로 쫓아감


        yield return null;
    }


    // Update is called once per frame
    void Update()
    {


        
    }



}
