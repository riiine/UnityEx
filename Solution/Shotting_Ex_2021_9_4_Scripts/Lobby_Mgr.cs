using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Mgr : MonoBehaviour
{
    public Text MyInfo_Text = null;

    public Button m_StoreBtn = null;
    public Button m_GameStartBtn = null;
    public Button m_LogOutBtn = null;

    //------ Fade In 관련 변수들...
    [Header("-------- FadeOut --------")]
    public Image m_FadeImg = null;
    private float AniDuring = 0.8f;  //페이드아웃 연출을 시간 설정
    private bool m_StartFade = false;
    private float m_CacTime = 0.0f;
    private float m_AddTimer = 0.0f;
    private Color m_Color;

    private float m_StVal = 1.0f;
    private float m_EndVal = 0.0f;

    string SceneName = "";
    //------ Fade In 관련 변수들...

    // Start is called before the first frame update
    void Start()
    {
        m_StVal = 1.0f;  //불투명
        m_EndVal = 0.0f; //투명
        m_FadeImg.gameObject.SetActive(true);
        m_StartFade = true; //페이트인 연출 시작 요청

        Time.timeScale = 1.0f; //원래 속도로...

        GlobalValue.LoadData();
        
        if (m_StoreBtn != null)
            m_StoreBtn.onClick.AddListener(() =>
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("StoreScene");
                ScFadeOutInit("StoreScene");
            });

        if (m_GameStartBtn != null)
            m_GameStartBtn.onClick.AddListener(() =>
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
                ScFadeOutInit("InGame");
            });

        if (m_LogOutBtn != null)
            m_LogOutBtn.onClick.AddListener(() =>
            {
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
                ScFadeOutInit("Title");
            });

        RefreshMyInfo();
    }

    // Update is called once per frame
    void Update()
    {
        //-----m_FadeInOut
        if (m_StartFade == true)
        {
            if (m_CacTime < 1.0f)
            {
                m_AddTimer = m_AddTimer + Time.deltaTime;
                m_CacTime = m_AddTimer / AniDuring;
                m_Color = m_FadeImg.color;
                m_Color.a = Mathf.Lerp(m_StVal, m_EndVal, m_CacTime);
                m_FadeImg.color = m_Color;
                if (1.0f <= m_CacTime)
                {
                    if (m_StVal == 1.0f && m_EndVal == 0.0f)// 들어올 때 
                    {
                        m_Color.a = 0.0f;
                        m_FadeImg.color = m_Color;
                        m_FadeImg.gameObject.SetActive(false);
                        m_StartFade = false;
                    }
                    else if (m_StVal == 0.0f && m_EndVal == 1.0f) //나갈 때
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
                    }
                }
            }
        }
        //-----m_FadeInOut
    }

    void RefreshMyInfo()
    {
        MyInfo_Text.text = "내정보 : 별명(" + GlobalValue.g_NickName +
                   ") : 점수(" + GlobalValue.g_BestScore.ToString() + 
                   "점) : 골드(" + GlobalValue.g_UserGold.ToString() + ")";
    }

    void ScFadeOutInit(string a_ScName)
    {
        SceneName = a_ScName;

        m_CacTime = 0.0f;
        m_AddTimer = 0.0f;
        m_StVal = 0.0f;
        m_EndVal = 1.0f;
        m_FadeImg.gameObject.SetActive(true);
        m_StartFade = true;
    }
}
