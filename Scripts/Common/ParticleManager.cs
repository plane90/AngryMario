using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    static ParticleManager instance;
    public static ParticleManager Instance
    {
        get
        {
            if (!instance)//인스턴스 즉,싱글턴이 없으면 만들고
            {
                instance = GameObject.Find("Manager").GetComponent<ParticleManager>();
            }
            return instance; //있으면 그냥 있는거 넘겨줌
        }
    }


    public GameObject brickBomb1;        //캐논 부딪칠때1
    public GameObject brickBomb2;        //캐논 부딪칠때2
    public GameObject brickStone1;       //돌 부딪칠때1
    public GameObject brickStone2;       //돌 부딪칠때2

    public GameObject waterPaticle;      //돌 물에 빠질때
    



    //충돌지점 파티클 만들기
    public void CreatParticle(Collision collision, GameObject creat)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //파티클 생성
        Instantiate(creat, contact.point, rot);
    }

    //충돌지점 파티클 만들기-지연시키고 싶다면이걸로
    public IEnumerator Wait_CreatParticle(Collision collision, GameObject creat, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //파티클 생성
        Instantiate(creat, contact.point, rot);
    }

    //충돌지점 파티클 만들기
    public void TriggerCreatParticle(Collision other, GameObject creat)
    {
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        //파티클 생성
        Instantiate(creat, contact.point, rot);
    }
}


