using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

namespace redner
{
    [System.Serializable]
    public class CameraData
    {
        public Point[] Points;
    }

    [System.Serializable]
    public class Point
    {
        public float X;
        public float Y;
        public float Z;
    }

    public class TestRender : MonoBehaviour
    {
        public TextAsset pointCloudJsonData;
        public Shader pointCloudShader;

        private Material pointCloudMaterial;
        private Vector3[] pointCloudPoints;
        private Mesh pointCloudMesh;


        void Start()
        {
            pointCloudMaterial = new Material(pointCloudShader);
            LoadPointCloudData();
        }

        void LoadPointCloudData()
        {
            // JSON 데이터 파싱
            CameraData cameraData = JsonConvert.DeserializeObject<CameraData>(pointCloudJsonData.text);

            // 포인트 클라우드 좌표 초기화
            pointCloudPoints = new Vector3[cameraData.Points.Length];

            // JSON 데이터에서 포인트 클라우드 좌표 추출
            for (int i = 0; i < cameraData.Points.Length; i++)
            {
                pointCloudPoints[i] = new Vector3(
                    cameraData.Points[i].X,
                    cameraData.Points[i].Y,
                    cameraData.Points[i].Z
                );
            }

            // Mesh 초기화
            InitializeMesh();

            Debug.Log("Point Cloud Data Loaded. Size: " + pointCloudPoints.Length);
        }

        void InitializeMesh()
        {
            pointCloudMesh = new Mesh();
            pointCloudMesh.vertices = pointCloudPoints;
            pointCloudMesh.SetIndices(Enumerable.Range(0, pointCloudPoints.Length).ToArray(), MeshTopology.Points, 0);
        }

        void OnRenderObject()
        {
            RenderPointCloud();
        }

        void RenderPointCloud()
        {
            // 포인트 클라우드 렌더링
            pointCloudMaterial.SetPass(0);
            Graphics.DrawMeshNow(pointCloudMesh, transform.localToWorldMatrix);
        }

        void OnDestroy()
        {
            if (pointCloudMesh != null)
            {
                Destroy(pointCloudMesh);
            }
        }
    }
}
