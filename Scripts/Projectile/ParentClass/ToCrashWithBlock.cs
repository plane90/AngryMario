using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
public class ToCrashWithBlock : MonoBehaviour, IPointerClickHandler
{
    private Rigidbody _rigidbody;
    //private SphereCollider _colliderTrigger;    //블록의 trigger 감지
    private MeshCollider _colliderCollision;    //블록의 collision 감지
    //public float radiusOfTriggerCollider;
    private bool isPlayingSound = false;

    public bool isFromMachine = false;

    void Start()
    {
        InitComponent();
        
        //_rigidbody.AddForce(Vector3.forward * 1000.0f);
    }

    /// <summary>
    /// triggerCollider 생성 및 컴포넌트 속성 초기화.
    /// </summary>
    private void InitComponent()
    {
        //_colliderTrigger = this.gameObject.AddComponent<SphereCollider>();
        _colliderCollision = GetComponent<MeshCollider>();
        //_colliderTrigger = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _colliderCollision.convex = true;
        _colliderCollision.isTrigger = false;
        //_colliderTrigger.radius = radiusOfTriggerCollider;
        //_colliderTrigger.isTrigger = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }
    
    /// <summary>
    /// 충돌 후에는 더이상 trigger 감지를 하지 않습니다.
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Block>())
        { 
            //해당 컴퍼넌트를 검색하여 값 적용
            if(this.gameObject.GetComponent<CannonBall>())
            {
                //충돌 지점 파티클 만들기(collision , creat : 생성할 파티클)
                ParticleManager.Instance.CreatParticle(collision, ParticleManager.Instance.brickBomb1);
                //충돌지점 파티클 만들기-지연시키고 싶다면이걸로(collision , creat : 생성할 파티클, waitTime : 지연시간)
                StartCoroutine(ParticleManager.Instance.Wait_CreatParticle(collision, ParticleManager.Instance.brickBomb2, 0.2f));
                if(!isPlayingSound)
                    SoundManager.Instance.AudioSetting(collision.gameObject, SoundManager.Instance.attakbrick, false);
                isPlayingSound = true;
            }
            else
            {
                //충돌 지점 파티클 만들기(collision , creat : 생성할 파티클)
                ParticleManager.Instance.CreatParticle(collision, ParticleManager.Instance.brickStone1);
                //충돌지점 파티클 만들기-지연시키고 싶다면이걸로(collision , creat : 생성할 파티클, waitTime : 지연시간)
                StartCoroutine(ParticleManager.Instance.Wait_CreatParticle(collision, ParticleManager.Instance.brickStone2,0.1f));
                if (!isPlayingSound)
                    SoundManager.Instance.AudioSetting(collision.gameObject, SoundManager.Instance.attakbrick, false);
                isPlayingSound = true;
            }
            
            //StartCoroutine(Disappear());
        }
    }

    //private IEnumerator Disappear()
    public IEnumerator SetCrashableAndDisappear()
    {
        isFromMachine = true;
        yield return new WaitForSeconds(3f);
        _colliderCollision.enabled = false;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        _colliderCollision.enabled = true;
        Init();
    }

    private void Init()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;
        if(this.gameObject.GetComponent<CannonBall>())
            this.gameObject.transform.localScale = Vector3.one * 1.5f;
        isFromMachine = false;
        isPlayingSound = false;
        if (GetComponent<Draggable>())
        {
            GetComponent<Draggable>().enabled = true;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.gameObject.GetComponent<Stone>())
        {
            //Debug.Log("Stone");
            SetRockPosition.Instance.SetPosition(this.gameObject);
        }
    }
}
