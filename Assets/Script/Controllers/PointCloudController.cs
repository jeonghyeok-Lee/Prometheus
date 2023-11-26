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

    /// <summary>
    /// 포인트 클라우드의 깊이 제한
    /// </summary>
    public float LimitDepth
    {
        get { return limitDepth; }
        set { limitDepth = value; }
    }

    public float DistanceFromCar
    {
        get { return distanceFromCar; }
        set { distanceFromCar = value; }
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public PointCloudController(Material pointCloudMaterial)
    {
        // 포인트 클라우드를 생성하는 클래스 생성
        generator = new PointCloudGenerator();

        limitDepth = 500f;
        distanceFromCar = 300f;
        size = 10;

        this.pointCloudMaterial = pointCloudMaterial;
    }

    // 분할된 포인트 클라우드를 사이즈에 맞게 생성하는 함수
    public void CreatePointCloud(DataController dataController, CarController car)
    {
        // Size에 따라 포인트 클라우드 생성
        for (int i = 0; i < size; i++)
        {
            generator.GeneratePointCloud(i, dataController, car, pointCloudMaterial, limitDepth, distanceFromCar, size);
        }
    }
}