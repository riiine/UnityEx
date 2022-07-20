using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store_Mgr : MonoBehaviour
{
    public Button m_Back_Btn = null;
    public Text m_UserInfoTxt;

    [Header("-------- Item목록 --------")]
    public GameObject m_Item_ScrollContent;  //ScrollContent 차일드로 생성될 Parent 객체
    public GameObject m_Item_NodeObj = null; //Node Prefab

    CharNodeCtrl[] m_CrNodeList;    //<---스크롤에 붙어 있는 Item 목록들...  
    CharType m_BuyCrType;           //-- 지금 뭘 구입하려고 시도한 건지?

    [Header("-------- 저장 정보 초기화 --------")]
    public Button m_ResetInfo_Btn = null;

    // Start is called before the first frame update
    void Start()
    {
        GlobalValue.LoadData();

        if (m_Back_Btn != null)
            m_Back_Btn.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
            });

        m_UserInfoTxt.text = "별명(" + GlobalValue.g_NickName
                    + ") : 보유골드(" + GlobalValue.g_UserGold + ")";

        //----------------- 아이템 목록 추가
        GameObject a_ItemObj = null;
        CharNodeCtrl a_CrItemNode = null;
        for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
        {
            a_ItemObj = (GameObject)Instantiate(m_Item_NodeObj);
            a_CrItemNode = a_ItemObj.GetComponent<CharNodeCtrl>();

            a_CrItemNode.InitData(GlobalValue.m_CrDataList[ii].m_CrType);

            a_ItemObj.transform.SetParent(m_Item_ScrollContent.transform, false); // false Prefab의 로컬 포지션을 유지하면서 추가해 주겠다는 뜻.

            //m_Item_ScrollContent.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f); // 피벗 좌표를 Top, Left로 설정
        }//for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
        //----------------- 아이템 목록 추가

        RefreshCrItemList();

        if (m_ResetInfo_Btn != null)
            m_ResetInfo_Btn.onClick.AddListener(() =>
            {
                string a_Mess = "정말 초기화 하시겠습니까? 모든 저장 정보가 초기화 됩니다.";
                GameObject a_DlgRsc = Resources.Load("DlgBox") as GameObject;
                GameObject a_DlgBoxObj = (GameObject)Instantiate(a_DlgRsc);
                GameObject a_Canvas = GameObject.Find("Canvas");
                a_DlgBoxObj.transform.SetParent(a_Canvas.transform, false); 
                // false Prefab의 로컬 포지션을 유지하면서 추가해 주겠다는 뜻.
                DlgBox_Ctrl a_DlgBox = a_DlgBoxObj.GetComponent<DlgBox_Ctrl>();
                if (a_DlgBox != null)
                {
                    a_DlgBox.m_Title_Txt.text = "초 기 화";
                    a_DlgBox.SetMessage(a_Mess, ResetInfoBtnFunc);
                }
            });

    }//void Start()

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshCrItemList()
    { //Count == 0 인 상태가 처음 나오는 아이템만 구매가능으로 표시해 준다.
        if (m_Item_ScrollContent != null)
        {
            if (m_CrNodeList == null || m_CrNodeList.Length <= 0)
                m_CrNodeList = m_Item_ScrollContent.GetComponentsInChildren<CharNodeCtrl>();
        }

        int a_FindAv = -1;
        for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
        {
            if (m_CrNodeList[ii].m_CrType != GlobalValue.m_CrDataList[ii].m_CrType)
                continue;

            if (GlobalValue.m_CrDataList[ii].m_Level <= 0)
            {
                if (a_FindAv < 0)
                {
                    //m_Level이 0보다 작은 상태가 처음 나온 상태
                    //구입가능 표시
                    m_CrNodeList[ii].SetState(CrState.BeforeBuy,
                                     GlobalValue.m_CrDataList[ii].m_Price);

                    a_FindAv = ii;
                }
                else
                {
                    //전부 Lock 표시
                    m_CrNodeList[ii].SetState(CrState.Lock,
                                    GlobalValue.m_CrDataList[ii].m_Price);
                }

                continue;
            }//if (GlobalValue.m_CrDataList[ii].m_Level <= 0)

            //활성화
            m_CrNodeList[ii].SetState(CrState.Active,
                                    GlobalValue.m_CrDataList[ii].m_UpPrice,
                                    GlobalValue.m_CrDataList[ii].m_Level);

        }// for (int ii = 0; ii < GlobalValue.m_CrDataList.Count; ii++)
    }//void RefreshCrItemList()

    public void BuyCharItem(CharType a_ChType)
    {//리스트뷰에 있는 캐릭터 가격버튼을 눌러 구입시도를 한 경우

        if ((int)a_ChType < 0 || 
            GlobalValue.m_CrDataList.Count <= (int)a_ChType)
            return;

        m_BuyCrType = a_ChType;

        string a_Mess = "";
        CrState a_CrState = CrState.Lock;
        CharInfo a_CrInfo = GlobalValue.m_CrDataList[(int)a_ChType];
        bool a_IsBuyPossible = false;
        if (m_CrNodeList != null && (int)a_ChType < m_CrNodeList.Length)
        {
            a_CrState = m_CrNodeList[(int)a_ChType].m_CrState;
        }

        if (a_CrState == CrState.Lock) //잠긴 상태
        {
            a_Mess = "이 아이템은 Lock 상태로 구입할 수 없습니다.";
        }
        else if (a_CrState == CrState.BeforeBuy) //구매 가능 상태
        {
            if (GlobalValue.g_UserGold < a_CrInfo.m_Price)
            {
                a_Mess = "보유(누적) 골드가 모자랍니다.";
            }
            else
            {
                a_Mess = "정말 구입하시겠습니까?";
                a_IsBuyPossible = true; //-----> 이 조건일 때 구매
            }
        }
        else if (a_CrState == CrState.Active) //활성화(업그레이드가능) 상태
        {
            int a_Cost = a_CrInfo.m_UpPrice + (a_CrInfo.m_UpPrice * (a_CrInfo.m_Level - 1));
            if (5 <= a_CrInfo.m_Level)
            {
                a_Mess = "최고 레벨입니다.";
            }
            else if (GlobalValue.g_UserGold < a_Cost)
            {
                a_Mess = "레벨업에 필요한 보유(누적) 골드가 모자랍니다.";
            }
            else
            {
                a_Mess = "정말 업그레이드하시겠습니까?";
                //-----> 이 조건일 때 업그레이드
                a_IsBuyPossible = true; //-----> 이 조건일 때 구매
            }
        } //else if (a_CrState == CrState.Active) //활성화(업그레이드가능) 상태

        GameObject a_DlgRsc = Resources.Load("DlgBox") as GameObject;
        GameObject a_DlgBoxObj = (GameObject)Instantiate(a_DlgRsc);
        GameObject a_Canvas = GameObject.Find("Canvas");
        a_DlgBoxObj.transform.SetParent(a_Canvas.transform, false); // false Prefab의 로컬 포지션을 유지하면서 추가해 주겠다는 뜻.
        DlgBox_Ctrl a_DlgBox = a_DlgBoxObj.GetComponent<DlgBox_Ctrl>();
        if (a_DlgBox != null)
        {
            if (a_IsBuyPossible == true)
                a_DlgBox.SetMessage(a_Mess, TryBuyCrItem);
            else
                a_DlgBox.SetMessage(a_Mess);
        }

    }//public void BuyCharItem(CharType a_ChType)

    public void TryBuyCrItem()  //구매 2단계(구매 확정) 함수 
    {
        //if ((int)m_BuyCrType < 0 ||
        //    GlobalValue.m_CrDataList.Count <= (int)m_BuyCrType)
        //    return;   //상위 함수에서 체크 함

        CharInfo a_CrInfo = GlobalValue.m_CrDataList[(int)m_BuyCrType];

        //if(5 <= a_CrInfo.m_Level) //구매 조건 체크
        //    return;   //상위 함수에서 체크 함

        int a_Cost = a_CrInfo.m_Price;
        if (0 < a_CrInfo.m_Level)
            a_Cost = a_CrInfo.m_UpPrice + (a_CrInfo.m_UpPrice * (a_CrInfo.m_Level - 1));

        //if (GlobalValue.g_UserGold < a_Cost)
        //    return;   //상위 함수에서 체크 함

        a_CrInfo.m_Level++;               //레벨 증가 
        GlobalValue.g_UserGold -= a_Cost; //가격 차감
        if (GlobalValue.g_UserGold < 0)
            GlobalValue.g_UserGold = 0;

        //로컬에 Save
        PlayerPrefs.SetInt("UserGold", GlobalValue.g_UserGold);
        GlobalValue.SaveCrItem(); //아이템 구매 상태를 로컬에 저장

        //로컬의 변수, UI 값들을 갱신해 주기... 
        RefreshCrItemList();
        m_UserInfoTxt.text = "별명(" + GlobalValue.g_NickName
                    + ") : 보유골드(" + GlobalValue.g_UserGold + ")";
    }

    void ResetInfoBtnFunc()
    {
        PlayerPrefs.DeleteAll(); //모두 초기화 
        GlobalValue.LoadData();  //데이터들을 다시 로딩

        //UI 값들을 갱신해 주기... 
        RefreshCrItemList();
        m_UserInfoTxt.text = "별명(" + GlobalValue.g_NickName
                    + ") : 보유골드(" + GlobalValue.g_UserGold + ")";
    }
}
