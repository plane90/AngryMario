using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    public Transform throwItems;                // 던질 아이템들을 모아둔 게임오브젝트의 트랜스폼

    private Transform holder;                   // 아이템이 고정되는 위치 
    private Transform itemTr;                   // 아이템의 위치
    private Rigidbody itemRb;                   // 아이템의 리지드 바디

    private void Start()
    {
        holder = GameObject.Find("HoldObjectPos").GetComponent<Transform>();
    }

    // 투석기 발사 애니메이션(SpoonUp)이 발동했을때 이벤트로 불려지는 코루틴 함수. StartCouroutine() 함수를 통하지 않고도 작동 되는데 왜그런지는 모르겠다.
    private IEnumerator ThrowObject()
    {
        itemTr = holder.GetComponentsInChildren<Transform>()[1];        // 겟컴포넌츠인칠드런은 자기 자신의 트랜스폼도 반환하므로 [1]번 인덱스로 받아야함.
        itemRb = holder.GetComponentInChildren<Rigidbody>();            // 아이템의 리지드 바디 할당

        itemTr.transform.SetParent(throwItems.transform);               // 아이템 하이어라키가 HoldObject에서 스푼에 붙기 때문에 발사시엔 원래 자리로 바꿔준다.

        // 아이템에 힘을 가해서 날아가게 만들어준다. ForceMode.Impulse는 물체의 무게에 영향을 받고, VelocityChange는 영향을 받지 않는 것을 확인하였음.
        itemRb.AddForce(new Vector3(1, 1, 0).normalized * 25.0f, ForceMode.VelocityChange);

        // 0.01초 기다렸다가 itemTr과 itemRb 바디를 초기화 해준다.
        yield return new WaitForSeconds(0.01f);
        itemTr = null;
        itemRb = null;
    }
}
