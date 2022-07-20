using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    public Button m_Even_Btn;
    public Button m_Odd_Btn;

    public Text m_UserInfo_Text;
    public Text m_Result_Text;

 
    int m_Money = 1000;  //������ ���� �ݾ�
    int m_WinCount = 0;  //�¸� ī��Ʈ   
    int m_LostCount = 0; //�й� ī��Ʈ

    [Header("--- Gamble ---")]
    public Slider m_Gameble_Slider;
    public Text m_Gameble_Text;
    int m_Gameble = 100;  //�� �ݾ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        if (m_Even_Btn != null)
            m_Even_Btn.onClick.AddListener(EvenBtnFunc);
        //�Լ� ������ ��� ���

        if (m_Odd_Btn != null)
            m_Odd_Btn.onClick.AddListener(OddBtnFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (1.0f <= m_Gameble_Slider.value || m_Money < 100)
            m_Gameble = m_Money;
        else
            m_Gameble = 100 + (int)(m_Gameble_Slider.value * (m_Money - 100));

        m_Gameble_Text.text = "�ݾװɱ� : " + m_Gameble;
    }

    void EvenBtnFunc()  //¦�� ��ư ������ �� ���� �Լ�
    {
        if (m_Money <= 0)
            return;     //<-- ��� �Լ��� ���� ������ ��ɾ�
             
        //Debug.Log("¦�� ��ư�� �������.");

        // ������ 0.¦�� ����  1. Ȧ�� ���� ���·� ������ ����
        int a_UserSel = 0;
        int a_DiceNum = Random.Range(1, 7); //1 ~ 6 ������ �߻�

        string a_StrCom = "¦��";

        if ((a_DiceNum % 2) == 1)
            a_StrCom = "Ȧ��";

        // ����
        if(a_UserSel == (a_DiceNum % 2)) //������
        {
            m_Result_Text.text = "�ֻ��� ���� (" + a_DiceNum + ")(" +
                a_StrCom + ") ���߼̽��ϴ�.";

            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //Ʋ�����
        {
            m_Result_Text.text = "�ֻ��� ���� (" + a_DiceNum + ")(" +
                a_StrCom + ") Ʋ���̽��ϴ�.";

            m_LostCount++;
            m_Money -= m_Gameble;   //200;

            if(m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //Ʋ�����
        // ����

        // ���� ���� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                            " : ��(" + m_WinCount + ")" +
                            " : ��(" + m_LostCount + ")";
        // ���� ���� UI ����
    }//void EvenBtnFunc()

    void OddBtnFunc()   //Ȧ�� ��ư ������ �� ���� �Լ�
    {
        //Debug.Log("Ȧ�� ��ư�� �������.");
        //m_Result_Text.text = "Ȧ�� ��ư�� �������.";

        if (m_Money <= 0)
            return;     //<-- ��� �Լ��� ���� ������ ��ɾ�

        // ������ 0.¦�� ����  1. Ȧ�� ���� ���·� ������ ����
        int a_UserSel = 1;
        int a_DiceNum = Random.Range(1, 7); //1 ~ 6 ������ �߻�

        string a_StrCom = "¦��";

        if ((a_DiceNum % 2) == 1)
            a_StrCom = "Ȧ��";

        // ����
        if (a_UserSel == (a_DiceNum % 2)) //������
        {
            m_Result_Text.text = "�ֻ��� ���� (" + a_DiceNum + ")(" +
                a_StrCom + ") ���߼̽��ϴ�.";

            m_WinCount++;
            m_Money += (m_Gameble * 2);  //100;
        }
        else //Ʋ�����
        {
            m_Result_Text.text = "�ֻ��� ���� (" + a_DiceNum + ")(" +
                a_StrCom + ") Ʋ���̽��ϴ�.";

            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //Ʋ�����
        // ����

        // ���� ���� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                            " : ��(" + m_WinCount + ")" +
                            " : ��(" + m_LostCount + ")";
        // ���� ���� UI ����

    }//void OddBtnFunc()

}//public class Game_Mgr
