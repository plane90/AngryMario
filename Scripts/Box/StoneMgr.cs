 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//버튼 누르면 스톤(투석체) 생성
public class StoneMgr : MonoBehaviour
{
    
    public GameObject[] stone;                      //오브젝트가 들어갈 배열 스톤
    static public int stoneCurrentCount = 1;        //생성된 오브젝트 갯수
    private int basicCount = 1;

    private Transform tr;                           //오브젝트 생성 위치

    
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    public void OnCreate()                                                          //버튼 누르면 실행될 함수
    {

        //최대 스톤(투석체) 갯수 3개 이하까지만 생성
        if (stoneCurrentCount <= 3)                   
        {
            OnCreateChr();
        }
        else return;
    }

    public void OnCreateChr()                                                        //생성및 스톤(투석체)
    {
        //배열에 들어간 스톤(투석체)오브젝트 종류 중 랜덤으로 고른다.
        int idx = Random.Range(0, stone.Length);

        //배열에 들어간 스톤(투석체)을 생성
        GameObject chr = Instantiate(stone[idx], tr.position, Quaternion.identity);

        //현재 오브젝트 갯수
        stoneCurrentCount += basicCount;
        //Debug.Log(stoneCurrentCount);

    }

   
}
