using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    private Transform throwObjPos;

    public CatapultCtrl _catapultCtrl;

    private bool isReady = false;
    public bool IsReady
    {
        get { return isReady; }
        set { isReady = value; }
    }

    private GameObject throwObj;
    public GameObject ThrowObj
    {
        get { return throwObj; }
        set { throwObj = value; }
    }
    private Rigidbody throwObjRb;
    public Rigidbody ThrowObjRb
    {
        get { return throwObjRb; }
        set { throwObjRb = value; }
    }

    void Start()
    {
        throwObjPos = GetComponent<Transform>();
    }

    private void Update()
    {
        //if (throwObjPos.childCount == 0 && _catapultCtrl.isFire && isReady)
        //{
            
        //}
    }

    public void SetRock(GameObject gameObject)
    {
        throwObj = gameObject;
        throwObjRb = throwObj.GetComponent<Rigidbody>();
        throwObj.transform.SetParent(throwObjPos, true);
        isReady = true;
    }

    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.CompareTag("Rock") && !isReady)
    //    {
    //        throwObj = coll.gameObject;
    //        throwObjRb = throwObj.GetComponent<Rigidbody>();
    //        throwObj.transform.SetParent(throwObjPos);
    //        isReady = true;
    //    }
    //}

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("Rock") && isReady)
        {
            throwObj.transform.SetParent(null);
            throwObj = null;
            throwObjRb = null;
            isReady = false;
        }
    }

    public void Fire()
    {
        throwObj.transform.SetParent(null);
        throwObjRb.AddForce(Vector3.forward, ForceMode.VelocityChange);
    }
}
