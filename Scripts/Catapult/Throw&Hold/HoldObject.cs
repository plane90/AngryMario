using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    private Transform tr;                                                   // 아이템이 붙을 게임오브젝트의 트랜스폼

    private Animator anim;                                                  // 애니메이터
    private readonly int hashIsFire = Animator.StringToHash("IsFire");      // 애니메이터의 파라메터 값을 받아올 해쉬값.
    private bool isFire = false;                                            // 스크립트 내에서 동작 제어에 사용할 boolean.

    private void Awake()
    {
        tr = this.transform;
        anim = GameObject.Find("Catapult").GetComponent<Animator>();        // 스크립트의 위치에 애니메이터가 없고, 상위 하이어라키에 존재하기에 찾아서 붙여줌.
    }

    private void Update()
    {
        StartCoroutine(ControlCatapult());
    }

    // 투석기의 애니메이션을 컨트롤할 코루틴 함수.
    private IEnumerator ControlCatapult()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFire = true;
            anim.SetBool(hashIsFire, isFire);

            // ObjectThrow 에서 아이템의 하이어라키를 본래 위치로 전환하는데 isFire가 계속 트루이면 자꾸 이곳에 붙게 되므로 0.01초 후에 false로 바꿔준다.
            yield return new WaitForSeconds(0.01f);
            isFire = false;
        }
        // 원상복귀 (디버그용)
        else if (Input.GetKeyDown(KeyCode.Slash))
        {
            anim.SetBool(hashIsFire, isFire);
        }
    }

    // 스푼에 던질 오브젝트가 있을 때
    private void OnTriggerStay(Collider other)
    {
        // 태그 비교 후
        if (other.transform.CompareTag("Rock"))
        {
            // 스페이스바를 누르면 isFire가 true가 되어 오브젝트를 SetParent 해줌.
            if (isFire)
            {
                other.transform.SetParent(tr);
            }
        }
    }
}

