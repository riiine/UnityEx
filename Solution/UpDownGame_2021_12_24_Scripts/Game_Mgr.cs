using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    public Button m_Equal_Btn;      //일치한다 버튼
    public Button m_Small_Btn;      //~보다 작다 버튼
    public Button m_Big_Btn;        //~보다 크다 버튼
    public Button m_Replay_Btn;     //게임다시하기 버튼

    public Text m_UserInfo_Text;
    public Text m_ComQuestion;
    public Text m_Result_Text;

    int m_Count = 0;            //진행 횟수
    int m_CurNum = 0;           //마지막으로 나온 랜덤값을 위한 변수 (질문 값)
    int m_Min = 1;              //최소값
    int m_Max = 101;            //최대값
    bool m_IsGamOver = false;   //게임 종료 여부 변수

    // Start is called before the first frame update
    void Start()
    {
        m_CurNum = Random.Range(m_Min, m_Max);
        m_ComQuestion.text = "당신이 생각한 숫자는 " + m_CurNum + "입니까?";

        if (m_Equal_Btn != null)
            m_Equal_Btn.onClick.AddListener(EqualBtnFuc);  //델리게이트 함수 대기 방식

        if (m_Small_Btn != null)
            m_Small_Btn.onClick.AddListener(SmallBtnFuc);

        if (m_Big_Btn != null)
            m_Big_Btn.onClick.AddListener(BigBtnFuc);

        if (m_Replay_Btn != null)
            m_Replay_Btn.onClick.AddListener(ReplayBtnFuc);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    void EqualBtnFuc() //같아요 버튼 눌렀을 때 호출되는 함수
    {
        if (m_IsGamOver == true)
            return;

        m_Result_Text.text = "당신이 생각한 숫자는 " + m_CurNum + "입니다.";
        m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
        m_IsGamOver = true;
    }

    void SmallBtnFuc() //작아요 버튼 눌렀을 때 호출되는 함수
    {
        if (m_IsGamOver == true)
            return;

        m_Max = m_CurNum;
        m_Count++;

        if ((m_Max - m_Min) <= 1)
        {
            m_Result_Text.text = "당신이 생각한 숫자는 " + m_Min + "입니다.";
            m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
            m_IsGamOver = true;
            return;
        }

        m_CurNum = Random.Range(m_Min, m_Max);
        m_ComQuestion.text = "당신이 생각한 숫자는 " + m_CurNum + "입니까?";
        m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
    }

    void BigBtnFuc()   //커요 버튼 눌렀을 때 호출되는 함수
    {
        if (m_IsGamOver == true)
            return;

        if (100 <= m_CurNum) //예외처리
        {
            m_Result_Text.text = "당신이 생각한 숫자는 " + "100" + "입니다.";
            m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
            m_IsGamOver = true;
            return;
        }

        m_Min = m_CurNum + 1;
        m_Count++;

        if ((m_Max - m_Min) <= 1)
        {
            m_Result_Text.text = "당신이 생각한 숫자는 " + m_Min + "입니다.";
            m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
            m_IsGamOver = true;
            return;
        }

        m_CurNum = Random.Range(m_Min, m_Max);
        m_ComQuestion.text = "당신이 생각한 숫자는 " + m_CurNum + "입니까?";
        m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
    }

    void ReplayBtnFuc()  //게임다시하기 버튼 함수
    {
        m_IsGamOver = false;
        m_Count = 0;
        m_Min = 1;
        m_Max = 101;
        m_CurNum = Random.Range(m_Min, m_Max);
        m_ComQuestion.text = "당신이 생각한 숫자는 " + m_CurNum + "입니까?";
        m_UserInfo_Text.text = "진행 횟수 : 20번 중 " + m_Count + "번";
        m_Result_Text.text = "게임결과";
    }
}
