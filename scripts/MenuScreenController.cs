using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void Switch_Room()
    {
        SceneManager.LoadScene("Room");
    }
}
