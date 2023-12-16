using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonEvent : MonoBehaviour
{
    // 클릭한 오브젝트의 자식 오브젝트 중 Text 컴포넌트를 가진 오브젝트의 text를 가져옴
    public void PrintText()
    {
        GameObject clickTarget = EventSystem.current.currentSelectedGameObject; // 클릭한 오브젝트를 가져옴
        
        Transform[] children = clickTarget.GetComponentsInChildren<Transform>(); // 클릭한 오브젝트의 자식 오브젝트들을 가져옴

        foreach(Transform child in children)
        {
            if(child.name == "Label")
            {
                Debug.Log(child.GetComponent<Text>().text); // 클릭한 오브젝트의 자식 오브젝트 중 Text 컴포넌트를 가진 오브젝트의 text를 가져옴
            }
        }

    }
}
