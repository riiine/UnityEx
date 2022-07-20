using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    public Button m_Gawi_Btn;   //������ư
    public Button m_Bawi_Btn;   //������ư
    public Button m_Bo_Btn;     //����ư

    public Text m_UserInfo_Text; //���� ���� �ؽ�Ʈ
    public Text m_Result_Text;   //��� �ؽ�Ʈ

    int m_Money = 1000;   //������ ���� �ݾ�
    int m_WinCount = 0;   //�¸� Ƚ��
    int m_LostCount = 0;  //�й� Ƚ��

    [Header("--- Gameble ---")]
    public Text m_Gameble_Text;
    public Slider m_Gameble_Slider;
    int m_Gameble = 100;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Gawi_Btn != null)
            m_Gawi_Btn.onClick.AddListener(GawiBtnFunc);

        if (m_Bawi_Btn != null)
            m_Bawi_Btn.onClick.AddListener(BawiBtnFunc);

        if (m_Bo_Btn != null)
            m_Bo_Btn.onClick.AddListener(BoBtnFunc);
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

    void GawiBtnFunc()
    {
        //m_Result_Text.text = "��������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 1;  //1 �̸� ������ �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "����";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if(a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if(a_UserSel == 1 && a_ComSel == 3) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble; //200;
            if(m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����
    } //void GawiBtnFunc()

    void BawiBtnFunc()
    {
        //m_Result_Text.text = "��������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 2;  //2 �̸� ������ �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "����";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if (a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if (a_UserSel == 2 && a_ComSel == 1) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;
            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����
    }

    void BoBtnFunc()
    {
        //m_Result_Text.text = "������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 3;  //3 �̸� ���� �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "��";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if (a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if (a_UserSel == 3 && a_ComSel == 2) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2);  //100;
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;
            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����
    }
}
