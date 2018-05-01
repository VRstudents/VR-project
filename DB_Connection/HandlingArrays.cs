using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using System;


public class HandlingArrays : MonoBehaviour
{
    
    public StudentsClasses[] classes;
    public Text MathTeacher;
    public int MathId;
    public Text SciTeacher;
    public int SciId;

    void Start()
    {
        StartCoroutine(LoadClasses());
        
    }

    IEnumerator LoadClasses()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        WWW www = new WWW(API.baseURL + "/GetClasses/" + PlayerPrefs.GetInt("studentId").ToString()); //### = URL Table name
        yield return www;
        classes = JsonHelper.getJsonArray<StudentsClasses>(www.text);
        Debug.Log("www: " + www.text);
        Debug.Log("classes: " + www.text);
        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(classes));
        OurClass(classes);

    }

    public void OurClass(StudentsClasses[] classes)
    {
        for (int i=0; i< classes.Length; i++)
        { 
            MathId = classes[i].Id;
            MathTeacher.text = classes[i].Teacher;
            if (i == 1)
            {
                SciTeacher.text = classes[i].Teacher;
                SciId = classes[i].Id;
            }


        }
            
    }
}