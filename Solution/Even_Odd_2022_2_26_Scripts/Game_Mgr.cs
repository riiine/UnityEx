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

 
    int m_Money = 1000;  //유저의 보유 금액
    int m_WinCount = 0;  //승리 카운트   
    int m_LostCount = 0; //패배 카운트

    [Header("--- Gamble ---")]
    public Slider m_Gameble_Slider;
    public Text m_Gameble_Text;
    int m_Gameble = 100;  //건 금액 저장 변수

    // Start is called before the first frame update
    void Start()
    {
        if (m_Even_Btn != null)
            m_Even_Btn.onClick.AddListener(EvenBtnFunc);
        //함수 포인터 대기 방식

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

        m_Gameble_Text.text = "금액걸기 : " + m_Gameble;
    }

    void EvenBtnFunc()  //짝수 버튼 눌렸을 때 반응 함수
    {
        if (m_Money <= 0)
            return;     //<-- 즉시 함수를 빠려 나가는 명령어
             
        //Debug.Log("짝수 버튼을 눌렀어요.");

        // 유저가 0.짝수 선택  1. 홀수 선택 상태로 생각할 것임
        int a_UserSel = 0;
        int a_DiceNum = Random.Range(1, 7); //1 ~ 6 랜덤값 발생

        string a_StrCom = "짝수";

        if ((a_DiceNum % 2) == 1)
            a_StrCom = "홀수";

        // 판정
        if(a_UserSel == (a_DiceNum % 2)) //맞춘경우
        {
            m_Result_Text.text = "주사위 값은 (" + a_DiceNum + ")(" +
                a_StrCom + ") 맞추셨습니다.";

            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;
        }
        else //틀린경우
        {
            m_Result_Text.text = "주사위 값은 (" + a_DiceNum + ")(" +
                a_StrCom + ") 틀리셨습니다.";

            m_LostCount++;
            m_Money -= m_Gameble;   //200;

            if(m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //틀린경우
        // 판정

        // 유저 정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                            " : 승(" + m_WinCount + ")" +
                            " : 패(" + m_LostCount + ")";
        // 유저 정보 UI 갱신
    }//void EvenBtnFunc()

    void OddBtnFunc()   //홀수 버튼 눌렸을 때 반응 함수
    {
        //Debug.Log("홀수 버튼을 눌렀어요.");
        //m_Result_Text.text = "홀수 버튼을 눌렀어요.";

        if (m_Money <= 0)
            return;     //<-- 즉시 함수를 빠려 나가는 명령어

        // 유저가 0.짝수 선택  1. 홀수 선택 상태로 생각할 것임
        int a_UserSel = 1;
        int a_DiceNum = Random.Range(1, 7); //1 ~ 6 랜덤값 발생

        string a_StrCom = "짝수";

        if ((a_DiceNum % 2) == 1)
            a_StrCom = "홀수";

        // 판정
        if (a_UserSel == (a_DiceNum % 2)) //맞춘경우
        {
            m_Result_Text.text = "주사위 값은 (" + a_DiceNum + ")(" +
                a_StrCom + ") 맞추셨습니다.";

            m_WinCount++;
            m_Money += (m_Gameble * 2);  //100;
        }
        else //틀린경우
        {
            m_Result_Text.text = "주사위 값은 (" + a_DiceNum + ")(" +
                a_StrCom + ") 틀리셨습니다.";

            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //틀린경우
        // 판정

        // 유저 정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                            " : 승(" + m_WinCount + ")" +
                            " : 패(" + m_LostCount + ")";
        // 유저 정보 UI 갱신

    }//void OddBtnFunc()

}//public class Game_Mgr
