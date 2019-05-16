using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //싱글톤 객체를 설정
    //싱글톤은 씬을 여러개 만들다 보면 문제가 생긴다. 그 싱글턴 한개만 
    
    static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)//인스턴스 즉,싱글턴이 없으면 만들고
            {
                /*
                GameObject container = new GameObject("SoundManager");
                instance = container.AddComponent(typeof(SoundManager)) as SoundManager;
                DontDestroyOnLoad(container); //씬이 다른씬으로 로드되더라도 이 싱글턴을 지우지 말아라라는것
                */
                instance = GameObject.Find("Manager").GetComponent<SoundManager>();
            }
            return instance; //있으면 그냥 있는거 넘겨줌
        }
    }
    

    //공용 : 배경음
    public List<AudioClip> bgSound;

    //공용 : 장착완료
    public AudioClip reloadingComplete_Sound;   //장착완료 소리

    //대포 효과음
    public AudioClip cannon_fireSound;          //대포 발사 소리
    public AudioClip wick_fire;                 //심지 불 붙는 소리
    public AudioClip waterSound;                //물 사운
    public AudioClip attakbrick;                //벽에 부딪힐때

    public AudioClip clearStage;                //클리어 소리
    public AudioClip endGame;                   //끝나는 소리
    public AudioClip timeOver;                  //타임 오버 소리

    // 투석기 효과음
    public AudioClip rotateCatapult;            //캐터펄트 회전
    public AudioClip shotRock;                  //캐터펄트 발사
    

    //오디오 재생기
    private AudioSource BGMsource;
    private AudioSource sfxSound;

    //무조건 매니저 활성화 되면 재생되는 음악 부분
    private void OnEnable()
    {
        //오디오 컴퍼넌트 셋팅
        BGMsource = gameObject.AddComponent<AudioSource>();
        BGMsource.playOnAwake = false;
        BGMsource.loop = true;

        if (bgSound != null)
        foreach (var item in bgSound)
        {
            BGMsource.PlayOneShot(item, 0.1f);
        }
    }

    //셋팅된 오디오 컴퍼넌트 추가(오디오 컴퍼넌트 넣을 오브젝트 , 재생시킬 사운드, loop값 bool값으로 설정)
    public void AudioSetting(GameObject targetObj, AudioClip soundClip, bool loopOn)
    {
        AudioSource obj = targetObj.AddComponent<AudioSource>();
        obj.playOnAwake = false;
        obj.loop = loopOn;
        obj.PlayOneShot(soundClip, 0.1f);
    }

    public void AudioSet(GameObject targetObj, AudioClip soundClip, bool loopOn)
    {
        if (targetObj.GetComponentInChildren<AudioSource>() != null)
        {
            AudioSource obj = targetObj.GetComponentInChildren<AudioSource>();
            if (obj.isPlaying)
            {
                obj.Stop();
            }
            obj.PlayOneShot(soundClip);
        }
        else
        {
            AudioSource obj = targetObj.AddComponent<AudioSource>();
            obj.playOnAwake = false;
            obj.loop = loopOn;
            obj.PlayOneShot(soundClip);
        }
    }
}
