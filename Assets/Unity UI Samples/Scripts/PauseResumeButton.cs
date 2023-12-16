using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResumeButton : MonoBehaviour
{
    private bool state;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (true == state) {
            button.interactable = true;
        } else {
            button.interactable = false;
        }
    }

    public bool GetState()
    {
        return state;
    }

    public void SetState(bool newState)
    {
        state = newState;
    }
}
