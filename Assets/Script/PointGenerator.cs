using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSetting;

public class PointGenerator : MonoBehaviour
{
    [SerializeField] private PointParticle pointParticlePrefab; // PointParticle 프리팹
    private ScanDataArray scanDataArray;                        // 포인트 데이터 [위치 데이터를 가지고 있는 배열 변수]
    private float distanceScale = 0.01f;                        // 거리 스케일
    
    /// <summary>
    /// 테스트용 변수들
    /// </summary>
    private float testY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 포인트 데이터 초기화
        SettingPointData();

        // PointParticle을 인스턴스화하고 초기화
        PointParticle pointParticle = Instantiate(pointParticlePrefab);
        pointParticle.InitializeParticleSystem();

        // 스캔 데이터를 파티클로 변환하여 추가
        ConvertAndAddScanData(pointParticle);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // 포인트 데이터 초기화
            SettingPointData();

            // PointParticle을 인스턴스화하고 초기화
            PointParticle pointParticle = Instantiate(pointParticlePrefab);
            pointParticle.InitializeParticleSystem();

            // 스캔 데이터를 파티클로 변환하여 추가
            ConvertAndAddScanData(pointParticle);
        }
    }

    /// <summary>
    /// 포인트 데이터 초기화
    /// </summary>
    private void SettingPointData()
    {
        DataController dataController = new DataController("json", "json/data00001");
        scanDataArray = dataController.LoadData(); // 데이터 로드
    }

    void ConvertAndAddScanData(PointParticle pointParticle)
    {
        foreach (ScanData[] scanDataRow in scanDataArray.data)
        {
            foreach (ScanData scanData in scanDataRow)
            {
                // 스캔 데이터로부터 거리와 각도
                float distance = float.Parse(scanData.distance) * distanceScale;
                float angle = float.Parse(scanData.angle);

                // 각도를 라디안으로 변환
                float angleInRadians = Mathf.Deg2Rad * angle;

                // 거리와 각도를 벡터로 변환하여 포인트 위치 계산
                Vector3 pointPosition = new Vector3(
                    distance * Mathf.Cos(angleInRadians),
                    testY, // 고정된 y 좌표 (자동차 형태를 유지)
                    distance * Mathf.Sin(angleInRadians)
                );

                // PointParticle을 사용하여 파티클 생성
                pointParticle.CreateParticle(pointPosition);
            }
        }
        Debug.Log("파티클 생성 완료");

        testY += 0.1f;

        // 파티클 시스템 중단
        pointParticle.StopParticleSystem();
    }
}