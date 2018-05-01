using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StudentsClasses : MonoBehaviour
{
    public int Id;
    public string Teacher;

    void Start()
    {
        PlayerPrefs.SetInt("ClassMathId", Id);
        PlayerPrefs.SetString("TeacherMathName", Teacher);
    }
    //public void UserConstruct(int Id, string Teacher)
    //{
    //    PlayerPrefs.SetInt("ClassMathId", Id);
    //    PlayerPrefs.SetString("TeacherMathName", Teacher);
    //}
    //public void UserConstruct2(int Id, string Teacher)
    //{
    //    PlayerPrefs.SetInt("ClassSciId", Id);
    //    PlayerPrefs.SetString("TeacherSciName", Teacher);
    //}

}