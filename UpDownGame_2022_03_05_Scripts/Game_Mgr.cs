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

    

    int m_Count = 0; //게임반복횟수
    int m_Strcom; // 랜덤숫자

    //1부터 100 사이의 범위
    int m_Start = 1;
    int m_End = 100;

    // Start is called before the first frame update
    void Start()
    {
        m_Strcom = Random.Range(m_Start, m_End); //1 ~ 100 랜덤값 발생

        m_Menu_Text.text = "당신이 생각한 숫자는 " + m_Strcom + "입니까?";


        if (m_Same_Btn != null)
            m_Same_Btn.onClick.AddListener(SameBtnFunc);
        //함수 포인터 대기 방식

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

    void SameBtnFunc()  //맞아요 버튼 눌렸을 때 반응 함수
    {

        m_Count++;

        m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count +"번";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "진행 횟수가 초과되었습니다.";
            m_Result_Text.text = "Game Over";
            
            //게임이 종료되면 버튼을 꺼버림
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Result_Text.text = "당신이 생각한 숫자는 " + m_Strcom + "입니다.";

        //게임이 종료되면 버튼을 꺼버림
        m_Same_Btn.interactable = false;
        m_Small_Btn.interactable = false;
        m_Big_Btn.interactable = false;
    }

    void SmallBtnFunc()  //작아요 버튼 눌렸을 때 반응 함수
    {

        m_Count++;

        m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count + "번";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "진행 횟수가 초과되었습니다.";
            m_Result_Text.text = "Game Over";

            //게임이 종료되면 버튼을 꺼버림
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_End = m_Strcom;

        m_Strcom = Random.Range(m_Start, m_End);

        m_Menu_Text.text = "당신이 생각한 숫자는 " + m_Strcom + "입니까?";

        if ( (m_End - m_Start) == 2 )
        {
            m_Result_Text.text = "당신이 생각한 숫자는 " + (m_Start + 1) + "입니다.";
            m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count + "번";

            //게임이 종료되면 버튼을 꺼버림
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

    }

    void BigBtnFunc()  //커요 버튼 눌렸을 때 반응 함수
    {

        m_Count++;

        m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count + "번";

        if (m_Count > 20)
        {
            m_UserInfo_Text.text = "진행 횟수가 초과되었습니다.";
            m_Result_Text.text = "Game Over";

            //게임이 종료되면 버튼을 꺼버림
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Start = m_Strcom;

        m_Strcom = Random.Range(m_Start, m_End);

        if ((m_End - m_Start) == 2)
        {
            m_Result_Text.text = "당신이 생각한 숫자는 " + (m_Start + 1) + "입니다.";
            m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count + "번";

            //게임이 종료되면 버튼을 꺼버림
            m_Same_Btn.interactable = false;
            m_Small_Btn.interactable = false;
            m_Big_Btn.interactable = false;
        }

        m_Menu_Text.text = "당신이 생각한 숫자는 " + m_Strcom + "입니까?";

        

    }

    void RestartBtnFunc()  //게임 다시하기 버튼 눌렸을 때 반응 함수
    {
        //버튼을 다시 켬
        m_Same_Btn.interactable = true;
        m_Small_Btn.interactable = true;
        m_Big_Btn.interactable = true;

        m_Count = 0;

        m_Strcom = Random.Range(1, 101); //1 ~ 100 랜덤값 발생

        m_Menu_Text.text = "당신이 생각한 숫자는 " + m_Strcom + "입니까?";

        m_UserInfo_Text.text = "진행 횟수 : 20 번 중 " + m_Count + "번";

        m_Result_Text.text = "게임결과";
    }



}
