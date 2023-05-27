using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 5.0f;
    private Vector3 move;
    private CharacterController characterController;
    private Vector3 characterForward;                                               // 플레이어의 forward;
    private Vector3 characterRight;                                                 // 플레이어의 right;
    private RotateToMouse rotateToMouse;                                            // 마우스 이동으로 카메라 회전
    private Camera mainCamera;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rotateToMouse = GetComponent<RotateToMouse>();
        mainCamera = Camera.main;

        //초기화
        characterForward = transform.forward;
        characterRight = transform.right;
    }


    // Update is called once per frame
    void Update()
    {
        // WASD 입력 받기
        // GetAxis (보간 사용) -> GetAxisRaw(보간 미사용) - 키 때는 즉시 종료
        float moveX = Input.GetAxisRaw("Horizontal");   
        float moveZ = Input.GetAxisRaw("Vertical");

        characterForward = transform.forward;
        characterRight = transform.right;
        characterForward.Normalize();
        characterRight.Normalize();

        // 이동 방향 계산
        move = characterForward * moveZ + characterRight * moveX;
        move.Normalize();

        // 캐릭터 이동
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // 마우스 좌클릭하며 마우스 이동시 카메라 화면 전환
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            rotateToMouse.CalculateRotation(mouseX, mouseY);
        }
    }
}
