using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string m_Name = "";  //학생이름
    public int m_Kor = 0;       //국어점수
    public int m_Eng = 0;       //영어점수
    public int m_Math = 0;      //수학점수
    public int m_Total = 0;     //총점
    public float m_Avg = 0.0f;  //평균

    public void InputData(string Name, int Kor, int Eng, int Math)
    {
        m_Name  = Name;
        m_Kor   = Kor;
        m_Eng   = Eng;
        m_Math  = Math;
        m_Total = m_Kor + m_Eng + m_Math;
        m_Avg   = m_Total / 3.0f;
    }

    public void PrintInfo()
    {
        string str = string.Format("이름({0}) 국어({1}) 영어({2}) 수학({3}) 총점({4}) 평균({5:N2})",
                                    m_Name, m_Kor, m_Eng, m_Math, m_Total, m_Avg);
        Debug.Log(str);
    }
}

