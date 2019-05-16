using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireCtrl : MonoBehaviour
{
    private Collider _colliderLoader;

    private void Start()
    {
        _colliderLoader = GetComponent<Collider>();
        _colliderLoader.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Loader");
    }
}
