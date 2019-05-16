using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;

    void Start()
    {
        //camTr = Camera.main.transform;                   //카메라 위치
        tr = GetComponent<Transform>();                  //현재 스크립트의 오브젝트 위치
    }

    void LateUpdate()
    {
        tr.LookAt(Camera.main.transform.position + Vector3.right * (-0.6f));
    }
}
