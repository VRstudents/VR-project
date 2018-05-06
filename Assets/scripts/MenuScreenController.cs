using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void SWITHWE()
    {
        SceneManager.LoadScene("Game2");
    }
    public void SWITHWE1()
    {
        SceneManager.LoadScene("001");
    }
    public void SWITHWE2()
    {
        SceneManager.LoadScene("002");
    }
}
