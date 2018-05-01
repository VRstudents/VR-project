using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;


// POST Request - with "jsonstr" that represents the "CODE" Parameter.
public class ExportingData : MonoBehaviour
{
    //public User user1;
    public Qustion_Answer Qjson = new Qustion_Answer();
    
    public Text text_User_Name;
    public Text text_SchoolName;
    //json code athuntication
    //static string jsonstr = "362532";
    


    void Start()
    {
        StartCoroutine(CreatePost());
    }

    IEnumerator CreatePost()
    {
        
        Dictionary<string, string> headers = new Dictionary<string, string>();
       // headers.Add("Code", jsonstr);
        headers.Add("Content-Type", "application/json");


        //var byteCodeData = System.Text.Encoding.UTF8.GetBytes(jsonstr);
        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(Qjson));

        WWW www = new WWW(API.baseURL+"QuestionAnswer", postData, headers);

        yield return www;

        Debug.Log("Created a qustion: " + Qjson.LessonId);
        Debug.Log("Created a user: " + www.text);
        //Creating the "User" Form Json Data
        Qjson = JsonUtility.FromJson<Qustion_Answer>(www.text);
        //show text on screen - 
        text_User_Name.text = Qjson.StudentId.ToString();
        text_SchoolName.text = Qjson.Result.ToString();
       // user1.Role;
       // user1.Grade;
    }

}