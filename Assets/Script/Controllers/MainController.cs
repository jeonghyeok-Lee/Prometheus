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

    /// <summary>
    /// RC카 관련 변수
    /// </summary>
    public Transform car;                              //  RCCar 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        pointCloudController = new PointCloudController(pointCloudMaterial);
        carController = new CarController(car);
        dataController = new DataController();

        pointCloudController.CreatePointCloud(dataController, carController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
