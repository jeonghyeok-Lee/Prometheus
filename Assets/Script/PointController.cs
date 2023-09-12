using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DataSetting;

public class PointController : MonoBehaviour
{
    [SerializeField] private GameObject point;         // 포인트 프리팹
    [SerializeField] private GameObject middle;        // 중심점
    [SerializeField] private float controllerAngle;    // 현재 각도

    private ScanDataArray pointData;                // 포인트 데이터 [위치 데이터를 가지고 있는 배열 변수]
    private PointGroup groupManager;                // 그룹 매니저
    private GameObject pointGroup;                  // 포인트 그룹
    private GameObject lineGroup;                   // 라인 그룹

    private const int LineObjectCount = 100;        // 라인 그룹당 포인트 개수
    private int lineCount = 1;                      // 라인 그룹의 개수 [ 몇 번째 라인인지 확인하기 위함 ]

    private const float DistanceRatio = 0.01f;      // 포인트 컨트롤러의 방향을 설정할 때 사용할 비율
    private Vector3 spanwPos;                       // 생성될 위치


    ///////////////////////////////////////////////// 테스트용
    // 프레임 체크를 위함
    private float deltaTime = 0.0f;
    private float msec;
    private float fps;
    private float currentTime = 0.0f;
    private float limitTime = 60.0f;
    private int nowTime = 0;


    private int testCount = 0;
    private Vector3 directionFromCenter;

    void Awake()
    {
        SettingPointData();     // 포인트 데이터 초기화

        groupManager = new PointGroup(this.transform);

    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        msec = deltaTime * 1000.0f;

        currentTime += Time.deltaTime;
        nowTime = (int)Math.Round(currentTime);

        if (fps > 50)
        {
            if (Math.Round(currentTime) > nowTime)
            {
                CreatePoint();
            }
        }
        else
        {
            CreatePoint();
        }

    }

    /// <summary>
    /// 포인트 데이터 초기화
    /// </summary>
    private void SettingPointData()
    {
        DataController dataController = new DataController("json", "json/data00001");

        pointData = dataController.LoadData();    // 데이터 로드
    }

    private void CreatePointGroups()
    {
        // 포인트 그룹 생성
        pointGroup = groupManager.createGroup("PointGroup" + lineCount);

        groupManager.setGroupPosition(pointGroup, Vector3.zero);
    }

    /// <summary>
    /// 포인트를 담을 라인 그룹을 생성하는 메서드
    /// </summary>
    private void CreateLineGroup()
    {
        // 라인 그룹 생성 [이름을 LineGroup + lineCount / LineObjectCount로 설정]
        lineGroup = groupManager.createGroup("LineGroup" + lineCount / LineObjectCount);

        // 라인 그룹의 위치를 설정
        groupManager.setGroupPosition(lineGroup, Vector3.zero);
    }

    /// <summary>
    /// 포인트의 위치 및 회전값을 계산하는 메서드
    /// </summary>
    /// <param name="angle">중심점과의 각도 포인터가 위치할 각도</param>
    /// <param name="distance">중심점과의 거리 포인터가 중심점으로부터 떨어진 거리</param>
    private void CalPointPosAndRot(float angle, float distance)
    {
        // 중심점[플레이어]의 회전값을 변경
        middle.transform.rotation = Quaternion.Euler(0, angle, 0);

        // 위치 설정 [포인터들이 생성될 때 중심점은 자리 변동이 없기 때문에 y값을 고정] [자동차 형태이기에]
        middle.transform.position = new Vector3(middle.transform.position.x, 0.0f, middle.transform.position.z);

        // 포인트컨트롤러가 바라보는 방향에서 0.01f만큼 떨어진 위치를 계산
        directionFromCenter = middle.transform.forward * distance * DistanceRatio;

        // 포인트가 생성될 위치를 계산
        spanwPos = middle.transform.position + directionFromCenter;
    }

    /// <summary>
    /// 포인트 초기화 함수
    /// </summary>
    private void InitPoint()
    {
        // 포인트 생성 및 세팅
        GameObject instance = Instantiate(point);
        instance.SetActive(true);
        instance.transform.position = spanwPos;

        // 생성된 포인트를 포인트 그룹의 자식으로 설정
        groupManager.addObjectToGroup(pointGroup, instance);
    }

    /// <summary>
    /// 포인트를 생성하는 메서드
    /// </summary>
    private void CreatePoint()
    {
        CreatePointGroups();

        if ((lineCount - 1) % LineObjectCount == 0)
        {
            CreateLineGroup();
        }

        // 포인트 그룹을 라인 그룹의 자식으로 설정
        groupManager.addObjectToGroup(lineGroup, pointGroup);
        for (int i = 0; i < pointData.data.Length; i++)
        {
            for (int j = 0; j < pointData.data[i].Length; j++)
            {
                CalPointPosAndRot(float.Parse(pointData.data[i][j].angle), float.Parse(pointData.data[i][j].distance));
                InitPoint();
            }
        }
        lineCount++;
        pointGroup.transform.rotation = Quaternion.Euler(controllerAngle, 0, 0);
    }

}
