using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerSfx
{
    public AudioClip[] wick_reload;
}


//포탄 장전과 발사 준비하는것
public class CannonReload_Fire : MonoBehaviour
{
    public GameObject wick;                                                           //심지 객체
    public Transform bombSettingPosition;                                             //포탄 장전지점
    public GameObject firePos;                                                        //포탄 발사지점
   

    public PlayerSfx _playerSfx;


    private Animator settingBomb;                                                     //포탄이 장전되는 애니메이터를 저장할 변수
    private GameObject bomb;                                                          //콜라이더 감지된 대포객체 저장 변수
   
    //포탄 애니메이션
    private Animator anim;
    private readonly int hashInit = Animator.StringToHash("Init");
    private readonly int cannonOpenDoor = Animator.StringToHash("isCannonDoor");      //포탄문 열고 닫는 애니메이션
    private readonly int cannonReady = Animator.StringToHash("isCannonBombReady");    //포탄문안으로 포탄이 굴러 들어가는 애니메이션


    //심지 이펙트 이동 애니메이션
    private readonly int OnWickFire = Animator.StringToHash("OnWickFire");

    //파티클 저장 변수
    private ParticleSystem spark0;
    private ParticleSystem spark1;
    private ParticleSystem spark2;
    private ParticleSystem spark3;
    private ParticleSystem fire_exeEffect;
    private ParticleSystem smoke_bomb;
    private ParticleSystem smoke_fire;

    private float Speed = 10000.0f;                                                    //발사 에너지
   
    private bool isReloadComplete = false;                                            //장전 유무


    //private void Start()
    private void OnEnable()
    {
        //애니메이션 넣어줌
        anim = GetComponent<Animator>();

        isReloadComplete = false;
        if (bomb != null)
            Destroy(bomb);

        anim.SetTrigger(hashInit);
        anim.SetBool(cannonOpenDoor, false);

        //심지 파티클
        spark0 = wick.transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>();
        spark1 = wick.transform.GetChild(0).transform.GetChild(1).GetComponent<ParticleSystem>();
        spark2 = wick.transform.GetChild(0).transform.GetChild(2).GetComponent<ParticleSystem>();
        spark3 = wick.transform.GetChild(0).transform.GetChild(3).GetComponent<ParticleSystem>();
        WickParticle_Stop();

        smoke_fire = firePos.transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>();
        smoke_fire.Stop();

        //발사 폭발 타이클
        fire_exeEffect = firePos.transform.GetChild(0).GetComponent<ParticleSystem>();
        fire_exeEffect.Stop();
        
        
        //심지 애니메이션을 끄기
        wick.GetComponent<Animator>().SetBool(OnWickFire, false);
        
    }

    //포탄 감지, 재장전
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("CannonBall")&&!isReloadComplete)
        {
            
            bomb = other.gameObject;
            bomb.GetComponent<ToCrashWithBlock>().isFromMachine = true;
            Debug.Log("대포장전");

            bomb.GetComponent<TrailRenderer>().enabled = true;

            //대포알의 연기 파티클을 파티클 변수에 넣은다.
            smoke_bomb = bomb.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();


            //장전된 대포알의 컨트롤을 막기위해 해당 부분을 끈다.
            bomb.GetComponent<Draggable>().enabled = false;
            bomb.GetComponent<Rigidbody>().isKinematic = true;

            //포탄이 있는 위치를 장점지점으로 고정
            bomb.transform.position = bombSettingPosition.position;
            //bomb.GetComponent<ToCrashWithBlock>().isFromMachine = true;
            

           

            //대포문을 닫는다.
            anim.SetBool(cannonOpenDoor, true);

            //★★★★★firePos에 셋팅한 오디오컴퍼넌트를 만든다.API
            //(targetObj : 오디오소스 컴퍼넌트 생성 대상, soundClip : 오디오 클립, loopOn : loop활성화)
            SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.reloadingComplete_Sound,true);


            //심지 파티클 실행
            WickParticle_Play();

            //발사준비
            StartCoroutine(FirePos());
            
            
            
            
            


            //장전 완료를 실행
            isReloadComplete = true;
        }
    }

    //포탄 발사지점 이동
    public IEnumerator FirePos()
    {
        yield return new WaitForSeconds(1.5f);
       
        //포탄이 있는 위치를 발사지점으로 이동
        bomb.transform.position = firePos.transform.position;

        //대포알을 firePos 발사위치에 자식 오브젝트로 세팅해라
        bomb.transform.SetParent(firePos.transform);

        
    }

    //발사
    public IEnumerator Fire()
    {
        if (!isReloadComplete) Debug.Log("대포 장전이 안되있어요");
        else
        {
            wick.GetComponent<Animator>().SetBool(OnWickFire, true);          //심지 이동 애니메이션을 실행

           


            spark3.Stop();
            yield return new WaitForSeconds(1.0f);

            WickParticle_Stop();                                              //심지 파티클 중지

            


            //firePos지점 자식계층을 빼준다.
            bomb.transform.SetParent(null);
            //firePos.transform.GetChild(0).transform.SetParent(null);      

            //그전 꺼준 리디드바디를 켜준다
            bomb.GetComponent<Rigidbody>().isKinematic = false;

            //firePos.GetComponent<AudioSource>().Play();

            //포탄 발사
            float cob = 5;
            bomb.GetComponent<Rigidbody>().AddForce(transform.forward * Speed * cob);

            
            smoke_bomb.Play();                                                //포탄 발사 연기 파티클 실행
            smoke_fire.Play();


            //발사 될때만 콜라이더 크기를 좀더 키워서 판정 범위를 넓힌다.
            bomb.GetComponent<SphereCollider>().radius = +0.25f;
            bomb.transform.localScale += new Vector3(1.75f, 1.75f, 1.75f);
            //발사 폭발 파티클
            fire_exeEffect.Play();

            //★★★★★firePos에 셋팅한 오디오컴퍼넌트를 만든다.API
            //(targetObj : 오디오소스 컴퍼넌트 생성 대상, soundClip : 오디오 클립, loopOn : loop활성화) 
            SoundManager.Instance.AudioSetting(firePos, SoundManager.Instance.cannon_fireSound, false);

            //Destroy(bomb, 3.0f);
            StartCoroutine(bomb.GetComponent<ToCrashWithBlock>().SetCrashableAndDisappear());   //3초뒤 SetActiv(false)

            //발사되기 때문에 false로 변경
            isReloadComplete = false;
            anim.SetBool(cannonOpenDoor, false);
            
            wick.GetComponent<Animator>().SetBool(OnWickFire, false);          //심지 이동 애니메이션을 실행

        }


    }

    //심지 파티클 실행
    private void WickParticle_Play()
    {
        spark0.Play();
        spark1.Play();
        spark2.Play();
        spark3.Play();

        wick.GetComponent<AudioSource>().Play(); //심지 불꽃 사운드 실행
    }

    //심지 파티클 멈춤
    private void WickParticle_Stop()
    {
        spark0.Stop();
        spark1.Stop();
        spark2.Stop();
        spark3.Stop();

        wick.GetComponent<AudioSource>().Stop(); //심지 불꽃 사운드 멈춤
    }

    //private IEnumerator CannonDelete()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    bomb.De

    //}
}
    