using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState //정수값 대신에 사용되는 별칭
{
    UserPlay_Ing = 0,  //유저가 선택을 고민하는 상태
    Result_Ing = 1,    //결과 보여주기 상태
    GameEnd = 2        //게임 종료 상태
}

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

    [Header("--- Direction ---")] //연출
    public Sprite[] m_IconImg;
    public Image m_UesrGBB_Img;
    public Image m_ComGBB_Img;

    public Text m_ShowResult;
    float m_ResultDelay = 0.0f;

    [Header("--- Auto Button ---")]
    public Toggle m_AutoToggle = null;
    public Button m_NextStartBtn = null; //다시한판

    [Header("-------- DamageText --------")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamagePrefab = null;
    public Transform m_SpawnTxtPos = null;

    GameState m_GameState = GameState.UserPlay_Ing;
    float m_DirecCount = 0.0f;   //컴퓨터 애니메이션 연출용 변수

    // Start is called before the first frame update
    void Start()
    {
        if (m_Gawi_Btn != null)
            m_Gawi_Btn.onClick.AddListener(GawiBtnFunc);

        if (m_Bawi_Btn != null)
            m_Bawi_Btn.onClick.AddListener(BawiBtnFunc);

        if (m_Bo_Btn != null)
            m_Bo_Btn.onClick.AddListener(BoBtnFunc);

        if (m_AutoToggle != null)
            m_AutoToggle.onValueChanged.AddListener(AutoToggleFunc);
        //onValueChanged 대리자함수(SoundOnOff) 등록

        if (m_NextStartBtn != null)
            m_NextStartBtn.onClick.AddListener(NextStartFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameState == GameState.GameEnd)
        {
            m_Gameble_Text.text = "금액걸기 : 0"; // + 0.ToString();
            return;
        }

        if (1.0f <= m_Gameble_Slider.value || m_Money < 100)
            m_Gameble = m_Money;
        else
            m_Gameble = 100 + (int)(m_Gameble_Slider.value * (m_Money - 100));

        m_Gameble_Text.text = "금액걸기 : " + m_Gameble;

        if (m_GameState == GameState.UserPlay_Ing)
        { //유저가 선택을 고민하는 상태
            m_DirecCount += (Time.deltaTime * 8.0f);
            if (3.0f <= m_DirecCount)
                m_DirecCount = 0.0f;
            ComImgChange((int)m_DirecCount);
        }
        else if (m_GameState == GameState.Result_Ing)
        { //결과를 보여줘야 하는 상태

            //------ ShowResult 끄기 연출
            if (m_AutoToggle.isOn == true) //자동일때
            if (0.0f < m_ResultDelay)
            {
                m_ResultDelay -= Time.deltaTime;
                //Time.deltaTime : 한프레임이 도는데 걸리는 시간 (단위는 초)

                if (m_ResultDelay <= 0.0f) //타임아웃 시점
                {
                    m_GameState = GameState.UserPlay_Ing;
                    m_UesrGBB_Img.enabled = false;
                    m_ShowResult.enabled = false;
                }
            }//if (0.0f < m_ResultDelay)
            //------ ShowResult 끄기 연출

        }//else if (m_GameState == GameState.Result_Ing)



    } //void Update()

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

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                                    new Color32(130, 130, 255, 255));
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble; //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                                    new Color32(255, 130, 130, 255));

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

        Refresh_UI(a_UserSel, a_ComSel);
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

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                        new Color32(130, 130, 255, 255));
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                            new Color32(255, 130, 130, 255));

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

        Refresh_UI(a_UserSel, a_ComSel);
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

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                        new Color32(130, 130, 255, 255));
        }
        else //패배한 경우
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " 패배하셨습니다.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                            new Color32(255, 130, 130, 255));

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

        Refresh_UI(a_UserSel, a_ComSel);
    }

    void Refresh_UI(int a_U_Sel, int a_C_Sel) //연출용 UI 갱신용 함수
    {
        a_U_Sel--;
        a_C_Sel--;

        if (a_U_Sel < 0 || m_IconImg.Length <= a_U_Sel)
            return;

        if (a_C_Sel < 0 || m_IconImg.Length <= a_C_Sel)
            return;

        if (m_UesrGBB_Img != null)
            m_UesrGBB_Img.sprite = m_IconImg[a_U_Sel];

        if (m_ComGBB_Img != null)
            m_ComGBB_Img.sprite = m_IconImg[a_C_Sel];

        m_GameState = GameState.Result_Ing;
        m_UesrGBB_Img.enabled = true;
        m_ShowResult.enabled = true;
        m_ResultDelay = 3.0f;

        //유저의 승리, 패배를 확인 하기 위해서 인덱스 돌려 놓기 
        a_U_Sel++;
        a_C_Sel++;

        if (a_U_Sel == a_C_Sel) //무승부
        {
            m_ShowResult.color = new Color32(90, 90, 90, 255);
            m_ShowResult.text = "무승부";
        }
        else if ( (a_U_Sel == 1 && a_C_Sel == 3) ||
                    (a_U_Sel == 2 && a_C_Sel == 1) ||
                    (a_U_Sel == 3 && a_C_Sel == 2) ) //"승리!!"
        {
            m_ShowResult.color = new Color32(0, 0, 255, 255);
            m_ShowResult.text = "승리!!";
        }
        else   //패배..
        {
            m_ShowResult.color = new Color32(255, 0, 0, 255);
            m_ShowResult.text = "패배..";
        }

        if (m_Money <= 0)
            m_GameState = GameState.GameEnd;

    }//void Refresh_UI(int a_U_Sel, int a_C_Sel) //연출용 UI 갱신용 함수

    void ComImgChange(int a_Sel)
    {
        if (m_ComGBB_Img == null || m_IconImg == null)
            return;

        if (a_Sel < 0 || m_IconImg.Length <= a_Sel)
            return;

        m_ComGBB_Img.sprite = m_IconImg[a_Sel];
    }//void ComImgChange(int a_Sel)

    public void AutoToggleFunc(bool value)
    { //체크 상태가 변경되었을 때 호출되게 할 함수
        Text a_Label = m_AutoToggle.GetComponentInChildren<Text>();

        if (value == true) //On
        {
            if (a_Label != null)
                a_Label.text = "자동";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(false);

            if (m_GameState == GameState.GameEnd)
                return;

            m_ResultDelay = 0.0f; //거의 즉시 실행 되도록...
            m_GameState = GameState.UserPlay_Ing;
            m_UesrGBB_Img.enabled = false;
            m_ShowResult.enabled = false;
        }
        else   //Off
        {
            if (a_Label != null)
                a_Label.text = "수동";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(true);
        }
    }//public void AutoToggleFunc(bool value)

    void NextStartFunc()
    { //다시한판 버튼 클릭시 호출되게 할 함수
        if (m_GameState != GameState.Result_Ing)
            return;

        m_GameState = GameState.UserPlay_Ing;
        m_UesrGBB_Img.enabled = false;
        m_ShowResult.enabled = false;
    }

    public void DamageTxt(float a_Value, Vector3 a_TxtPos, Color a_Color)
    {
        if (m_DamagePrefab == null || m_HUD_Canvas == null)
            return;

        GameObject a_DamClone = (GameObject)Instantiate(m_DamagePrefab);
        a_DamClone.transform.SetParent(m_HUD_Canvas, false);
        a_DamClone.transform.position = a_TxtPos;

        DamageTxt a_DamageTx = a_DamClone.GetComponent<DamageTxt>();
        if (a_DamageTx != null)
            a_DamageTx.InitDamage(a_Value, a_Color);
    }

} //public class Game_Mgr : MonoBehaviour
