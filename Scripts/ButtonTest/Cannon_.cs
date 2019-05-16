using UnityEngine;
using System;

public class Cannon_ : Controllable
{
    public Transform LR_tr;
    public Transform UD_tr;
    public float LR_rotAngle;    //Left  1초당 회전각도
    public float UD_rotAngle;    //Up    1초당 회전각도
    public CannonReload_Fire CannonFire;
    public float fireRate = 3.0f;
    
    private float nextFire = 0;

    public override void OnCtrl(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Left:
                if (LR_tr.localRotation.y <= -0.3826834) return;
                LR_tr.Rotate(Vector3.up * (-LR_rotAngle) * Time.deltaTime, Space.Self);
                break;
            case ButtonType.Right:
                if(LR_tr.localRotation.y >= 0.3826834) return;
                LR_tr.Rotate(Vector3.up * (LR_rotAngle) * Time.deltaTime);
                break;
            case ButtonType.Up:
                if (UD_tr.localRotation.x <= -0.1736f) return;
                UD_tr.Rotate(Vector3.right * (-UD_rotAngle) * Time.deltaTime);
                break;
            case ButtonType.Down:
                if (UD_tr.localRotation.x >= 0.0f) return;
                UD_tr.Rotate(Vector3.right * (UD_rotAngle) * Time.deltaTime);
                break;
            case ButtonType.Shot:
                Debug.Log("캐논 발사 버튼을 눌렀습니다."); 
                if(Time.time >= nextFire)
                {
                    StartCoroutine(CannonFire.Fire());
                    nextFire = Time.time + fireRate;
                }
                
                break;

        }
    }
}
