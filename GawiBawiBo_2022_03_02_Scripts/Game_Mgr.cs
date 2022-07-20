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

    int m_Money = 1000; //유저의 보유 금액
    int m_WinCount = 0; //승리 카운트
    int m_LostCount = 0; //패배 카운트

    [Header("--- Gamble ---")]
    public Slider m_Gamble_Slider;
    public Text m_Gamble_Text;
    int m_Gamble = 100;   //건 금액 저장 변수

    // Start is called before the first frame update
    void Start()
    {
        if (m_Gawii_Btn != null)
            m_Gawii_Btn.onClick.AddListener(GawiiBtnFunc);
        //함수 포인터 대기 방식

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

        m_Gamble_Text.text = "금액걸기 : " + m_Gamble;
        //m_Gamble_Text.text = m_Gamble_Slider.value.ToString();
    }

    void GawiiBtnFunc()  //가위 버튼 눌렸을 때 반응 함수
    {
        if (m_Money <= 0)
            return; //<-- 즉시 함수를 빠져 나가는 명령어

        //Debug.Log("가위 버튼을 눌렀어요.");
        m_Result_Text.text = "가위 버튼을 눌렀어요.";

        // 유저가 1. 가위 선택 2. 바위 선택 3. 보 선택 상태로 생각할 것임
        int a_UserSel = 1;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 랜덤값 발생

        // 판정
        if (a_UserSel == a_StrCom)
        { //무승부
            m_Result_Text.text = "User(가위): Com(가위) 비기셨습니다.";
        }

        if (a_StrCom == 3)
        { //승리
            m_Result_Text.text = "User(가위): Com(보) 이기셨습니다.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //패배
        {
            m_Result_Text.text = "User(가위) : Com(바위) 졌습니다.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //패배
        //판정

        //유저 정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                             " : 승(" + m_WinCount + ")" +
                             " : 패(" + m_LostCount + ")";
        //유저 정보 UI 갱신
    }//void GawiiBtnFunc()

    void BawiiBtnFunc()  //바위 버튼 눌렸을 때 반응 함수
    {
        if (m_Money <= 0)
            return; //<-- 즉시 함수를 빠져 나가는 명령어

        //Debug.Log("바위 버튼을 눌렀어요.");
        m_Result_Text.text = "바위 버튼을 눌렀어요.";

        // 유저가 1. 가위 선택 2. 바위 선택 3. 보 선택 상태로 생각할 것임
        int a_UserSel = 2;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 랜덤값 발생

        // 판정
        if (a_UserSel == a_StrCom)
        { //무승부
            m_Result_Text.text = "User(바위): Com(바위) 비기셨습니다.";
        }

        if (a_StrCom == 1)
        { //승리
            m_Result_Text.text = "User(바위): Com(가위) 이기셨습니다.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //패배
        {
            m_Result_Text.text = "User(바위) : Com(보) 졌습니다.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //패배
        //판정

        //유저 정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                             " : 승(" + m_WinCount + ")" +
                             " : 패(" + m_LostCount + ")";
        //유저 정보 UI 갱신
    }//void GawiiBtnFunc()

    void BoBtnFunc()  //보 버튼 눌렸을 때 반응 함수
    {
        if (m_Money <= 0)
            return; //<-- 즉시 함수를 빠져 나가는 명령어

        //Debug.Log("바위 버튼을 눌렀어요.");
        m_Result_Text.text = "보 버튼을 눌렀어요.";

        // 유저가 1. 가위 선택 2. 바위 선택 3. 보 선택 상태로 생각할 것임
        int a_UserSel = 3;
        int a_StrCom = Random.Range(1, 4); //1 ~ 3 랜덤값 발생

        // 판정
        if (a_UserSel == a_StrCom)
        { //무승부
            m_Result_Text.text = "User(보): Com(보) 비기셨습니다.";
        }

        if (a_StrCom == 2)
        { //승리
            m_Result_Text.text = "User(보): Com(바위) 이기셨습니다.";

            m_WinCount++;
            m_Money += (m_Gamble * 2); // 100;
        }

        else //패배
        {
            m_Result_Text.text = "User(보) : Com(가위) 졌습니다.";

            m_LostCount++;
            m_Money -= m_Gamble; // 200;

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }

        }//else //패배
        //판정

        //유저 정보 UI 갱신
        m_UserInfo_Text.text = "유저의 보유금액 : " + m_Money +
                             " : 승(" + m_WinCount + ")" +
                             " : 패(" + m_LostCount + ")";
        //유저 정보 UI 갱신
    }//void GawiiBtnFunc()
}
