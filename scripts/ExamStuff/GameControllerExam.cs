using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

public class GameControllerExam : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public Text highScoreDisplay;

    private DataControllerExam dataController;
    private ExamJson currentRoundData;
    private ExamQuestionsDTO[] questionPool;

    private bool isRoundActive;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataControllerExam>();
        currentRoundData = dataController.GetCurrentExamJson();
        questionPool = currentRoundData.Questions;
        

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        ExamQuestionsDTO questionData = questionPool[questionIndex];
        AnswerDataExam[] AnswerArr = new AnswerDataExam[4];
        AnswerArr[0] = questionData.AnswerA;
        AnswerArr[1] = questionData.AnswerB;
        AnswerArr[2] = questionData.AnswerC;
        AnswerArr[3] = questionData.AnswerD;

        questionDisplayText.text = questionData.Question;
        for (int i = 0; i < 4; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButtonExam answerButton1 = answerButtonGameObject.GetComponent<AnswerButtonExam>();
            answerButton1.Setup1(AnswerArr[i]);
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

    public void AnswerButtonClicked(bool IsRight)
    {
        if (IsRight)
        {
            playerScore += 10;
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
        //SendQustionPerAnswer(questionIndex, IsRight);

    }

    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Room");
    }



    //private void SendQustionPerAnswer(int qustionNum, bool result)//לזכור להעתיק את זה לכל אחד מהמשחקים
    //{
    //    Qustion_Answer Qjson = new Qustion_Answer();
    //    Dictionary<string, string> headers = new Dictionary<string, string>
    //    {
    //        { "Content-Type", "application/json" }
    //    };
    //    Qjson.StudentId = PlayerPrefs.GetInt("studentId");
    //    Qjson.LessonId = 777;
    //    Qjson.Result = result;
    //    byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(Qjson));

    //    WWW www = new WWW(API.baseURL + "QuestionAnswer", postData, headers);
    //}


    // Update is called once per frame
    void Update()
    {
    }
}