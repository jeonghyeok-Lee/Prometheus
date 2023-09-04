using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;           // 케릭터 이동속도
    [SerializeField] private float mouseSensitivity;    // 마우스 감도

    private Rigidbody playerRigidbody;          // 케릭터 리지드바디
    private ICamUpdatable camUpdatable;         // 카메라 제어 인터페이스

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();            // 케릭터 리지드바디 컴포넌트 가져오기
        
        camUpdatable = GetComponentInChildren<ICamUpdatable>(); // 카메라 제어 인터페이스 컴포넌트 가져오기
    }


    // Update is called once per frame
    void Update()
    {
        Move();

        // 마우스 좌클릭하며 마우스 이동시 카메라 화면 전환
        if (Input.GetMouseButton(0))
        {
            camUpdatable.UpdateRotation(gameObject, mouseSensitivity);
        }
    }

    private void Move()
    {
        // 케릭터 이동키 입력   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // 이동 값 계산
        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;

        // 이동 값 정규화
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        // 리지드바디를 이용한 이동
        playerRigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
