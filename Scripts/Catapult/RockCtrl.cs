using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCtrl : MonoBehaviour
{
    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("ThrowObjectCage") && !GetComponent<ToCrashWithBlock>().isFromMachine)
        {
            Debug.Log("히히삭제1!");
            this.gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //StartCoroutine(DeActive());
        }
    }

    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(0.0f);
        this.gameObject.SetActive(false);
    }
}
