using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public Collider _colliderDoor;
    public Collider _colliderLoader;

    public delegate void FireBallHandler();
    public static event FireBallHandler OnFire;

    public void ColliderToggle()
    {
        //Debug.Log("done");
        _colliderDoor.enabled = !_colliderDoor.enabled;
    }

    public void LoadToggle()
    {
        //_colliderLoader.enabled = !_colliderLoader.enabled;
        ////if (OnLoadBall != null)
        ////    OnLoadBall(_colliderLoader.enabled);
        //OnLoadBall?.Invoke(_colliderLoader.enabled);
    }

    public void Fire()
    {
        Debug.Log("Fire");
        if (OnFire != null)
            OnFire();
    }
}
