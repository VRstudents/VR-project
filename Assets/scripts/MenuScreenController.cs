using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{

    public void StartGame1()
    {
        SceneManager.LoadScene("Room");
    }
    public void SWITHWE1()
    {
        PlayerPrefs.SetInt("LessonMathId", 1);
        SceneManager.LoadScene("Game");
    }

    public void SWITHWE2()
    {
        PlayerPrefs.SetInt("LessonMathId", 2);
        SceneManager.LoadScene("Game2");
    }
    public void SWITHWE3()
    {
        PlayerPrefs.SetInt("LessonMathId", 3);
        SceneManager.LoadScene("Game3");
    }
    public void SWITHWE4()
    {
        PlayerPrefs.SetInt("LessonMathId", 4);
        SceneManager.LoadScene("Game4");
    }
    public void SWITHWE5()
    {
        SceneManager.LoadScene("001");
    }
    public void SWITHWE6()
    {
        SceneManager.LoadScene("002");
    }
}
