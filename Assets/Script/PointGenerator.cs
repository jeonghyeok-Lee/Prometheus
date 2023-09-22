using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSetting;

public class PointGenerator : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem; // 파티클 시스템을 가리키는 참조

    private ScanDataArray scanDataArray;                // 포인트 데이터 [위치 데이터를 가지고 있는 배열 변수]
    private float distanceScale = 0.05f;                // 거리 스케일

    // Start is called before the first frame update
    void Start()
    {
        // 포인트 데이터 초기화
        SettingPointData();
        
        // 파티클 시스템을 초기화하고 포인트를 추가하는 함수 호출
        InitializeParticleSystem();   

        // 스캔 데이터를 파티클로 변환하여 추가
        ConvertAndAddScanData();
    }

    /// <summary>
    /// 포인트 데이터 초기화
    /// </summary>
    private void SettingPointData()
    {
        DataController dataController = new DataController("json", "json/data00001");

        scanDataArray = dataController.LoadData();    // 데이터 로드
    }

    void InitializeParticleSystem()
    {
        // 파티클 시스템 초기화
        particleSystem.Clear(); // 기존 파티클 제거
        particleSystem.Stop();  // 시스템 정지

        // 시스템을 다시 시작하여 파티클을 표시
        particleSystem.Play();
    }

    void ConvertAndAddScanData()
    {
        // 파티클 시스템 컴포넌트 가져오기
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        foreach (ScanData[] scanDataRow in scanDataArray.data)
        {
            foreach (ScanData scanData in scanDataRow)
            {
                // 스캔 데이터로부터 거리와 각도 가져오기
                float distance = float.Parse(scanData.distance) * distanceScale;
                float angle = float.Parse(scanData.angle);

                // 각도를 라디안으로 변환
                float angleInRadians = Mathf.Deg2Rad * angle;

                // 거리와 각도를 벡터로 변환하여 포인트 위치 계산
                Vector3 pointPosition = new Vector3(
                    distance * Mathf.Cos(angleInRadians),
                    0.0f, // 고정된 y 좌표 (자동차 형태를 유지)
                    distance * Mathf.Sin(angleInRadians)
                );

                // 포인트 위치 설정
                emitParams.position = pointPosition;

                // 파티클 시스템에 포인트 추가
                particleSystem.Emit(emitParams, 1);
            }
        }
    }
}
