using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleKeplerOrbits;

public class Satallite : MonoBehaviour
{
    //스크립트에 부모행성이 누구인지
    //핸들은 어떤 핸들인지 부여해줘야됨

    //예제 부모행성
    [SerializeField] GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        KeplerOrbitMover[] a = FindObjectsOfType<SimpleKeplerOrbits.KeplerOrbitMover>();
        //포지션 정하고
        //parent.transform.position = new Vector3(0, 0, -1.856f);
        //this.transform.position = new Vector3(0, 0, 2.91f);

        //서클
        for(int i=0; i<a.Length; i++)
        {
            a[i].SetAutoCircleOrbit();
            a[i].InverseOrbit();
            a[i].InversePositionRelativeToAttractor();
            a[i].InverseOrbit();
            a[i].ResetOrbit();
        } 
    }
}
