using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton3 : MonoBehaviour
{

    public Text answerText;
    private AnswerData answerData;
    private GameController3 gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController3>();
    }

    public void Setup1(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }


    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
      
    }
}
