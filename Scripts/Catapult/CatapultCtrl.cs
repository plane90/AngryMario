using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultCtrl : Controllable                // Controllable을 상속받은 클래스 CataputlCtrl
{
    private Transform catapultTr;                       // 자신의 트랜스폼을 받아옴
    private Animator catapultAnim;                      // 자신의 애니메이터 컴포넌트를 받아옴

    private float spoonRot = 0.0f;                      // 투석기 애니메이션의 값을 컨트롤 하기 위한 스푼 회전 값.
    private float rotSpeed = 0.5f;                      // 투석기 좌우 회전속도
    private float spoonRotSpeed = 0.4f;                 // 스푼 상하 회전속도
    private float MaxShotPower = 25.0f;                 // 투석기 투사체 발사 맥스 파워
    private float shotPower;                            // 스푼 상하 각도에 따른 발사 파워 = MaxShotPower * AngleChangeValue
    private bool isFire = false;                        // 발사 여부를 결정 해줄 boolean.

    private readonly int hashAngle = Animator.StringToHash("AngleChangeValue");     // 스푼 회전 애니메이션을 컨트롤 하는 애니메이션 파라미터의 인덱스 값.
    private readonly int hashFire = Animator.StringToHash("Fire");                  // 투석기 발사 애니메이션을 컨트롤 하는 애니메이션 파라미터의 인덱스 값.

    public ObjectHold objHold;                          // 돌이 스푼에 붙어서 같이 움직이게 하기 위한 홀드 포지션

    private bool soundIsPlaying = false;
    
    private void Awake()
    {
        catapultTr = GetComponent<Transform>();         // 캐시처리 1
        catapultAnim = GetComponent<Animator>();        // 캐시처리 2
    }

    public override void OnCtrl(ButtonType type)
    {
        PlaySound(type);

        switch (type)
        {
            case ButtonType.Left:
                if (catapultTr.localRotation.y <= -0.3827f) return;     // 투석기의 회전각도를 제한 하기 위해 세팅.
                catapultTr.Rotate(Vector3.up, -rotSpeed);               // 
                break;

            case ButtonType.Right:
                if (catapultTr.localRotation.y >= 0.3827f) return;
                catapultTr.Rotate(Vector3.up, +rotSpeed);
                break;

            case ButtonType.Up:
                if (spoonRot <= 0)
                {
                    spoonRot = 0;
                    return;
                }
                spoonRot -= spoonRotSpeed * Time.deltaTime;
                catapultAnim.SetFloat(hashAngle, spoonRot);
                shotPower = Mathf.Clamp(MaxShotPower * catapultAnim.GetFloat(hashAngle), 0.0f, MaxShotPower);
                break;

            case ButtonType.Down:
                isFire = false;
                if (spoonRot >= 1)
                {
                    spoonRot = 1;
                    return;
                }
                spoonRot += spoonRotSpeed * Time.deltaTime;
                catapultAnim.SetFloat(hashAngle, spoonRot);
                shotPower = Mathf.Clamp(MaxShotPower * catapultAnim.GetFloat(hashAngle), 0.0f, MaxShotPower);
                break;

            case ButtonType.Shot:
                if (!isFire)
                {
                    isFire = true;
                    catapultAnim.SetTrigger(hashFire);
                    spoonRot = 0.0f;
                    catapultAnim.SetFloat(hashAngle, spoonRot);
                }
                break;
        }
    }

    private void ThrowObj()
    {
        //objHold.ThrowObj.GetComponent<ToCrashWithBlock>().isFromMachine = true;
        StartCoroutine(objHold.ThrowObj.GetComponent<ToCrashWithBlock>().SetCrashableAndDisappear());
        objHold.ThrowObj.transform.SetParent(null);
        objHold.ThrowObjRb.AddForce(((catapultTr.forward + Vector3.up).normalized) * shotPower, ForceMode.VelocityChange);
        objHold.IsReady = false;
    }

    private void PlaySound(ButtonType type)
    {
        if (type == ButtonType.Shot && !SoundManager.Instance.shotRock.loadInBackground)
        {
            SoundManager.Instance.AudioSet(catapultTr.gameObject, SoundManager.Instance.shotRock, false);
        }
        //else if (type != ButtonType.Shot && !SoundManager.Instance.rotateCatapult.loadInBackground)
        //{
        //    SoundManager.Instance.AudioSet(catapultTr.gameObject, SoundManager.Instance.rotateCatapult, false);
        //}
    }
}
