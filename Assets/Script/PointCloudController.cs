using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class PointCloudController : MonoBehaviour
{
    public TextAsset jsonFile; // JSON 파일을 할당하기 위한 변수

    public Material pointCloudMaterial; // 포인트 클라우드를 렌더링할 Material

    public float distanceRatio = 0.01f; // 포인트 클라우드의 거리 비율
    public float depthScale = 0.01f; // 포인트의 깊이에 대한 스케일 조정

    void Start()
    {
        GeneratePointCloud();
    }

    private void GeneratePointCloud()
    {
        // JSON 파일 파싱
        PointDataTest jsonData = JsonConvert.DeserializeObject<PointDataTest>(jsonFile.ToString());

        int width = jsonData.depth_data[0].Length;
        int height = jsonData.depth_data.Length;

        // 포인트 클라우드 생성
        GameObject pointCloudObject = new GameObject("PointCloud");
        pointCloudObject.transform.position = new Vector3(0, 0, 10); // 카메라에서 10의 거리에 배치

        Mesh pointCloudMesh = new Mesh();
        int arraySize = width * height;
        Vector3[] vertices = new Vector3[arraySize];
        int vertexIndex = 0;

        // Unity의 월드 좌표에서 이미지 중앙으로 이동하는 벡터
        Vector3 offset = new Vector3(-width / 2 * distanceRatio, -height / 2 * distanceRatio, 0);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float depth = jsonData.depth_data[i][j] * distanceRatio;

                float x = j * distanceRatio;
                float y = (height - i) * distanceRatio;
                float z = depth;

                // Unity의 월드 좌표로 변환 후 offset 적용
                Vector3 worldCoordinate = new Vector3(x, y, z) + offset;

                vertices[vertexIndex] = worldCoordinate;
                vertexIndex++;
            }
        }

        pointCloudMesh.vertices = vertices;
        pointCloudMesh.SetIndices(Enumerable.Range(0, arraySize).ToArray(), MeshTopology.Points, 0);

        // MeshFilter 및 MeshRenderer 추가
        MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = pointCloudMesh;
        meshRenderer.material = pointCloudMaterial;
    }

    // 테스트용
    private void Test()
    {
        // 테스트용
        // PointDataTest json = JsonUtility.FromJson<PointDataTest>(jsonFile2.text);
        PointDataTest jsonTest = JsonConvert.DeserializeObject<PointDataTest>(jsonFile.ToString());             // 스캔한 데이터

        int width = jsonTest.depth_data[0].Length;
        int height = jsonTest.depth_data.Length;

        int number = 0;

        // 좌표로 변환
        List<Vector3> unityCoordinates = new List<Vector3>();

        // 포인트 클라우드를 생성하고 위치 정보를 설정
        GameObject pointCloudObject = new GameObject("PointCloud");
        pointCloudObject.transform.position = Vector3.zero;

        // 각 포인트에 대해 메시에 추가
        Mesh pointCloudMesh = new Mesh();
        int arraySize = width * height;
        Vector3[] vertices = new Vector3[arraySize];
        int vertexIndex = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float depth = jsonTest.depth_data[i][j] * distanceRatio;

                // 이미지의 중앙을 (0, 0, 0)으로 간주하고, 각 픽셀의 좌표를 계산
                float x = (j - width / 2) * distanceRatio;
                float y = (i - height / 2) * distanceRatio;

                // depth_data의 값이 이미지에서 해당 픽셀까지의 깊이를 나타내므로, z 좌표로 사용
                float z = depth;

                if (x != 0.0f || y != 0.0f || z != 0.0f)
                {
                    vertices[vertexIndex] = new Vector3(x, y, z);
                    vertexIndex++;
                }
            }
        }

        pointCloudMesh.vertices = vertices;
        pointCloudMesh.SetIndices(Enumerable.Range(0, arraySize).ToArray(), MeshTopology.Points, 0);

        // 메시 필터 및 렌더러 추가
        MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = pointCloudMesh;
        meshRenderer.material = pointCloudMaterial;
    }

    /// <summary>
    /// //////////////////////////////// 테스트용
    /// </summary>
    [System.Serializable]
    public class PointDataTest
    {
        public Location location;
        public int[][] depth_data;
    }

    [System.Serializable]
    public class Location
    {
        public float x;
        public float y;
        public float z;
    }

}