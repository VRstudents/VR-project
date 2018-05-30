using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExamJson 
{
    public int ClassId;
    public string Category;
    public int[] QuestionIDs;
    public ExamQuestionsDTO[] Questions;
    

}
