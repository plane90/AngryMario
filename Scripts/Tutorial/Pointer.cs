using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 360.0f * Time.deltaTime);
    }
}
