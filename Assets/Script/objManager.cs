using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using JsonSetting;


public class objManager : MonoBehaviour
{
    public GameObject point;    // 하나의 점 역할을 수행할 객체
    public GameObject middle;    // 

    private GameObject instance; // 생성한 포인트들을 가지고 있을 객체

    private Vector3 playerPos;
    private Vector3 playerDir;
    private Vector3 spawnPos;

    private ScanDataArray myData;

    public float limitTime = 60;
    private float currTime;

    float y;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson ldJson = new LoadJson("json/data");        // json 데이터를 로드
        myData = ldJson.Data;                               // json 데이터 가져오기

    }

    // Update is called once per frame
    void Update()
    {

        if (currTime > limitTime)        // 현재 시간이 제한 시간보다 큰 경우
        {
            currTime = 0;

        }
        else
        {
            currTime += Time.deltaTime;

            y = (float)middle.transform.position.y + .1f;
            for (int i = 0; i < myData.data.Length; i += 2)
            {
                middle.transform.rotation = Quaternion.Euler(0, float.Parse(myData.data[i].angle), 0);                                  // 중심 위치 객체의 rotaion값 변경
                middle.transform.position = new Vector3((float)middle.transform.position.x, y, (float)middle.transform.position.z);     // y축 값 추가를 위해 객체의 position값 변경
                playerPos = middle.transform.position;                                                                                  // 현재 플레이어 위치
                playerDir = middle.transform.forward;                                                                                   // 현재 플레이어가 바라보는 방향
                playerDir *= float.Parse(myData.data[i].distance) * 0.01f;
                spawnPos = playerPos + playerDir;
                instance = Instantiate(point);
                instance.transform.position = spawnPos;
            }
            Debug.Log($"{Mathf.Round(currTime)} {currTime}");
        }

    }
}
