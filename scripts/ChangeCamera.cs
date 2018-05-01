using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour
{
    public Camera RoomViewCamera;
    public Camera LoginViewCamera;

    public void ShowRoomView()
    {
        RoomViewCamera.enabled = false;
        LoginViewCamera.enabled = true;
    }

    public void ShowLoginView()
    {
        RoomViewCamera.enabled = true;
        LoginViewCamera.enabled = false;
    }
}