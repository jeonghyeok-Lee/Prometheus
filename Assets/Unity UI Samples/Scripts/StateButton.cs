using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StateButton : MonoBehaviour
{
    private string state;
    private Text label;

    void Start()
    {
        if (GameObject.Find("Continue")) {
            Debug.Log("Find Continue");
            
            GameObject continueObject = GameObject.Find("Continue");
            Transform labelTransform = continueObject.transform.Find("Label");
            if (labelTransform != null) {
                Debug.Log("Find Label Component");
            } else {
                Debug.Log("Not Found Label Component");
            }
        }

        state = "off";
        if(gameObject.transform.Find("Label")) {
            Debug.Log("Find Label");
            label = gameObject.transform.Find("Label").GetComponent<Text>();
        } else {
            Debug.Log("Not Found");
        }
    }

    public void OnClick()
    {
        Transform buttonTransform = transform;
        Transform childTransform = buttonTransform.Find("Label");
        string buttonText = childTransform.GetComponent<Text>().text;

        SocketScript socketMessage = FindObjectOfType<SocketScript>();
        OnOffButton onOffButtonState = FindObjectOfType<OnOffButton>();
        PauseResumeButton pauseResumeButtonState = FindObjectOfType<PauseResumeButton>();

        if ("ON" == buttonText || "OFF" == buttonText)
        {
            if ("off" == state) {
                state = "on";
                label.text = "OFF";
                Debug.Log("Button Text : " + buttonText + ", State : " + state);

                socketMessage.SetMessage("on");
                pauseResumeButtonState.SetState(true);
            } else if ("pause" == state) {
                state = "on";
                label.text = "OFF";
                Debug.Log("Restart, State : " + state);

                socketMessage.SetMessage("on");
                pauseResumeButtonState.SetState(true);
            } else {
                state = "off";
                label.text = "ON";
                Debug.Log("Button Text : " + buttonText + ", State : " + state);

                socketMessage.SetMessage("off");
                pauseResumeButtonState.SetState(false);
            }
        } else if ("Pause" == buttonText) {
            state = "pause";
            label.text = "Resume";
            Debug.Log("Button Text : " + buttonText + ", State : " + state);
            
            socketMessage.SetMessage("pause");
            onOffButtonState.SetState(false);
        } else if ("Resume" == buttonText) {
            state = "on";
            label.text = "Pause";
            Debug.Log("Button Text : " + buttonText + ", State : " + state);
            
            socketMessage.SetMessage("on");
            onOffButtonState.SetState(true);
        }
    }
}
