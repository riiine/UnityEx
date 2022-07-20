using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrSmallNode : MonoBehaviour
{
    [HideInInspector] public CharType m_CrType;

    [HideInInspector] public int m_CurSkCount = 0;
    public Text m_LvText;
    public Image m_CrIconImg;
    public Text m_SkCountText; //스킬 카운트
    public Image m_BackBtnImg;
    public Text m_ShortcutTxt;

    // Start is called before the first frame update
    void Start()
    {
        Button m_BtnCom = this.GetComponent<Button>(); //using UnityEngine.UI;  추가해야 함
        if (m_BtnCom != null)
        {
            m_BtnCom.onClick.AddListener(() =>
            {
                if (GlobalValue.m_CrDataList[(int)m_CrType].m_CurSkillCount <= 0)
                { //스킬 소진으로 사용할 수 없음
                    return;
                }

                HeroCtrl a_Hero = GameObject.FindObjectOfType<HeroCtrl>();
                if (a_Hero != null)
                    a_Hero.UseItem(m_CrType);
                Refresh_UI(m_CrType);
            });
        }//if (m_FireBtnCom != null)       
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void InitState(CharInfo a_CrInfo)
    {
        m_CrType = a_CrInfo.m_CrType;
        m_ShortcutTxt.text = ((int)m_CrType + 1).ToString();
        m_CrIconImg.sprite = a_CrInfo.m_IconImg;
        m_CrIconImg.GetComponent<RectTransform>().sizeDelta
                = new Vector2(a_CrInfo.m_IconSize.x * 103.0f, 103.0f);
        m_CurSkCount = a_CrInfo.m_Level;
        //스프라이트 사이즈 조정 필요
        m_LvText.text = "Lv " + a_CrInfo.m_Level.ToString();
        m_SkCountText.text = m_CurSkCount.ToString() +
                                " / " + a_CrInfo.m_Level.ToString();
    }

    public void Refresh_UI(CharType a_CrType)
    {
        m_CurSkCount = GlobalValue.m_CrDataList[(int)a_CrType].m_CurSkillCount;
        m_SkCountText.text =
            m_CurSkCount.ToString() +
            " / " + GlobalValue.m_CrDataList[(int)a_CrType].m_Level.ToString();

        if (m_CurSkCount <= 0)
        {
            m_CrIconImg.color = new Color32(255, 255, 255, 100);
        }
    }
}
