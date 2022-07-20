using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    public Button m_Gawi_Btn;   //가위버튼
    public Button m_Bawi_Btn;   //바위버튼
    public Button m_Bo_Btn;     //보버튼

    public Text m_UserInfo_Text; //유저 정보 텍스트
    public Text m_Result_Text;   //결과 텍스트

    int m_Money = 1000;   //유저의 보유 금액
    int m_WinCount = 0;   //승리 횟수
    int m_LostCount = 0;  //패배 횟수

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

        m_Gameble_Text.text = "금액걸기 : " + m_Gameble;
    }

    void GawiBtnFunc()
    {
        //m_Result_Text.text = "가위선택";
        if (m_Money <= 0)
            return;     //<-- 함수를 즉시 빠져 나가는 코드

        int a_UserSel = 1;  //1 이면 가위를 의미함
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 랜덤한 값이 나온다. (4는 제외)
        string a_strUser = "가위";

        string a_strCom = "가위";
        if (a_ComSel == 2)
            a_strCom = "바위";
        else if (a_ComSel == 3)
            a_strCom = "보";

        //--- 판정
        if(a_UserSel == a_ComSel) //비긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 비겼습니다.";
        }
        else if(a_UserSel == 1 && a_ComSel == 3) //이긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 승리하셨습니다.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble; //200;
            if(m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- 판정

        //--- 유저정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                                " : 승(" + m_WinCount + ")" +
                                " : 패(" + m_LostCount + ")";
        //--- 유저정보 UI 갱신
    } //void GawiBtnFunc()

    void BawiBtnFunc()
    {
        //m_Result_Text.text = "바위선택";
        if (m_Money <= 0)
            return;     //<-- 함수를 즉시 빠져 나가는 코드

        int a_UserSel = 2;  //2 이면 바위를 의미함
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 랜덤한 값이 나온다. (4는 제외)
        string a_strUser = "바위";

        string a_strCom = "가위";
        if (a_ComSel == 2)
            a_strCom = "바위";
        else if (a_ComSel == 3)
            a_strCom = "보";

        //--- 판정
        if (a_UserSel == a_ComSel) //비긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 비겼습니다.";
        }
        else if (a_UserSel == 2 && a_ComSel == 1) //이긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 승리하셨습니다.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;
            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- 판정

        //--- 유저정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                                " : 승(" + m_WinCount + ")" +
                                " : 패(" + m_LostCount + ")";
        //--- 유저정보 UI 갱신
    }

    void BoBtnFunc()
    {
        //m_Result_Text.text = "보선택";
        if (m_Money <= 0)
            return;     //<-- 함수를 즉시 빠져 나가는 코드

        int a_UserSel = 3;  //3 이면 보를 의미함
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 랜덤한 값이 나온다. (4는 제외)
        string a_strUser = "보";

        string a_strCom = "가위";
        if (a_ComSel == 2)
            a_strCom = "바위";
        else if (a_ComSel == 3)
            a_strCom = "보";

        //--- 판정
        if (a_UserSel == a_ComSel) //비긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 비겼습니다.";
        }
        else if (a_UserSel == 3 && a_ComSel == 2) //이긴 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " 승리하셨습니다.";
            m_WinCount++;
            m_Money += (m_Gameble * 2);  //100;
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;
            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- 판정

        //--- 유저정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                                " : 승(" + m_WinCount + ")" +
                                " : 패(" + m_LostCount + ")";
        //--- 유저정보 UI 갱신
    }
}
