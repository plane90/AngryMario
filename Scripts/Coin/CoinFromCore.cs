using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFromCore : MonoBehaviour
{
    public float periodOfTime = 4.0f;

    private void Start()
    {
        StartCoroutine(DestroyCoin());
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 360.0f * Time.deltaTime);
    }

    private IEnumerator DestroyCoin()
    {
        yield return new WaitForSeconds(periodOfTime);
        Destroy(this.gameObject);
    }
}
