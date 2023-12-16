using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEvent : MonoBehaviour
{
    private bool isEnable;                          // 현재 포인트들이 활성화 되고있는지 여부 확인
    private GameObject objMgr;

    // Start is called before the first frame update
    void Start()
    {
        isEnable = false;                                           // 초기 로드시 비활성화
        objMgr = GameObject.Find("objManager");
        // pointMgr = objMgr.GetComponent<PointMgr>();
    }

    // 메뉴바를 활성화 비활성화를 진행할 함수
    public void OnMenuList()
    {
        // 활성화 여부에 따라서 자식 버튼의 활성화 비활성화 진행
        if (isEnable)
        {
            isEnable = false;
            for(int i = 1; i< this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(isEnable);
            }
        }
        else
        {
            isEnable = true;
            for (int i = 1; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(isEnable);
            }

        }
    }


    // 게임 일시정지/시작
    public void OnEnablePoint()
    {
        
    }

    public void OnPointReset()
    {

    }
}
