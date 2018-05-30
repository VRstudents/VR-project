using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

public class GameController2 : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public Text highScoreDisplay;
    public Image bravo;

    private DataController2 dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataController2>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
        bravo.enabled = false;
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton2 answerButton = answerButtonGameObject.GetComponent<AnswerButton2>();
            answerButton.Setup1(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        bravo.enabled = false;
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
            bravo.enabled = true;
            //save "correct" answer and qustion number
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            //save "Incorrect" answer and qustion number
            EndRound();
        }
        SendQustionPerAnswer(questionIndex, isCorrect);

    }

    public void EndRound()
    {
        isRoundActive = false;
        bravo.enabled = false;

        dataController.SubmitNewPlayerScore(playerScore);
        SendPerLesson(playerScore);

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Room");
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }
    private void SendQustionPerAnswer(int qustionNum, bool result)//לזכור להעתיק את זה לכל אחד מהמשחקים
    {
        Qustion_Answer Qjson = new Qustion_Answer();
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        Qjson.StudentId = PlayerPrefs.GetInt("studentId");
        Qjson.LessonId = PlayerPrefs.GetInt("LessonMathId");
        //PlayerPrefs.GetInt("mathLassonId");// ליצור משתנה בשאר מסכים
        Qjson.QuestionNum = qustionNum;
        Qjson.Result = result;
        //var byteCodeData = System.Text.Encoding.UTF8.GetBytes(jsonstr);
        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(Qjson));

        WWW www = new WWW(API.baseURL + "QuestionAnswer", postData, headers);
    }
    public void SendPerLesson(int SCORE)
    {
        FinishLesson FinalScore = new FinishLesson();
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        Debug.Log(PlayerPrefs.GetInt("studentId"));
        FinalScore.StudentId = PlayerPrefs.GetInt("studentId");
        Debug.Log(PlayerPrefs.GetInt("LessonMathId"));
        FinalScore.LessonId = PlayerPrefs.GetInt("LessonMathId");
        FinalScore.Result = SCORE;

        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(FinalScore));

        WWW www = new WWW(API.baseURL + "FinishLesson", postData, headers);

    }
    // Update is called once per frame
    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();

            if (timeRemaining <= 0f)
            {
                EndRound();
            }

        }
    }
}