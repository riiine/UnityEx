using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game_Mgr : MonoBehaviour
{
    public Button m_Gawii_Btn;
    public Button m_Bawii_Btn;
    public Button m_Bo_Btn;

    public Text m_UserInfo_Text;
    public Text m_Result_Text;

    int m_Money = 1000; //������ ���� �ݾ�
    int m_WinCount = 0; //�¸� ī��Ʈ
    int m_LostCount = 0; //�й� ī��Ʈ

    [Header("--- Gamble ---")]
    public Slider m_Gamble_Slider;
    public Text m_Gamble_Text;
    int m_Gamble = 100;   //�� �ݾ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        if (m_Gawii_Btn != null)
            m_Gawii_Btn.onClick.AddListener(GawiiBtnFunc);
        //�Լ� ������ ��� ���

        if (m_Bawii_Btn != null)
            m_Bawii_Btn.onClick.AddListener(BawiiBtnFunc);

        if (m_Bo_Btn != null)
            m_Bo_Btn.onClick.AddListener(BoBtnFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (1.0f <= m_Gamble_Slider.value || m_Money < 100)
            m_Gamble = m_Money;
        else
            m_Gamble = 100 + (int)(m_Gamble_Slider.value * (m_Money - 100));

        m_Gamble_Text.text = "�ݾװɱ� : " + m_Gamble;
        //m_Gamble_Text.text = m_Gamble_Slider.value.ToString();
    }

    void GawiiBtnFunc()  //���� ��ư ������ �� ���� �Լ�
    {
        if (m_Money <= 0)
            return; //<-- ��� �Լ��� ���� ������ ��ɾ�

        //Debug.Log("���� ��ư�� �������.");
        m_Result_Text.text = "���� ��ư�� �������.";

        // ������ 1. ���� ���� 2. ���� ���� 3. �� ���� ���·� ������ ����
        int a_UserSel = 1;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 ������ �߻�

        // ����
        if (a_UserSel == a_StrCom)
        { //���º�
            m_Result_Text.text = "User(����): Com(����) ���̽��ϴ�.";
        }

        if (a_StrCom == 3)
        { //�¸�
            m_Result_Text.text = "User(����): Com(��) �̱�̽��ϴ�.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //�й�
        {
            m_Result_Text.text = "User(����) : Com(����) �����ϴ�.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //�й�
        //����

        //���� ���� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                             " : ��(" + m_WinCount + ")" +
                             " : ��(" + m_LostCount + ")";
        //���� ���� UI ����
    }//void GawiiBtnFunc()

    void BawiiBtnFunc()  //���� ��ư ������ �� ���� �Լ�
    {
        if (m_Money <= 0)
            return; //<-- ��� �Լ��� ���� ������ ��ɾ�

        //Debug.Log("���� ��ư�� �������.");
        m_Result_Text.text = "���� ��ư�� �������.";

        // ������ 1. ���� ���� 2. ���� ���� 3. �� ���� ���·� ������ ����
        int a_UserSel = 2;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 ������ �߻�

        // ����
        if (a_UserSel == a_StrCom)
        { //���º�
            m_Result_Text.text = "User(����): Com(����) ���̽��ϴ�.";
        }

        if (a_StrCom == 1)
        { //�¸�
            m_Result_Text.text = "User(����): Com(����) �̱�̽��ϴ�.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //�й�
        {
            m_Result_Text.text = "User(����) : Com(��) �����ϴ�.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //�й�
        //����

        //���� ���� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                             " : ��(" + m_WinCount + ")" +
                             " : ��(" + m_LostCount + ")";
        //���� ���� UI ����
    }//void GawiiBtnFunc()

    void BoBtnFunc()  //�� ��ư ������ �� ���� �Լ�
    {
        if (m_Money <= 0)
            return; //<-- ��� �Լ��� ���� ������ ��ɾ�

        //Debug.Log("���� ��ư�� �������.");
        m_Result_Text.text = "�� ��ư�� �������.";

        // ������ 1. ���� ���� 2. ���� ���� 3. �� ���� ���·� ������ ����
        int a_UserSel = 3;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 ������ �߻�

        // ����
        if (a_UserSel == a_StrCom)
        { //���º�
            m_Result_Text.text = "User(��): Com(��) ���̽��ϴ�.";
        }

        if (a_StrCom == 2)
        { //�¸�
            m_Result_Text.text = "User(��): Com(����) �̱�̽��ϴ�.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //�й�
        {
            m_Result_Text.text = "User(��) : Com(����) �����ϴ�.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //�й�
        //����

        //���� ���� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                             " : ��(" + m_WinCount + ")" +
                             " : ��(" + m_LostCount + ")";
        //���� ���� UI ����
    }//void GawiiBtnFunc()
}
