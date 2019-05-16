using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    public Animator _animator;
    public GameObject firePos;
    public Collider _colliderWick;

    private bool canOpen = false;
    private bool isLoaded = false;
    private bool isLit = false;
    private Collider _colliderDoor;
    private GameObject cannonBall;
    private Rigidbody _rigidbodyBall;
    private readonly int hashOpen = Animator.StringToHash("Open");
    private readonly int hashClose = Animator.StringToHash("Close");
    private readonly int hashLight = Animator.StringToHash("Light");


    private void Start()
    {
        canOpen = true;
        _colliderDoor = GetComponent<Collider>();
        _colliderWick = _colliderWick.GetComponent<Collider>();
        _colliderWick.enabled = false;
        AnimEvent.OnFire += Fire;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLoaded)
        {
            if(!isLit)
                LightWick();
            return;
        }
        Debug.Log("OnTriggerEnter");
        //if (canOpen == true)
        //{
        //    _animator.SetTrigger(hashOpen);
        //    canOpen = !canOpen;
        //}
        //else
        //{
        //    _animator.SetTrigger(hashClose);
        //    canOpen = !canOpen;
        //}
        if(canOpen)
        {
            StartCoroutine(WaitToLoadBall());
        }
        else if(other.CompareTag("BALL"))
        {
            isLoaded = true;
            LoadBall(other.gameObject);
        }

    }

    public IEnumerator WaitToLoadBall()
    {
        _animator.SetTrigger(hashOpen);
        canOpen = false;

        yield return new WaitForSeconds(3.0f);

        if(!isLoaded)
        { 
            _animator.SetTrigger(hashClose);
            canOpen = true;
        }
    }

    private void LoadBall(GameObject ball)
    {
        cannonBall = ball;
        _rigidbodyBall = cannonBall.GetComponent<Rigidbody>();
        Debug.Log("LoadingBall");
        _animator.SetTrigger(hashClose);
        _colliderDoor.enabled = false;
        cannonBall.SetActive(false);
        cannonBall.SetActive(true);
        cannonBall.transform.position = firePos.transform.position;
        _colliderWick.enabled = true;
    }
    
    private void LightWick()
    {
        _colliderWick.enabled = false;
        _animator.SetTrigger(hashLight);
        isLit = true;
        Debug.Log("Light Wick");
    }

    private void Fire()
    {
        Debug.Log("Fire !!");
        _rigidbodyBall.isKinematic = false;
        _rigidbodyBall.AddForce(Vector3.forward * 1000.0f);
        Reset();
    }

    private void Reset()
    {
        isLoaded = false;
        isLit = false;
        canOpen = true;
        _colliderDoor.enabled = true;
        _colliderWick.enabled = false;
    }

}   
