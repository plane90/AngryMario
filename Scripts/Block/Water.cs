using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //private void OnTriggerEnter(Collision other)
    //{
        
    //    if(other.gameObject.CompareTag("Rock")|| other.gameObject.CompareTag("CannonBall"))
    //    {



    //        Debug.Log("빠져 버렸구만");
    //        ParticleManager.Instance.TriggerCreatParticle(other, ParticleManager.Instance.waterPaticle);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("CannonBall"))
        {
            Debug.Log("빠져 버렸구만");
            ParticleManager.Instance.TriggerCreatParticle(collision, ParticleManager.Instance.waterPaticle);
            collision.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            SoundManager.Instance.AudioSetting(collision.gameObject, SoundManager.Instance.waterSound, false);
        }
    }

    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.CompareTag("Rock") || coll.gameObject.CompareTag("CannonBall"))
    //    {
    //        Debug.Log("빠져 버렸구만");
    //        ParticleManager.Instance.TriggerCreatParticle(coll, ParticleManager.Instance.waterPaticle);
    //    }
    //}
}
