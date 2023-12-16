using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonEvent : MonoBehaviour
{
    void Start()
    {

    }
    
    // 게임 종료 함수
    public void GameQuit()
    {
        // 게임 종료
        Application.Quit();
        Debug.Log("게임 종료");

        // 에디터에서 실행 중이면 종료 명령을 미 실행
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }

    // 버튼 클릭시 현재 자신의 text를 출력하는 함수
    public void PlayStart()
    {
        SceneManager.LoadScene("PlayScene");

    }


}
