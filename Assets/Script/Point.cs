using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int pointNum;            // 포인트 번호
    public float x;                 // 포인트의 x좌표
    public float y;                 // 포인트의 y좌표
    public float z;                 // 포인트의 z좌표
    public bool isShow;             // 포인트가 활성화 되었는지
    public Transform pointTrans;    // 포인트의 Transform

    /// <summary>
    /// @todo : 생성된 포인트 위치에 다른 포인트가 있으면 생성하지 않도록 수정
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

    }
}