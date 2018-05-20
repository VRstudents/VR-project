using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

public class GameController3 : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public Text highScoreDisplay;

    private DataController3 dataController;
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
        dataController = FindObjectOfType<DataController3>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
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

            AnswerButton3 answerButton = answerButtonGameObject.GetComponent<AnswerButton3>();
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
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
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

        dataController.SubmitNewPlayerScore(playerScore);

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
        Qjson.LessonId = 2;
        //PlayerPrefs.GetInt("mathLassonId");// ליצור משתנה בשאר מסכים
        Qjson.QustionNum = qustionNum;
        Qjson.Result = result;
        //var byteCodeData = System.Text.Encoding.UTF8.GetBytes(jsonstr);
        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(Qjson));

        WWW www = new WWW(API.baseURL + "QuestionAnswer", postData, headers);
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