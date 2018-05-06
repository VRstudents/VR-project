using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class timeController: MonoBehaviour
{

    public float targetTime = 60f;
    public Text scoreDisplayText;

    void Update()
    {

        targetTime -= Time.deltaTime;
        scoreDisplayText.text = targetTime.ToString();
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        SceneManager.LoadScene("Room");
    }


}

