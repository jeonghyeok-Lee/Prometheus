using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointCloudController
{
    private Material pointCloudMaterial;         // 포인트 클라우드를 렌더링할 Material
    private float limitDepth = 0f;             // 포인트 클라우드의 깊이 제한
    private float distanceFromCar = 0f;          // 포인트 클라우드와 RCCar 사이의 거리
    private int size = 0;                       // 원활한 출력을 위한 포인트 클라우드를 나눌 개수
    private PointCloudGenerator generator;       // 포인트 클라우드를 생성하는 클래스
    private int pointCloudObjectCount = 0;       // 생성된 포인트 클라우드 오브젝트의 개수

	private PointState pointState;      // 포인트 클라우드의 상태정보를 가지고 있는 클래스
    	public PointState PointState
	{
		get { return pointState; }
		set { 
			pointState = value; 
		}
	}

    public PointCloudController(Material pointCloudMaterial)
    {
        // 포인트 클라우드를 생성하는 클래스 생성
        generator = new PointCloudGenerator();
        
        pointState = new PointState(32000f, 0f, 10, 0.05f);      // 포인트 클라우드 상태 정보 클래스 생성
        generator.PointState = pointState;                      // 포인트 클라우드 생성 클래스에 상태 정보 클래스 설정
        
        // generator.LimitDepth = limitDepth;
        // generator.DistanceFromCar = distanceFromCar;
        // generator.Size = size;

        this.pointCloudMaterial = pointCloudMaterial;
    }

    // 분할된 포인트 클라우드를 사이즈에 맞게 생성하는 함수
    public void CreatePointCloud(DataController dataController, CarController car)
    {
        GameObject pointCloudObject = new GameObject("PointCloud" + pointCloudObjectCount++);
        pointCloudObject.transform.position = Vector3.zero;

        Debug.Log(1);
        // Size에 따라 포인트 클라우드 생성
        for (int i = 0; i < pointState.Size; i++)
        {
            generator.GeneratePointCloud(i, dataController, car, pointCloudMaterial);
            
            // 생성된 포인트 클라우드를 pointCloudObject의 자식으로 설정
            generator.PointCloudObject.transform.parent = pointCloudObject.transform;
        }
    }

}