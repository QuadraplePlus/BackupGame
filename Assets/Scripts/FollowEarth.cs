using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEarth : MonoBehaviour
{
    [SerializeField] Transform target;
    // Update is called once per frame
    private void Start()
    {
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 vec = target.position - transform.position;
            vec.Normalize();
            Quaternion r = Quaternion.LookRotation(vec);
            transform.rotation = r;
            Debug.Log("회전");
        }
    }
}
