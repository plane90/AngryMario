using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(obj, transform.position, transform.rotation);
            //Vector3.back;
        }
    }
}
