using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticle : MonoBehaviour
{
private ParticleSystem particleSystem; // 파티클 시스템 컴포넌트

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// 파티클 시스템 초기화 함수
    /// </summary>
    public void InitializeParticleSystem()
    {
        particleSystem.Clear(); // 기존 파티클 제거
        particleSystem.Stop();  // 시스템 정지
        particleSystem.Play();  // 시스템 시작
    }

    /// <summary>
    /// 파티클 생성 함수
    /// </summary>
    /// <param name="position"></param>
    public void CreateParticle(Vector3 position)
    {
        // 파티클 시스템 컴포넌트 가져오기
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        // 포인트 위치 설정
        emitParams.position = position;

        // 파티클 시스템에 포인트 추가
        particleSystem.Emit(emitParams, 1);
    }

    /// <summary>
    /// 파티클 시스템 중단 함수
    /// </summary>
    public void StopParticleSystem()
    {
        particleSystem.Stop();
    }
}