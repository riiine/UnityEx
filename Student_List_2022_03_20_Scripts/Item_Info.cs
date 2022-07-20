using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Info  //�л� �� �� ���� ���� Ŭ���� ����
{
    public string m_Name = "";    // �л� �̸�
    public int m_Korean = 0;      // ���� ����  0 ~ 100
    public int m_English = 0;     // ���� ����  0 ~ 100
    public int m_Math = 0;        // ���� ����  0 ~ 100
    public float m_Total = 0.0f;  // ���� 0.0f ~ 300.0f
    public float m_Avg = 0.0f;    // ��� 0.0f ~ 100.0f

    public string m_ShowStudentInfo = ""; //�˻��� ���ؼ� ���� �л��� ���� ���� ǥ��

    public Item_Info(string a_Name, int a_Korean, int a_English, int a_Math)
    {  //������ �����ε� �Լ�
        m_Name = a_Name;
        m_Korean = a_Korean;
        m_English = a_English;
        m_Math = a_Math;
        m_Total = (m_Korean + m_English + m_Math);
        m_Avg = m_Total / 3.0f;

    }
}