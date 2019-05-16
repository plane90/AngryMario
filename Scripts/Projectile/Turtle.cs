using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : ToDamageEnemy
{
    private Collider _collider;

    private bool isHit = false;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null && !isHit)
        {
            isHit = true;
            StartCoroutine(Disappear());
        }
        else
        {
            return;
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
