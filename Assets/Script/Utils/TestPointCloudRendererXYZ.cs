// public class TestPointCloudRendererXYZ : MonoBehaviour
// {
// public ComputeShader pointCloudShader;
//     public int pointCount = 1000000; // 포인트 클라우드의 예상 포인트 수

//     ComputeBuffer pointBuffer;

//     void Start()
//     {
//         InitializePointCloud();
//     }

//     void InitializePointCloud()
//     {
//         // 포인트 클라우드 데이터 생성 및 GPU에 전송
//         PointData[] pointData = new PointData[pointCount];
//         for (int i = 0; i < pointCount; i++)
//         {
//             pointData[i].position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
//         }

//         pointBuffer = new ComputeBuffer(pointCount, sizeof(float) * 3);
//         pointBuffer.SetData(pointData);

//         pointCloudShader.SetBuffer(0, "buffer", pointBuffer);
//     }

//     void Update()
//     {
//         // Compute Shader를 사용하여 포인트 클라우드 렌더링
//         pointCloudShader.Dispatch(0, pointCount / 512, 1, 1);
//     }

//     void OnDestroy()
//     {
//         // 사용이 끝난 버퍼 해제
//         pointBuffer.Release();
//     }

//     struct PointData
//     {
//         public Vector3 position;
//     }
// }