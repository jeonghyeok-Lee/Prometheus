using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 상태를 나타내는 열거형
    public enum PlayerMode
    {
        Move,
        Cam
    }
    // 현재 플레이어 모드 변수
    private PlayerMode currentMode; // 현재 플레이어 모드
    private UIMainController uiMainController; // UI 메인 컨트롤러


    [SerializeField] private float moveSpeed;           // 케릭터 이동속도
    [SerializeField] private float mouseSensitivity;    // 마우스 감도

    private float currentMoveSpeed;                     // 현재 케릭터 이동속도
    private Rigidbody playerRigidbody;          // 케릭터 리지드바디
    private ICamUpdatable camUpdatable;         // 카메라 제어 인터페이스

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();            // 케릭터 리지드바디 컴포넌트 가져오기
        
        camUpdatable = GetComponentInChildren<ICamUpdatable>(); // 카메라 제어 인터페이스 컴포넌트 가져오기

        currentMoveSpeed = moveSpeed;                           // 현재 케릭터 이동속도 초기화

        currentMode = PlayerMode.Move;                          // 현재 플레이어 모드 초기화

        // StateController 컴포넌트 가져오기
        uiMainController = FindObjectOfType<UIMainController>();
        // 예외 처리: uiMainController 찾을 수 없는 경우 에러 출력
        if (uiMainController == null)
        {
            Debug.LogError("uiMainController가 현재 씬에 없음!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentMode == PlayerMode.Move){
            Move();

            // 마우스 좌클릭하며 마우스 이동시 카메라 화면 전환
            if (Input.GetMouseButton(0))
            {
                camUpdatable.UpdateRotation(gameObject, mouseSensitivity);
            }

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentMoveSpeed = moveSpeed * 10;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentMoveSpeed = moveSpeed;
            }
        }

        // T키 입력시 현재 Mode를 전환 및 텍스트 변경
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleMode();
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
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * currentMoveSpeed;

        // 리지드바디를 이용한 이동
        playerRigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void ToggleMode()
    {
        // 현재 모드가 Move인 경우 Cam으로 변경
        if (currentMode == PlayerMode.Move)
        {
            currentMode = PlayerMode.Cam;
            uiMainController.SetModeText("Cam");
        }
        // 현재 모드가 Cam인 경우 Move로 변경
        else if (currentMode == PlayerMode.Cam)
        {
            currentMode = PlayerMode.Move;
            uiMainController.SetModeText("Move");
        }
    }
}
