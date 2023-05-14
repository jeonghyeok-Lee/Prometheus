using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using JsonSetting;


public class objManager : MonoBehaviour
{
    public GameObject point;                                // 하나의 점 역할을 수행할 객체
    public GameObject middle;                               // 중심을 나타내는 중심점

    private LineRenderer lineRenderer;                      // 생성되는 포인트와 중심점을 연결시킬 선

    private GameObject instance;                            // 포인트객체
    private GameObject lineObject;                          // 한 라인의 포인트들을 가지고 있을 lineObject
    private List<GameObject> instanceList;                  // 생성한 포인트들을 리스트형식

    private Vector3 playerPos;
    private Vector3 playerDir;
    private Vector3 spawnPos;

    private Vector3 mgrPosition;                            // 자신의 포지션을 저장할 Vector3변수
    private float x = 0.0f, y = 0.0f, z = 0.0f;             // x,y,z값

    private ScanDataArray myData;                           // JsonData를 가진 배열변수

    private float deltaTime = 0.0f;
    private float msec;
    private float fps;

    public float limitTime = 60;
    private float currTime = 0.0f;
    private int nowTime = 0;

    public float mdAngle = 0;                               

    private int showNum = 0;                                // 현재 활성화된 포인터 개수

    private int lineCount = 1;

    private void Awake()
    {
        LoadJson ldJson = new LoadJson("json/data");        // json 데이터를 로드
        myData = ldJson.Data;                               // json 데이터 가져오기
        mgrPosition = new Vector3();
        instanceList = new List<GameObject>();              // 시작시 리스트 생성

        // LineRenderer 컴포넌트 생성
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

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
            nowTime = (int)Math.Round(currTime);

            if (fps < 25)                                                                   // 프레임이 25이하(자연스러운 움직임을 감지)로 떨어질 경우  
            {           
                if (Math.Round(currTime) > nowTime)                                        // 1초마다 생성                    
                {
                    createPoint();
                }

            }
            else
            {
                createPoint();
            }

        }
       
        if (nowTime > 5)                                                                    // 5초가 지난 이후 현재 오브젝트를 순서대로 활성화
        {
            if (instanceList.Count > showNum)
            {
                instanceList[showNum].SetActive(true);
                lineRenderer.positionCount = 2;                                             // 활성화 할때마다 포지션포인트 2개를 생성 하고 세팅
                lineRenderer.SetPosition(0, middle.transform.position);
                lineRenderer.SetPosition(1, instanceList[showNum].transform.position);
                showNum++;
            }
        }

    }


    /// <summary>
    /// 포인트 오브젝트를 생성하는 과정을 함수로 묶어서 사용
    /// </summary>
    private void createPoint()
    {
        GameObject lineObject = new GameObject("line" + lineCount);                                                               // 한 라인을 담아둘 오브젝트 생성
        lineObject.transform.position = new Vector3(0, y, 0);                                                                     // 라인의 위치를 변경           

        for (int i = 0; i < myData.data.Length; i += 2)
        {

            this.transform.rotation = Quaternion.Euler(0, float.Parse(myData.data[i].angle), 0);                                  // 중심 위치 객체의 rotaion값 변경

            mgrPosition.x = (float)this.transform.position.x;
            mgrPosition.y = y;
            mgrPosition.z = (float)this.transform.position.z;
            this.transform.position = mgrPosition;

            playerDir = this.transform.forward;                                                                                   // 현재 플레이어가 바라보는 방향
            playerDir *= float.Parse(myData.data[i].distance) * 0.01f;
            spawnPos = this.transform.position + playerDir;

            instance = Instantiate(point);                                                                                        // 포인트 생성
            instance.SetActive(false);                                                                                            //  생성 시에는 비활성화
            instance.transform.position = spawnPos;                                                                               // 위치 설정
            instance.transform.SetParent(lineObject.transform);                                                                   // lineObject의 자식으로 생성
            instanceList.Add(instance);                                                                                           // 리스트에 추가

        }
        lineCount++;
        y = (float)this.transform.position.y + (float)instance.transform.localScale.y;
        lineObject.transform.rotation = Quaternion.Euler(mdAngle, 0, 0);                                                          // 라인의 각도를 수정
    }

}
