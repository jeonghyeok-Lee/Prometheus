using System.Collections.Generic;
using UnityEngine;
using System;
using DataSetting;


public class PointMgr : MonoBehaviour
{
    public GameObject point;                                // 하나의 점 역할을 수행할 객체
    public GameObject middle;                               // 중심을 나타내는 중심점
    private LineRenderer lineRenderer;                      // 생성되는 포인트와 중심점을 연결시킬 선

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

    public float mdAngle = 0;                               // 현재 각도

    private int showNum = 0;                                // 현재 활성화된 포인터 개수

    private int lineCount = 1;

    private GameObject instance;                            // 포인트객체

    private PointGroup groupMgr;                              // 그룹핑 객체 생성
    public GameObject pointGroup;                          // 한 라인의 포인트들을 가지고 있을 pointGroup
    public GameObject lineGroup;                           // 라인 오브젝트를 그룹핑하기 위한 오브젝트    [관리를위함]
    public List<GameObject> instanceList;                  // 생성한 포인트들을 리스트형식

    public bool enablePoint;                                // 생성된 포인트를 활성화할지를 확인하는 변수

    private void Awake()
    {

        pointInit();
        enablePoint = true;                                     // 기본값으로는 true로 초기화

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

        if (enablePoint)
        {
            OnEnablePoint(5);
        }
        

    }

    public void pointInit()
    {
        // DataJson ldJson = new DataJson("json/data00001");        // json 데이터를 로드
        DataController dataController = new DataController("json", "json/data00001");

        /*        ldJson.setPath("json/data");*/
        // myData = ldJson.ScanData;                               // json 데이터 가져오기
        myData = dataController.LoadData();

        mgrPosition = new Vector3();
        instanceList = new List<GameObject>();                  // 시작시 리스트 생성

        groupMgr = new PointGroup(null);                          // 객체 초기화


        // LineRenderer 컴포넌트 생성
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }


    // 시간을 할당 받아 해당 시간을 넘어가면 활성화를 진행
    private void OnEnablePoint(int timer) 
    {
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
        pointGroup = groupMgr.createGroup("line" + lineCount);
        groupMgr.setGroupPosition(pointGroup, new Vector3(0, y, 0));                                                                 // 라인의 위치를 변경           

        if ((lineCount - 1) % 100 == 0)                                                                                              // 라인오브젝트가 100개마다 새로운 라인그룹을 생성                                               
        {
            lineGroup = groupMgr.createGroup("lineGroup" + lineCount / 100);
        }
        groupMgr.setGroupPosition(lineGroup, new Vector3(0, 0, 0));
        groupMgr.addObjectToGroup(lineGroup, pointGroup);

        for (int i = 0; i < myData.data.Length; i++)
        {
            for(int j = 0; j < myData.data[i].Length; j++)
            {
                this.transform.rotation = Quaternion.Euler(0, float.Parse(myData.data[i][j].angle), 0);                                  // 중심 위치 객체의 rotaion값 변경

                mgrPosition.x = (float)this.transform.position.x;
                mgrPosition.y = y;
                mgrPosition.z = (float)this.transform.position.z;
                this.transform.position = mgrPosition;

                playerDir = this.transform.forward;                                                                                   // 현재 플레이어가 바라보는 방향
                playerDir *= float.Parse(myData.data[i][j].distance) * 0.01f;                                                         
                spawnPos = this.transform.position + playerDir;

                instance = Instantiate(point);                                                                                        // 포인트 생성
                instance.SetActive(false);                                                                                            //  생성 시에는 비활성화
                instance.transform.position = spawnPos;                                                                               // 위치 설정
                groupMgr.addObjectToGroup(pointGroup, instance);                                                                      // pointGroup의 자식으로 생성
                instanceList.Add(instance);                                                                                           // 리스트에 추가
            }

        }
        lineCount++;
/*        y = (float)this.transform.position.y + (float)instance.transform.localScale.y;*/
        y = (float)this.transform.position.y;

        pointGroup.transform.rotation = Quaternion.Euler(mdAngle, 0, 0);                                                          // 라인의 각도를 수정
    }

}
