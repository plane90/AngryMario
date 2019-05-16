using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot2 : MonoBehaviour
{
    private Rigidbody rb;                   // 투사체의 리지드바디
    private float rotSpeed = 0.0f;          // 투사체의 회전속도

    private int rotVectorNum = 0;           // 투사체의 회전중심을 결정하기 위한 인티저값
    private Vector3 rotVector;              // 투사체의 회전중심축

    private bool isFire;                    // 투사체가 발사 되었는가?

    /// <summary>
    /// 파워 값들은 현재 랜덤으로 값을 정하여 투사체를 날리기 위해 사용되었으나,
    /// 최종적으로 다른 요소(손잡이 감은 정도 등)에 의해 파워를 결정하도록 변경해야함.
    /// </summary>
    public float shotPowerMin = 10.0f;      // 최소 발사 파워
    public float shotPowerMax = 50.0f;      // 최대 발사 파워

    public float rotSpeedMin = 90.0f;       // 최소 회전 속도
    public float rotSpeedMax = 360.0f;      // 최대 회전 속도

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // 현재는 생성되면 바로 날아가게 세팅 되어있어서 이렇게 만듬.
        // 최종적으론 진짜 발사가 될때에만 변경되어야 할 것.
        isFire = true;
    }

    private void OnEnable()
    {
        // 투사체의 앞 방향으로 shotPowerMin, Max 두 수치 사이의 랜덤한 값으로 발사                                    /// <summary>
        rb.AddForce(transform.forward * Random.Range(shotPowerMin, shotPowerMax), ForceMode.VelocityChange);       /// ForceMode 는 Impulse와 VelocityChange가 아니면 제대로 발사가 되지 않았음.
                                                                                                                   /// 관련해서 좀 더 알아볼 필요가 있음.
        // 회전축을 랜덤하게 결정                                                                                    /// </summary>                    
        rotVectorNum = Random.Range((int)0, (int)6);

        // 위에서 랜덤하게 받은 rotVectorNum을 이용하여 회전축을 설정.
        switch (rotVectorNum)
        {
            case 0:
                rotVector = Vector3.forward;
                break;
            case 1:
                rotVector = Vector3.back;
                break;
            case 2:
                rotVector = Vector3.right;
                break;
            case 3:
                rotVector = Vector3.left;
                break;
            case 4:
                rotVector = Vector3.up;
                break;
            case 5:
                rotVector = Vector3.down;
                break;
        }
        // 회전 속도 또한 랜덤.
        rotSpeed = Random.Range(rotSpeedMin, rotSpeedMax);

        // 100초 후 파괴.
        Destroy(this.gameObject, 100.0f);
    }


    // 투사체의 컬라이더가 충돌한것을 감지하면 isFire 를 false 로 변경.
    private void OnCollisionEnter(Collision coll)
    {
        isFire = false;
    }

    private void Update()
    {
        // isFire 가 true 일 경우에 회전시킨다.
        if (isFire)
        {
            transform.Rotate(rotVector * 180.0f * Time.deltaTime);
        }
    }
}