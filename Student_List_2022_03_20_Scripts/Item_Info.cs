using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Info  //학생 한 명에 대한 정보 클래스 설계
{
    public string m_Name = "";    // 학생 이름
    public int m_Korean = 0;      // 국어 점수  0 ~ 100
    public int m_English = 0;     // 영어 점수  0 ~ 100
    public int m_Math = 0;        // 수학 점수  0 ~ 100
    public float m_Total = 0.0f;  // 총점 0.0f ~ 300.0f
    public float m_Avg = 0.0f;    // 평균 0.0f ~ 100.0f

    public string m_ShowStudentInfo = ""; //검색을 통해서 나온 학생에 대한 정보 표시

    public Item_Info(string a_Name, int a_Korean, int a_English, int a_Math)
    {  //생성자 오버로딩 함수
        m_Name = a_Name;
        m_Korean = a_Korean;
        m_English = a_English;
        m_Math = a_Math;
        m_Total = (m_Korean + m_English + m_Math);
        m_Avg = m_Total / 3.0f;

    }
}