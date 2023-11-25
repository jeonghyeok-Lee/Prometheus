using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointCloudController : MonoBehaviour
{
    public Material pointCloudMaterial;         // 포인트 클라우드를 렌더링할 Material
    public CarController car;                   // RCCar의 위치, 방향, 회전 정보를 가져오기 위한 Car 클래스
    public DataController dataController;       // JSON 파일을 파싱하기 위한 DataController 클래스
    public float limitDepth = 500f;             // 포인트 클라우드의 깊이 제한
    
    // RCCar와 이미지 사이의 거리
    public float distanceFromCar = 0f;          // 포인트 클라우드의 거리

    public int size = 10;                       // 원활한 출력을 위한 포인트 클라우드를 나눌 개수


    void Start()
    {
        CreatePointCloud();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreatePointCloud();
        }
    }
    // 분할된 포인트 클라우드를 사이즈에 맞게 생성하는 함수
    private void CreatePointCloud()
    {
        // PointCloudGenerator 인스턴스 생성
        PointCloudGenerator generator = new PointCloudGenerator();

        // Size에 따라 포인트 클라우드 생성
        for (int i = 0; i < size; i++)
        {
            generator.GeneratePointCloud(i, dataController, car, pointCloudMaterial, limitDepth, distanceFromCar, size);
        }
    }
}