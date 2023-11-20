using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class PointCloudController : MonoBehaviour
{
    public TextAsset jsonFile;                  // JSON 파일을 할당하기 위한 변수

    public Material pointCloudMaterial;         // 포인트 클라우드를 렌더링할 Material

    public float distanceRatio = 0.01f;         // 포인트 클라우드의 거리 비율
    public float depthScale = 0.01f;            // 포인트의 깊이에 대한 스케일 조정

    public int size = 10;                       // 원활한 출력을 위한 포인트 클라우드를 나눌 개수


    void Start()
    {
        // for(int i = 0; i < size; i++)
        // {
        //     GeneratePointCloud(i);
        // }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            createPoint();
        }
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
        Debug.Log("width : " + width + " height : " + height);

        // 포인트 클라우드 생성
        GameObject pointCloudObject = new GameObject("PointCloud");
        pointCloudObject.transform.position = new Vector3(-1 * (width/2), -1 * (height/2), 750); // 카메라에서 10의 거리에 배치

        Mesh pointCloudMesh = new Mesh();   // 포인트 클라우드를 생성할 Mesh
        int arraySize = width * height; 
        Vector3[] vertices = new Vector3[arraySize];
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
                if(z == 0) continue;

                vertices[vertexIndex] = new Vector3(x, y, z);
                vertexIndex++;
            }
        }

        // Mesh에 vertices 및 indices 할당
        pointCloudMesh.vertices = vertices;
        pointCloudMesh.SetIndices(Enumerable.Range(0, arraySize).ToArray(), MeshTopology.Points, 0);

        // MeshFilter 및 MeshRenderer 추가
        MeshFilter meshFilter = pointCloudObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pointCloudObject.AddComponent<MeshRenderer>();

        // Mesh 및 Material 할당
        meshFilter.mesh = pointCloudMesh;
        meshRenderer.material = pointCloudMaterial;
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