using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_Mgr : MonoBehaviour
{
    public Button m_Same_Btn;
    public Button m_Small_Btn;
    public Button m_Big_Btn;
    public Button m_Restart_Btn;

    public Text m_UserInfo_Text;
    public Text m_Result_Text;
    public Text m_Menu_Text;

    

    int m_Count = 0; //���ӹݺ�Ƚ��
    int m_Strcom; // ��������

    //1���� 100 ������ ����
    int m_Start = 1;
    int m_End = 100;

    // Start is called before the first frame update
    void Start()
    {
        m_Strcom = Random.Range(m_Start, m_End); //1 ~ 100 ������ �߻�

        m_Menu_Text.text = "����� ������ ���ڴ� " + m_Strcom + "�Դϱ�?";


        if (m_Same_Btn != null)
            m_Same_Btn.onClick.AddListener(SameBtnFunc);
        //�Լ� ������ ��� ���

        if (m_Small_Btn != null)
            m_Small_Btn.onClick.AddListener(SmallBtnFunc);

        if (m_Big_Btn != null)
            m_Big_Btn.onClick.AddListener(BigBtnFunc);

        if (m_Restart_Btn != null)
            m_Restart_Btn.onClick.AddListener(RestartBtnFunc);

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SameBtnFunc()  //�¾ƿ� ��ư ������ �� ���� �Լ�
    {

        m_Count++;

        m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count +"��";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "���� Ƚ���� �ʰ��Ǿ����ϴ�.";
            m_Result_Text.text = "Game Over";
            
            //������ ����Ǹ� ��ư�� ������
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Result_Text.text = "����� ������ ���ڴ� " + m_Strcom + "�Դϴ�.";

        //������ ����Ǹ� ��ư�� ������
        m_Same_Btn.interactable = false;
        m_Small_Btn.interactable = false;
        m_Big_Btn.interactable = false;
    }

    void SmallBtnFunc()  //�۾ƿ� ��ư ������ �� ���� �Լ�
    {

        m_Count++;

        m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count + "��";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "���� Ƚ���� �ʰ��Ǿ����ϴ�.";
            m_Result_Text.text = "Game Over";

            //������ ����Ǹ� ��ư�� ������
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_End = m_Strcom;

        m_Strcom = Random.Range(m_Start, m_End);

        m_Menu_Text.text = "����� ������ ���ڴ� " + m_Strcom + "�Դϱ�?";

        if ( (m_End - m_Start) == 2 )
        {
            m_Result_Text.text = "����� ������ ���ڴ� " + (m_Start + 1) + "�Դϴ�.";
            m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count + "��";

            //������ ����Ǹ� ��ư�� ������
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

    }

    void BigBtnFunc()  //Ŀ�� ��ư ������ �� ���� �Լ�
    {

        m_Count++;

        m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count + "��";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "���� Ƚ���� �ʰ��Ǿ����ϴ�.";
            m_Result_Text.text = "Game Over";

            //������ ����Ǹ� ��ư�� ������
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Start = m_Strcom;

        m_Strcom = Random.Range(m_Start, m_End);

        if ((m_End - m_Start) == 2)
        {
            m_Result_Text.text = "����� ������ ���ڴ� " + (m_Start + 1) + "�Դϴ�.";
            m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count + "��";

            //������ ����Ǹ� ��ư�� ������
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Menu_Text.text = "����� ������ ���ڴ� " + m_Strcom + "�Դϱ�?";

        

    }

    void RestartBtnFunc()  //���� �ٽ��ϱ� ��ư ������ �� ���� �Լ�
    {
        //��ư�� �ٽ� ��
        m_Same_Btn.interactable = true;
        m_Small_Btn.interactable = true;
        m_Big_Btn.interactable = true;

        m_Count = 0;

        m_Strcom = Random.Range(1, 101); //1 ~ 100 ������ �߻�

        m_Menu_Text.text = "����� ������ ���ڴ� " + m_Strcom + "�Դϱ�?";

        m_UserInfo_Text.text = "���� Ƚ�� : 20 �� �� " + m_Count + "��";

        m_Result_Text.text = "���Ӱ��";
    }



}
