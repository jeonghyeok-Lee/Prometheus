using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int pointNum;            // 포인트 번호
    public bool isMiddle;           // 포인트가 중심점인지
    public bool isShow;             // 포인트가 활성화 되었는지
    public Transform pointTrans;    // 포인트의 Transform

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            // Debug.Log(other.gameObject.name + "와 충돌");
            Destroy(other.gameObject);  
        }
    }
}