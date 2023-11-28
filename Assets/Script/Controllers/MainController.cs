using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class MainController : MonoBehaviour
{
    private PointCloudController pointCloudController;  //  포인트 클라우드를 생성하는 클래스
    private CarController carController;                //  RCCar의 위치, 방향, 회전 정보를 가져오기 위한 Car 클래스
    private DataController dataController;              //  JSON 파일을 파싱하기 위한 DataController 클래스

    /// <summary>
    /// pointCloud 관련 변수
    /// </summary>
    public Material pointCloudMaterial;                 //  포인트 클라우드를 렌더링할 Material
    private PointData jsonData;

    /// <summary>
    /// RC카 관련 변수
    /// </summary>
    public Transform car;                              //  RCCar 오브젝트

    /// <summary>
    /// 테스트용 변수
    /// </summary>
    int i = 1;
    private int count;  // json 파일의 개수
    private bool isRunning = false; // 포인트 클라우드 생성 중인지 확인하는 변수

    // Start is called before the first frame update
    void Start()
    {
        pointCloudController = new PointCloudController(pointCloudMaterial);
        carController = new CarController(car);
        dataController = new DataController();

        // pointCloudController.CreatePointCloud(dataController, carController);
        count = dataController.GetFileCount();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // isRunning = !isRunning; // 시작/정지
            
            // if(isRunning){
            //     StartCoroutine(CreatePointsCoroutine());
            // }
                StartCoroutine(CreatePointsCoroutine());
        }
    }
    
    /// <summary>
    /// 포인트 클라우드 생성 코루틴
    /// </summary>
    IEnumerator CreatePointsCoroutine(){

        // while (i < count && isRunning)
        // {
        //     // 파일 이름 설정 및 JSON 데이터 가져오기
        //     dataController.SetJsonData("depth_data_" + i);
        //     jsonData = dataController.GetJsonData();
        //     Location location = jsonData.location;

        //     // RCCar 위치 및 회전 업데이트
        //     carController.CarPosition = new Vector3(location.y, 0.15f, location.x * -1f);
        //     carController.CarRotation = Quaternion.Euler(0, 360 - location.yaw , 0);
        //     carController.CarForward = car.forward;

        //     // 포인트 클라우드 생성
        //     pointCloudController.CreatePointCloud(dataController, carController);

        //     // 0.5 초 기다림
        //     yield return new WaitForSeconds(0.1f);

        //     // i 증가 및 wrap-around
        //     i = (i % count) + 1;
        // }
        if(i < count){
            // 파일 이름 설정 및 JSON 데이터 가져오기
            dataController.SetJsonData("depth_data_" + i);
            jsonData = dataController.GetJsonData();
            Location location = jsonData.location;

            // RCCar 위치 및 회전 업데이트
            carController.CarPosition = new Vector3(location.y*0.01f, 0.15f, location.x * -0.01f);
            // carController.CarRotation = Quaternion.Euler(0, 360 - location.yaw , 0);
            Quaternion  q = new Quaternion(location.yaw[0], -location.yaw[2], location.yaw[1], location.yaw[3]);
            carController.CarRotation = q;
            carController.CarForward = car.forward;

            // 포인트 클라우드 생성
            pointCloudController.CreatePointCloud(dataController, carController);

            // 0.5 초 기다림
            yield return new WaitForSeconds(0.1f);

            // i 증가 및 wrap-around
            i = (i % count) + 1;
        }
    }

}
