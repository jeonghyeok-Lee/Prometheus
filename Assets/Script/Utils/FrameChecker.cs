using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameCheck
{
    public class FrameChecker : MonoBehaviour
    {
        private float deltaTime = 0.0f;

        private GUIStyle style;
        private Rect rect;
        private float msec;
        private float fps;
        private float worstFps = 100f;
        private string text;

        void Awake()         // 
        {

            int w = Screen.width, h = Screen.height;    // 현재 게임 화면의 크기

            rect = new Rect(0, 0, w, h * 4 / 100);

            style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 4 / 100;
            style.normal.textColor = Color.cyan;

            StartCoroutine("worstReset");

        }

        IEnumerator worstReset() //코루틴으로 15초 간격으로 최저 프레임 리셋해줌.
        {
            while (true)
            {
                yield return new WaitForSeconds(15f);
                worstFps = 100f;
            }
        }

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fps = 1.0f / deltaTime;
            msec = deltaTime * 1000.0f;
        }

        void OnGUI()                                                                        //소스로 GUI 표시.
        {

/*            msec = deltaTime * 1000.0f;*/
/*            fps = 1.0f / deltaTime;                                                         //초당 프레임 - 1초에*/

            if (fps < worstFps)                                                             //새로운 최저 fps가 나왔다면 worstFps 바꿔줌.
                worstFps = fps;
            text = msec.ToString("F1") + "ms (" + fps.ToString("F1") + ") //worst : " + worstFps.ToString("F1");
            GUI.Label(rect, text, style);
        }
    }

}