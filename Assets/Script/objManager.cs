using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using JsonSetting;


public class objManager : MonoBehaviour
{
    public GameObject point;                                // 하나의 점 역할을 수행할 객체
/*    public GameObject middle;    // */

    private GameObject instance;                            // 생성한 포인트들을 가지고 있을 객체

    private Vector3 playerPos;
    private Vector3 playerDir;
    private Vector3 spawnPos;

    private ScanDataArray myData;

    private float deltaTime = 0.0f;
    private float msec;
    private float fps;

    public float limitTime = 60;
    private float currTime;
    private float nowTime = 0;

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
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        msec = deltaTime * 1000.0f;

        if (currTime > limitTime)        // 현재 시간이 제한 시간보다 큰 경우
        {
            /*currTime = 0;*/
        }
        else
        {
            currTime += Time.deltaTime;

            if (fps < 15)                                                                   // 프레임이 15이하로 떨어질 경우
            {           
                if (Mathf.Round(currTime) > nowTime)                                        // 1초마다 생성                    
                {
                    nowTime = Mathf.Round(currTime);
                    createPoint();
                }

/*            Debug.Log($"{Mathf.Round(currTime)} {currTime}");*/
            }
            else
            {
                createPoint();
            }

        }

    }


    /// <summary>
    /// 포인트 오브젝트를 생성하는 과정을 함수로 묶어서 사용
    /// </summary>
    private void createPoint()
    {
        for (int i = 0; i < myData.data.Length; i += 2)
        {
            this.transform.rotation = Quaternion.Euler(0, float.Parse(myData.data[i].angle), 0);                                  // 중심 위치 객체의 rotaion값 변경
            this.transform.position = new Vector3((float)this.transform.position.x, y, (float)this.transform.position.z);     // y축 값 추가를 위해 객체의 position값 변경
            playerPos = this.transform.position;                                                                                  // 현재 플레이어 위치
            playerDir = this.transform.forward;                                                                                   // 현재 플레이어가 바라보는 방향
            playerDir *= float.Parse(myData.data[i].distance) * 0.01f;
            spawnPos = playerPos + playerDir;
            instance = Instantiate(point);
            instance.transform.position = spawnPos;
        }
        y = (float)this.transform.position.y + .1f;
    }
}
