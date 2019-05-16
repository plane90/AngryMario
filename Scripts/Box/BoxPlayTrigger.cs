using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayTrigger : MonoBehaviour
{
    public GameObject[] uiObject;


    private void Start()
    {
        foreach (var item in uiObject)
        {
            item.gameObject.SetActive(false);
        }
        //uiObject[0].gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            foreach (var item in uiObject)
            {
                item.gameObject.SetActive(true);
                Debug.Log("발동");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            foreach (var item in uiObject)
            {
                item.gameObject.SetActive(false);
                Debug.Log("꺼쪄어어엉");
            }

        }
    }
}
