
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GET_request : MonoBehaviour
{

    public Lessons lesson;
    public Text textToShow;

    void Start()
    {
        StartCoroutine(LoadUser());
    }

    IEnumerator LoadUser()
    {
        // here we add to BaseUrl the specific ID for user/class/lesson
        WWW www = new WWW(API.baseURL + "GetLessons/1/5");
        yield return www;
        NewMethod(www);
        Debug.Log("Lesson: " + www.text);
        textToShow.text = lesson.Name;
    }

    private void NewMethod(WWW www)
    {
        lesson = JsonUtility.FromJson<Lessons>(www.text);
    }
}