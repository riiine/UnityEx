using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMgr : MonoBehaviour
{

    //스크린의 월드 좌표
    public static Vector3 m_SceenWMin = new Vector3(-10.0f, -5.0f, 0.0f);
    public static Vector3 m_SceenWMax = new Vector3(10.0f, 5.0f, 0.0f);
    //스크린의 월드 좌표

    [Header("-------- Info UI --------")]
    public Text m_ScoreTxt = null;
    public Text m_CurScoreTxt = null;
    public Text m_UserGoldTxt = null;
    public Text UserInfo_Text = null;

    int m_CurScore = 0;      //이번 스테이지에 얻은 게임점수
    int m_CurGold = 0;       //이번 스테이지에 얻은 골드값

    //----------------- 머리위에 데미지 띄우기용 변수 선언
    GameObject a_DamClone;
    DamageTxt  a_DamageTx;
    Vector3    a_StCacPos;
    [Header("-------- DamageText --------")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamageObj = null;
    //----------------- 머리위에 데미지 띄우기용 변수 선언

    //---------------------------- 환경설정 Dlg 관련 변수
    [Header("-------- ConfigBox --------")]
    public Button m_CfgBtn = null;
    public GameObject Canvas_Dialog = null;
    public GameObject m_ConfigBoxObj = null;

    [Header("-------- GameOver --------")]
    public GameObject ResultPanel = null;
    public Text   Result_Txt = null;
    public Button Replay_Btn = null;
    public Button RstLobby_Btn = null;

    [Header("-------- GoLobby --------")]
    public Button GoLobby_Btn = null;

    //---------------------------- ScrollView OnOff
    [Header("-------- ScrollView OnOff --------")]
    public Button m_InVen_Btn = null;
    public Transform m_InVenScrollTr = null;
    private bool m_InVen_ScOnOff = false;
    private float m_ScSpeed = 9000.0f;
    private Vector3 m_ScOnPos = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 m_ScOffPos = new Vector3(-1000.0f, 0.0f, 0.0f);
    private Vector3 m_BtnOnPos = new Vector3(410.0f, -247.8f, 0.0f);
    private Vector3 m_BtnOffPos = new Vector3(-569.6f, -247.8f, 0.0f);

    public Transform m_ScContent;
    public GameObject m_CrSmallPrefab;
    //---------------------------- ScrollView OnOff

    public static GameObject m_CoinItem = null;

    public static InGameMgr Inst = null;

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f; //원래 속도로...

        GlobalValue.LoadData();

        RenewMyCharList();

        //------스크린의 월드 좌표 구하기
        Vector3 a_ScMin = new Vector3(0.0f, 0.0f, 0.0f);
        //ScreenViewPort 좌측하단
        m_SceenWMin = Camera.main.ViewportToWorldPoint(a_ScMin);
        //카메라 화면 좌측하단 코너의 월드 좌표

        Vector3 a_ScMax = new Vector3(1.0f, 1.0f, 0.0f); 
        //ScreenViewPort 우측상단
        m_SceenWMax = Camera.main.ViewportToWorldPoint(a_ScMax);
        //카메라 화면 우측상단 코너의 월드 좌표
        //------스크린의 월드 좌표 구하기

        //--- Refrash Info 
        m_ScoreTxt.text = "최고점수(" + GlobalValue.g_BestScore + ")";
        m_UserGoldTxt.text = "보유골드(" + GlobalValue.g_UserGold + ")";

        UserInfo_Text.text = "내정보 : 별명(" + GlobalValue.g_NickName + ")";
        //--- Refrash Info 

        m_CoinItem = Resources.Load("CoinItemPrefab") as GameObject;

        //---------------------------- 환경설정 Dlg 관련 구현 부분
        if (m_CfgBtn != null)
            m_CfgBtn.onClick.AddListener(() =>
            {   //닉네임 변경 요청 버튼
                if (m_ConfigBoxObj == null)
                    m_ConfigBoxObj = Resources.Load("ConfigBox") as GameObject;

                GameObject a_CfgBoxObj = (GameObject)Instantiate(m_ConfigBoxObj);
                a_CfgBoxObj.transform.SetParent(Canvas_Dialog.transform, false); 
                //false 로 해야 로컬 프리팹에 설정된 좌표를 유지한체 차일드로 붙게된다. 
                Time.timeScale = 0.0f;
            });
        //---------------------------- 환경설정 Dlg 관련 구현 부분

        if (Replay_Btn != null)
            Replay_Btn.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
            });

        if (RstLobby_Btn != null)
            RstLobby_Btn.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
            });

        if (GoLobby_Btn != null)
            GoLobby_Btn.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");   
            });

        if (m_InVen_Btn != null)
        {
            m_InVen_Btn.onClick.AddListener(() =>
            {
                m_InVen_ScOnOff = !m_InVen_ScOnOff;
            });
        }//if(m_InVen_Btn != null)

    }//void Start()

    // Update is called once per frame
    void Update()
    {
        ScrollViewOnOff();

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Keypad1))
        {
            UseSkill_Key(CharType.Char_0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Keypad2))
        {
            UseSkill_Key(CharType.Char_1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Keypad3))
        {
            UseSkill_Key(CharType.Char_2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Keypad4))
        {
            UseSkill_Key(CharType.Char_3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Keypad5))
        {
            UseSkill_Key(CharType.Char_4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Keypad6))
        {
            UseSkill_Key(CharType.Char_5);
        }
    }

    public void DamageTxt(float a_Value, Transform txtTr, Color a_Color)
    {
        if (m_DamageObj == null || m_HUD_Canvas == null)
            return;

        a_DamClone = (GameObject)Instantiate(m_DamageObj);
        a_DamClone.transform.SetParent(m_HUD_Canvas);
        a_DamageTx = a_DamClone.GetComponent<DamageTxt>();
        if (a_DamageTx != null)
            a_DamageTx.InitDamage(a_Value, a_Color);
        a_StCacPos = new Vector3(txtTr.position.x, 
                                 txtTr.position.y + 1.15f, 0.0f);
        a_DamClone.transform.position = a_StCacPos;
    }

    public void AddScore(int Value = 10)
    {
        m_CurScore += Value;
        if (m_CurScore < 0)
            m_CurScore = 0;
        //if (int.MaxValue < m_CurScore) //int.MaxValue == 2147483647
        //    m_CurScore = int.MaxValue;
        if (999999999 < m_CurScore)
            m_CurScore = 999999999;
        m_CurScoreTxt.text = "현재점수(" + m_CurScore + ")";
        if (GlobalValue.g_BestScore < m_CurScore)
        {
            GlobalValue.g_BestScore = m_CurScore;
            PlayerPrefs.SetInt("BestScore", GlobalValue.g_BestScore);
            m_ScoreTxt.text = "최고점수(" + GlobalValue.g_BestScore + ")";
        }
    }//public void AddScore(int Value = 10)

    public void AddGold(int Value = 10)
    {
        GlobalValue.g_UserGold += Value;
        if (GlobalValue.g_UserGold < 0)
            GlobalValue.g_UserGold = 0;
        m_CurGold += Value;
        if (m_CurGold < 0)
            m_CurGold = 0;
        PlayerPrefs.SetInt("UserGold", GlobalValue.g_UserGold);
        m_UserGoldTxt.text = "보유골드(" + GlobalValue.g_UserGold + ")";
    }

    public void ItemDrop(Vector3 a_Pos)
    {
        //---- 보상으로 아이템 드롭 
        int dice = Random.Range(0, 10);     //0 ~ 9 랜덤값 발생
        if (dice < 3)  //30% 확률
        if (m_CoinItem != null)
        {
            GameObject a_CoinObj = (GameObject)Instantiate(m_CoinItem);
            a_CoinObj.transform.position = a_Pos;
            Destroy(a_CoinObj, 10.0f);  //10초뒤에 파괴 설정 (10초내에 먹어야 한다.)
        }
        //---- 보상으로 아이템 드롭 
    }

    public void NickUIUpdate(string a_NickStr)
    {
        GlobalValue.g_NickName = a_NickStr;
        PlayerPrefs.SetString("NickName", a_NickStr);
        if (UserInfo_Text != null)
            UserInfo_Text.text = "내정보 : 별명(" + GlobalValue.g_NickName + ")";
    }

    public void Reset_CurInfo()
    {
        m_CurScore = 0;      //이번 스테이지에 얻은 게임점수
        m_CurGold = 0;       //이번 스테이지에 얻은 골드값
    }

    public void Refresh_UI()
    {
        //--- Refrash Info 
        m_ScoreTxt.text    = "최고점수(" + GlobalValue.g_BestScore + ")";
        m_CurScoreTxt.text = "현재점수(" + m_CurScore + ")";
        m_UserGoldTxt.text = "보유골드(" + GlobalValue.g_UserGold + ")";
        UserInfo_Text.text = "내정보 : 별명(" + GlobalValue.g_NickName + ")";

        //GlobalValue.g_Exp   = 0;
        //GlobalValue.g_Level = 0;
        //--- Refrash Info 
    }

    public void GameOverFunc()
    {
        ResultPanel.SetActive(true);

        Result_Txt.text = "NickName\n" + GlobalValue.g_NickName + "\n\n" +
                    "획득 점수\n" + m_CurScore + "\n\n" + "획득 골드\n" + m_CurGold;
    }

    void ScrollViewOnOff()
    {
        if (m_InVenScrollTr == null)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_InVen_ScOnOff = !m_InVen_ScOnOff;
        }

        if (m_InVen_ScOnOff == false)
        {
            if (m_InVenScrollTr.localPosition.x > m_ScOffPos.x)
            {
                m_InVenScrollTr.localPosition =
                            Vector3.MoveTowards(m_InVenScrollTr.localPosition,
                                      m_ScOffPos, m_ScSpeed * Time.deltaTime);
            }

            if (m_InVen_Btn.transform.localPosition.x > m_BtnOffPos.x)
            {
                m_InVen_Btn.transform.localPosition =
                        Vector3.MoveTowards(m_InVen_Btn.transform.localPosition,
                                        m_BtnOffPos, m_ScSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (m_ScOnPos.x > m_InVenScrollTr.localPosition.x)
            {
                m_InVenScrollTr.localPosition =
                        Vector3.MoveTowards(m_InVenScrollTr.localPosition,
                                          m_ScOnPos, m_ScSpeed * Time.deltaTime);
            }

            if (m_BtnOnPos.x > m_InVen_Btn.transform.localPosition.x)
            {
                m_InVen_Btn.transform.localPosition =
                        Vector3.MoveTowards(m_InVen_Btn.transform.localPosition,
                                        m_BtnOnPos, m_ScSpeed * Time.deltaTime);
            }
        }
        //------------- Menu Scroll 연출
    }

    void RenewMyCharList()
    {
        for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
        {
            GlobalValue.m_CrDataList[ii].m_CurSkillCount =
                            GlobalValue.m_CrDataList[ii].m_Level;
            //시작(로그인)할 때 초기화 해 주고 시작한다.

            if (GlobalValue.m_CrDataList[ii].m_Level <= 0)
            {
                break;
            }

            GameObject a_CharClone = Instantiate(m_CrSmallPrefab);
            a_CharClone.GetComponent<CrSmallNode>().InitState(
                                        GlobalValue.m_CrDataList[ii]);
            a_CharClone.transform.SetParent(m_ScContent, false);
        }//for(int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    }//void RenewMyCharList()

    void UseSkill_Key(CharType a_CrType)
    {
        if (GlobalValue.m_CrDataList[(int)a_CrType].m_CurSkillCount <= 0)
        {  //스킬 소진으로 사용할 수 없음
            return;
        }

        HeroCtrl a_Hero = GameObject.FindObjectOfType<HeroCtrl>();
        if (a_Hero != null)
            a_Hero.UseItem(a_CrType);
  
        if (m_ScContent == null)
            return;

        //--- 아이템 사용 에 대한 UI 갱신 코드
        CrSmallNode[] m_CrSmallList = 
                    m_ScContent.GetComponentsInChildren<CrSmallNode>();
        for (int ii = 0; ii < m_CrSmallList.Length; ii++)
        {
            if (m_CrSmallList[ii].m_CrType == a_CrType)
            {
                m_CrSmallList[ii].Refresh_UI(a_CrType);
                break;
            }
        }
        //--- 아이템 사용 에 대한 UI 갱신 코드
    }//void UseSkill_Key(CharType a_CrType)
}
