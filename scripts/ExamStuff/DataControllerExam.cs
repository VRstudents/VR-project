using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataControllerExam : MonoBehaviour
{
    private PlayerProgress playerProgress;
    public ExamJson[] allRoundData;
    

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadExam();
        
    }

    public ExamJson GetCurrentExamJson()
    {
        return allRoundData[0];
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    public void LoadExam()
    {
        WWW www = new WWW(API.baseURL + "/GetChallenge/" + PlayerPrefs.GetInt("studentId")); //### = URL Table name 

        // Wait until reseponse will return
        while (!www.isDone) { }

        Debug.Log("Exam: " + www.text);

        //classes = JsonHelper.GetJsonArray<StudentsClasses>(www.text);
        //allRoundData = JsonUtility.FromJson<ExamJson[]>(www.text);
        allRoundData = JsonHelper.GetJsonArray<ExamJson>(www.text);
        


    }

    }