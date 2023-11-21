using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class PointCloudController : MonoBehaviour
{
    public TextAsset jsonFile;                  // JSON 파일을 할당하기 위한 변수

    public Material pointCloudMaterial;         // 포인트 클라우드를 렌더링할 Material
    public Shader pointCloudShader;             // 포인트 클라우드를 렌더링할 Shader

    public GameObject RCCar;         // 포인트 클라우드를 생성할 GameObject

    public float distanceRatio = 0.01f;         // 포인트 클라우드의 거리 비율
    public float depthScale = 0.01f;            // 포인트의 깊이에 대한 스케일 조정

    public int size = 10;                       // 원활한 출력을 위한 포인트 클라우드를 나눌 개수

    private float distance = 50f;                     // 포인트 클라우드의 거리

    void Start()
    {
        createPoint();
    }

    // 분할된 포인트 클라우드를 사이즈에 맞게 생성하는 함수
    private void createPoint(){
        for(int i = 0; i < size; i++)
        {
            GeneratePointCloud(i);
        }
    }

    // json 데이터를 전달받아 포인트 클라우드를 생성하는 함수
    private void GeneratePointCloud(int now)
    {
        // JSON 파일 파싱
        PointDataTest jsonData = JsonConvert.DeserializeObject<PointDataTest>(jsonFile.ToString());

        int width = jsonData.depth_data[0].Length;
        int height = jsonData.depth_data.Length;

        // RCCar의 위치, 방향, 회전 얻기
        Vector3 carPosition = RCCar.transform.position;
        Vector3 carForward = RCCar.transform.forward;
        Quaternion carRotation = RCCar.transform.rotation;

        // 포인트 클라우드 생성
        GameObject pointCloudObject = new GameObject("PointCloud");
        // pointCloudObject.transform.position = new Vector3(-1 * (width/2), -1 * (height/2), 750); // 카메라에서 의 거리에 배치
        pointCloudObject.transform.position = carPosition + carForward * distance; // RCCar 바라보는 방향에서 distance만큼 떨어진 위치에 배치
        pointCloudObject.transform.rotation = carRotation; // RCCar의 회전 정보 적용

        Mesh pointCloudMesh = new Mesh();                   // 포인트 클라우드를 생성할 Mesh
        int arraySize = width * height; 

        Vector3[] vertices = new Vector3[arraySize];        // 포인트 클라우드의 위치를 설정할 Vector3 배열
        Color[] colors = new Color[arraySize];              // 포인트 클라우드의 색상을 설정할 Color 배열
        int vertexIndex = 0;

        int hSize = height / size; 

        for (int i =(hSize * now); i < (hSize * (now+1)); i++)
        {
            for (int j = 0; j < width; j++)
            {
                float depth = jsonData.depth_data[i][j] * 0.1f;

                float x = j;
                
                float y = (height - i);

                float z = depth;

                // depth가 0이면 포인트 클라우드에 추가하지 않음
                // if(z == 0) continue;

                if (z == 0)
                {
                    colors[vertexIndex] = Color.blue;
                }
                else
                {
                    // depth에 따라 색상 설정 (여기서는 빨간색을 강조)
                    float normalizedDepth = Mathf.Clamp01(z / (307200f*0.1f)); // 정규화된 깊이 값 (0~1)
                    colors[vertexIndex] = new Color(normalizedDepth, 1 - normalizedDepth, 0); // 깊이에 따라 색상 설정
                }

                vertices[vertexIndex] = carPosition + carForward * distance + new Vector3(x, y, z); // RCCar 바라보는 방향에서 distance만큼 떨어진 위치에 포인트 클라우드의 위치를 설정

                vertexIndex++;
            }
        }
        pointCloudMesh.vertices = vertices;
        pointCloudMesh.colors = colors;      
        pointCloudMesh.SetIndices(Enumerable.Range(0, arraySize).ToArray(), MeshTopology.Points, 0);

        // MeshFilter 및 MeshRenderer 추가
        MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

        pointCloudMaterial.shader = pointCloudShader; // 포인트 클라우드의 Shader를 설정
        pointCloudMaterial.SetColorArray("_Colors", colors); // 포인트 클라우드의 색상을 설정

        // Mesh 및 Material 할당
        meshFilter.mesh = pointCloudMesh;
        meshRenderer.material = pointCloudMaterial ;
        
    }
    
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