using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButtonExam : MonoBehaviour
{

    public Text answerText;
    public bool IsRight;
    private AnswerDataExam answerData;
    private GameControllerExam gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameControllerExam>();
    }

    public void Setup1(AnswerDataExam data)
    {
        answerText.text = data.Answer;
        IsRight = data.IsRight;
    }


    public void HandleClick()
    {
        gameController.AnswerButtonClicked(IsRight);
      
    }
}
