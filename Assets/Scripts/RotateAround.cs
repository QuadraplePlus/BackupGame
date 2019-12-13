using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Start()
    {  
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.localPosition, Vector3.up , 1f * Time.deltaTime);
    }
}   
