using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class SwitchBetweenCameras : MonoBehaviour
{

    //public Transform Player;

    public Camera LoginCam;

    public Camera roomCam;

    public Button yourButton;


    public bool camSwitch = false;

    void Start()
    {
        roomCam.gameObject.SetActive(false);
        LoginCam.gameObject.SetActive(true);
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        LoginCam.gameObject.SetActive(false);
        roomCam.gameObject.SetActive(!true);
    }
           
       

    public void ChangeCam()
    {
            camSwitch = !camSwitch;
            LoginCam.gameObject.SetActive(camSwitch);
            roomCam.gameObject.SetActive(!camSwitch);
            camSwitch = !camSwitch;
    }
}

