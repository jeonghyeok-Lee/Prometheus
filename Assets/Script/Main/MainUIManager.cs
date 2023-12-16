using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    public CanvasGroup uiCanvasGroup; // UI 요소의 CanvasGroup 컴포넌트 [ 마우스 클릭 시 보여줄 UI]
    public CanvasGroup uiTitle; // UI 요소의 CanvasGroup 컴포넌트 [ 마우스 클릭 시 사라질 UI]

    private bool isUIVisible = false;   // UI가 보이는지 여부
    private float loadUITime = 1.5f;    // UI가 보이는 시간


    // Start is called before the first frame update
    void Start()
    {
        // 초기 설정: uiTitle은 보이지 않게, uiCanvasGroup은 투명하게 설정
        InitializeUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isUIVisible){
            isUIVisible = true;
            StartCoroutine(LoadUI());   //  UI를 나타내는 코루틴 함수 호출
        }
        
    }

    // UI초기 설정
    private void InitializeUI()
    {
        uiTitle.alpha = 1f;
        uiTitle.interactable = true;
        uiTitle.blocksRaycasts = true;

        uiCanvasGroup.alpha = 0f;
        uiCanvasGroup.interactable = false;
        uiCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator LoadUI()
    {
        float elapsedTime = 0f; // 경과 시간

        // uiTitle을 서서히 투명하게
        while (elapsedTime < loadUITime)
        {
            uiTitle.alpha = Mathf.Lerp(1f, 0f, elapsedTime / loadUITime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // uiTitle 비활성화
        uiTitle.interactable = false;
        uiTitle.blocksRaycasts = false;

        elapsedTime = 0f; // 경과 시간 초기화

        // uiCanvasGroup을 서서히 나타내기
        while (elapsedTime < loadUITime)
        {
            uiCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / loadUITime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // uiCanvasGroup 완전히 나타난 후에 상호작용 및 레이캐스트 활성화
        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;
    }
}
