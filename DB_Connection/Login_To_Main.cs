using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;


// POST Request - with "jsonstr" that represents the "CODE" Parameter.
public class Login_To_Main : MonoBehaviour
{
    //-------------Objects-----------------
    public User user1 = new User();

    //public Qustion_Answer Qjson = new Qustion_Answer();

    public Text text_ALL;
    public string jsonstr;  //json code athuntication
    public InputField userCodeInput;
    public string userCode;

    //-------------Objects-----------------
    void Start()
    {
        jsonstr = userCodeInput.text;

    }

    public void SaveUsername(InputField userCodeInput)
    {
        if (userCodeInput.text != null)
            jsonstr = userCode;
        Debug.Log(userCodeInput.text);
    }

    //void Start()
    //{
    //    StartCoroutine(CreatePost());
    //}

    public void request()
    {
        StartCoroutine(CreatePost());
    }

    public IEnumerator CreatePost()
    {

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        //headers.Add("Code", jsonstr);


        var byteCodeData = System.Text.Encoding.UTF8.GetBytes(userCodeInput.text);
        Debug.Log(byteCodeData.ToString());
        byte[] postData = Encoding.ASCII.GetBytes(JsonUtility.ToJson(user1));

        WWW www = new WWW(API.baseURL + "Auth", byteCodeData, headers);

        yield return www;

        Debug.Log("Created a user: " + www.text);
        user1 = JsonUtility.FromJson<User>(www.text);
        Debug.Log("Created a user: " + user1.Name);

   
        //text_Grade.text = user1.Grade.ToString();
        // user1.Grade;
        UserConstruct(user1.Id, user1.Name, user1.SchoolName, user1.Grade, user1.picture); //insert all user info to PlayerPref

        //text_SchoolName.text = user1.SchoolName;
        text_ALL.text = "Hello" + PlayerPrefs.GetString("studentName") + "\nEnjoy the game";

        print(PlayerPrefs.GetString("studentName"));
        print(PlayerPrefs.GetString("studentSchoolName"));
        print(PlayerPrefs.GetString("studentPicture"));
        print(PlayerPrefs.GetInt("studentId"));
        print(PlayerPrefs.GetInt("studentGrade"));





    }

    public void UserConstruct(int id, string name, string schoolName, int grade, string picture)
    {
        PlayerPrefs.SetInt("studentId", id);
        PlayerPrefs.SetString("studentName", name);
        PlayerPrefs.SetString("studentSchoolName", schoolName);
        PlayerPrefs.SetInt("studentGrade", grade);
        PlayerPrefs.SetString("studentPicture", picture);

    }
}