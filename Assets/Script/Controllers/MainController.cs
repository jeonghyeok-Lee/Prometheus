using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        pointCloudController = new PointCloudController(pointCloudMaterial);
        carController = new CarController(car);
        dataController = new DataController();

        // pointCloudController.CreatePointCloud(dataController, carController);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dataController.SetJsonData("depth_data_" + i);
            if(i < 4){
                i++;
            }
            else{
                i = 1;
            }
            jsonData = dataController.GetJsonData();
            Location location = jsonData.location;

            // RCCar 데이터를 json 파일의 location 정보로 변경
            carController.CarPosition = new Vector3(location.x, location.y, location.z);
            carController.CarRotation = Quaternion.Euler(0, location.r, 0);
            carController.CarForward = car.forward;

            // 포인트 클라우드 생성
            pointCloudController.CreatePointCloud(dataController, carController);
        }
    }
}
