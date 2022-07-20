using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CrState
{
    Lock,       //구매불가상태
    BeforeBuy,  //구매가능상태
    Active      //구매완료
}

public class CharNodeCtrl : MonoBehaviour
{
    [HideInInspector] public CharType m_CrType = CharType.CrCount;  //초기화
    [HideInInspector] public CrState  m_CrState = CrState.Lock;

    public Text m_LvText;
    public Image m_CrIconImg;
    public Text m_HelpText;
    public Text m_BuyText;

    // Start is called before the first frame update
    void Start()
    {
        //리스트뷰에 있는 캐릭터 가격버튼을 눌러 구입시도를 한 경우
        Button m_BtnCom = this.GetComponentInChildren<Button>();
        if (m_BtnCom != null)
        {
            m_BtnCom.onClick.AddListener(() =>
            {
                Store_Mgr a_StoreMgr = null;
                GameObject a_StoreObj = GameObject.Find("Store_Mgr");
                if (a_StoreObj != null)
                    a_StoreMgr = a_StoreObj.GetComponent<Store_Mgr>();
                if (a_StoreMgr != null)
                    a_StoreMgr.BuyCharItem(m_CrType);
            });
        }//if (m_BtnCom != null)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitData(CharType a_CrType)
    {
        if (a_CrType < CharType.Char_0 || CharType.CrCount <= a_CrType)
            return;

        m_CrType = a_CrType;
        m_CrIconImg.sprite = GlobalValue.m_CrDataList[(int)a_CrType].m_IconImg;

        m_CrIconImg.GetComponent<RectTransform>().sizeDelta
            = new Vector2(GlobalValue.m_CrDataList[(int)a_CrType].m_IconSize.x
                                                                  * 135.0f, 135.0f);
        m_HelpText.text = GlobalValue.m_CrDataList[(int)a_CrType].m_SkillExp;
    }

    public void SetState(CrState a_CrState, int a_Price, int a_Lv = 0)
    {
        m_CrState = a_CrState;
        if (a_CrState == CrState.Lock) //잠긴 상태
        {
            m_LvText.color = new Color32(110, 110, 110, 255);
            m_LvText.text = "0/5";
            m_CrIconImg.color = new Color32(0, 0, 0, 185);
            m_HelpText.gameObject.SetActive(false);
            m_BuyText.text = a_Price.ToString() + " 골드"; //여기서는 그냥 기본 가격
        }
        else if (a_CrState == CrState.BeforeBuy) //구매 가능 상태
        {
            m_LvText.color = new Color32(110, 110, 110, 255);
            m_LvText.text = "0/5";
            m_CrIconImg.color = new Color32(255, 255, 255, 120); 
            m_HelpText.gameObject.SetActive(true);
            m_HelpText.color = new Color32(110, 110, 110, 255); //new Color32(180, 180, 180, 255);
            m_BuyText.text = a_Price.ToString() + " 골드";  //여기서는 그냥 기본 가격
        }
        else if (a_CrState == CrState.Active) //활성화 상태
        {
            m_LvText.color = new Color32(255, 255, 255, 255);
            m_LvText.text = a_Lv.ToString() + "/5";
            m_CrIconImg.color = new Color32(255, 255, 255, 255);
            m_HelpText.gameObject.SetActive(true);
            m_HelpText.color = new Color32(0, 0, 0, 255); //new Color32(255, 255, 255, 255);
            int a_CacPrice = a_Price + (a_Price * (a_Lv - 1));
            m_BuyText.text = "Up " + a_CacPrice.ToString() + " 골드"; //여기서는 업데이트 가격
        }
    }//public void SetState(CrState a_CrState, int a_Price, int a_Lv = 0)

}
