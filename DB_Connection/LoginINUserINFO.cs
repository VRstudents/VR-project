using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using System;


// POST Request - with "jsonstr" that represents the "CODE" Parameter.

public class LoginINUserINFO : MonoBehaviour
{

    //-------------Objects-----------------

    //public GameObject x = user1;
    //public Qustion_Answer Qjson = new Qustion_Answer();
    public Text username;
    public Text schoolName;
    public int StudentId;
    void Start()
    {
        schoolName.text = PlayerPrefs.GetString("studentSchoolName");
        username.text = PlayerPrefs.GetString("studentName");
        StudentId = PlayerPrefs.GetInt("studentId");
        

    }
}
