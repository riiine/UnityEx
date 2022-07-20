using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigBox : MonoBehaviour
{
    public Button m_OK_Btn = null;
    public Button m_Close_Btn = null;
    public Button m_ResetInfo_Btn = null;
    public InputField NickInputField = null;

    public Text m_Message = null;
    float ShowMsTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_OK_Btn != null)
            m_OK_Btn.onClick.AddListener(OKBtnFunction);

        if (m_Close_Btn != null)
            m_Close_Btn.onClick.AddListener(CloseBtnFunction);

        if (m_ResetInfo_Btn != null)
            m_ResetInfo_Btn.onClick.AddListener(ResetInfoBtnFunc);

        Text a_Placeholder = null;
        if (NickInputField != null)
        {
            Transform a_PlhTr = NickInputField.transform.Find("Placeholder");
            a_Placeholder = a_PlhTr.GetComponent<Text>();
        }

        if (a_Placeholder != null)
            a_Placeholder.text = GlobalValue.g_NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < ShowMsTimer)
        {
            ShowMsTimer -= Time.unscaledDeltaTime;
            if (ShowMsTimer <= 0.0f)
            {
                MessageOnOff(false); //메시지 끄기
            }
        }//if (0.0f < ShowMsTimer)
    }

    void OKBtnFunction()
    {
        // 이름 변경 ToDo
        string a_NickStr = NickInputField.text.Trim();
        if (a_NickStr == "")
        {
            MessageOnOff(true, "별명은 빈칸 없이 입력해 주셔야 합니다.");
            return;
        }

        if (!(2 <= a_NickStr.Length && a_NickStr.Length < 20))  //2~20
        {
            MessageOnOff(true, "별명은 2글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        //InGameUI 갱신 함수 호출
        InGameMgr a_InGameMgr = GameObject.FindObjectOfType<InGameMgr>();
        if (a_InGameMgr != null)
            a_InGameMgr.NickUIUpdate(a_NickStr);

        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
    }

    void CloseBtnFunction()
    {
        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
    }

    void ResetInfoBtnFunc()
    {
        PlayerPrefs.DeleteAll(); //모두 초기화 
        GlobalValue.LoadData();  //데이터들을 다시 로딩
        //GlobalValue.g_NickName = "초보영웅";
        //GlobalValue.g_BestScore = 0;
        //GlobalValue.g_UserGold = 0;
        //GlobalValue.g_Exp   = 0;
        //GlobalValue.g_Level = 0;

        //InGamne UI 갱신
        InGameMgr a_InGameMgr = GameObject.FindObjectOfType<InGameMgr>();
        if (a_InGameMgr != null)
        {
            a_InGameMgr.Reset_CurInfo();
            a_InGameMgr.Refresh_UI();
        }
    }

    void MessageOnOff(bool isOn = true, string Mess = "")
    {
        if (isOn == true)
        {
            m_Message.text = Mess;
            m_Message.gameObject.SetActive(true);
            ShowMsTimer = 5.0f;
        }
        else
        {
            m_Message.text = "";
            m_Message.gameObject.SetActive(false);
        }
    }

 }
