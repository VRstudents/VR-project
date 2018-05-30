using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

public class manager : MonoBehaviour {

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;
    public GameObject roundEndDisplay;
    public GameObject canvas;
    private bool _init = false;
    private int _matches = 0;

    public float timeRemaining;
    public Text scoreDisplayText;
    private bool isRoundActive;
 
    void Start()
    {
        UpdateTimeRemainingDisplay();
        isRoundActive = true;
    }
    void Update()
    {
        if (!_init)
        {
            initializeCards();

        }
        if (Input.GetMouseButtonUp(0))
            checkCards();
        
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

    void initializeCards()
    {
        for (int id = 0; id < 2; id++)
        {
            for (int i = 1; i < 6; i++)
            {
                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<card>().initialized);
                }
                cards[choice].GetComponent<card>().cardValue = i;
                cards[choice].GetComponent<card>().initialized = true;
            }
        }

        foreach (GameObject c in cards)
            c.GetComponent<card>().setupGraphics();

        if (!_init)
            _init = true;
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }
    public Sprite getCardFace(int i)
    {
        return cardFace[i - 1];
    }

    void checkCards()
    {
        List<int> c = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<card>().state == 1)
                c.Add(i);
        }

        if (c.Count == 2)
            cardComparison(c);
    }

    void cardComparison(List<int> c)
    {
        card.DO_NOT = true;

        int x = 0;

        if (cards[c[0]].GetComponent<card>().cardValue == cards[c[1]].GetComponent<card>().cardValue)
        {
            x = 2;
            _matches++;
            matchText.text = "Number of Matches: " + _matches;
            if (_matches == 5)
            {
                canvas.SetActive(false);
                roundEndDisplay.SetActive(true);
        
               SceneManager.LoadScene("Room");
            }
        }
        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<card>().state = x;
            cards[c[i]].GetComponent<card>().falseCheck();
        }
    }
    private void UpdateTimeRemainingDisplay()
    {


        scoreDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();


    }
    public void EndRound()
    {
        SendZikaron(_matches*2);
        isRoundActive = false;
        canvas.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void SendZikaron(int SCORE)
    {
        FinishLesson FinalScore = new FinishLesson();
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        Debug.Log(PlayerPrefs.GetInt("studentId"));
        FinalScore.StudentId = PlayerPrefs.GetInt("studentId");
        Debug.Log(PlayerPrefs.GetInt("LessonSciId"));
        FinalScore.LessonId = PlayerPrefs.GetInt("LessonSciId");
        FinalScore.Result = SCORE;

        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(FinalScore));

        WWW www = new WWW(API.baseURL + "FinishLesson", postData, headers);

    }
}﻿


