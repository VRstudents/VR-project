using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsVRStudents {

    int studentId;
    string Name;
    string SchoolName;
    int Grade;
    string picture;
     

     public void UserConstruct(int id , string name , string schoolName , int grade , string picture)
    {
        PlayerPrefs.SetInt("studentId", id);
        PlayerPrefs.SetString("studentName", name);
        PlayerPrefs.SetString("studentSchoolName", schoolName);
        PlayerPrefs.SetInt("studentGrade", grade);
        PlayerPrefs.SetString("studentPicture", picture);

    } 

	
	
}
