using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharType
{
    Char_0 = 0,
    Char_1,
    Char_2,
    Char_3,
    Char_4,
    Char_5,
    CrCount
}

public class CharInfo  //각 Item 정보
{
    public string m_Name = "";              //캐릭터 이름
    public CharType m_CrType = CharType.Char_0;
    public Vector2 m_IconSize = Vector2.one;  //아이콘의 가로 사이즈, 세로 사이즈
    public int m_Price = 500;   //아이템 기본 가격 
    public int m_UpPrice = 250; //업그레이드 가격, 타입에 따라서
    public int m_Level = 0;
    //그전엔 Lock, 레벨0 이면 아직 구매 안됨 (구매가 완료되면 레벨 1부터)
    public int m_CurSkillCount = 1;   //사용할 수 있는 스킬 카운트
    //public int m_MaxUsable = 1;     //사용할 수 있는 최대 스킬 카운트는 Level과 같다.
    public string m_SkillExp = "";    //스킬 효과 설명
    public Sprite m_IconImg = null;

    public void SetType(CharType a_CrType)
    {
        m_CrType = a_CrType;
        if (a_CrType == CharType.Char_0)
        {
            m_Name = "강아지";
            m_IconSize.x = 0.766f;   //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 500; //기본가격
            m_UpPrice = 250; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "Hp+30%충전";
            m_IconImg = Resources.Load("Image/m0011", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == CharType.Char_1)
        {
            m_Name = "울버독";
            m_IconSize.x = 0.81f;    //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 1000; //기본가격
            m_UpPrice = 500; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "Hp+50%충전";
            m_IconImg = Resources.Load("Image/m0367", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == CharType.Char_2)
        {
            m_Name = "구미호";
            m_IconSize.x = 0.946f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 2000; //기본가격
            m_UpPrice = 1000; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "Hp+100%충전";
            m_IconImg = Resources.Load("Image/m0054", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == CharType.Char_3)
        {
            m_Name = "야옹이";
            m_IconSize.x = 0.93f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 4000; //기본가격
            m_UpPrice = 2000; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "보호막";
            m_IconImg = Resources.Load("Image/m0423", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == CharType.Char_4)
        {
            m_Name = "드래곤";
            m_IconSize.x = 0.93f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 8000; //기본가격
            m_UpPrice = 4000; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "총알3배";
            m_IconImg = Resources.Load("Image/m0244", typeof(Sprite)) as Sprite;
        }
        else if (a_CrType == CharType.Char_5)
        {
            m_Name = "팅커벨";
            m_IconSize.x = 0.93f;     //세로에 대한 가로 비율
            m_IconSize.y = 1.0f;     //세로를 기준으로 잡을 것이기 때문에 그냥 1.0f

            m_Price = 15000; //기본가격
            m_UpPrice = 8000; //Lv1->Lv2  (m_UpPrice + (m_UpPrice * (m_Level - 1)) 가격 필요

            m_SkillExp = "유도탄";
            m_IconImg = Resources.Load("Image/m0172", typeof(Sprite)) as Sprite;
        }
    }
}

public class GlobalValue 
{
    public static string g_NickName = "";   //유저의 별명
    public static int g_BestScore = 0;      //게임점수
    public static int g_UserGold = 0;       //게임머니
    public static int g_Exp = 0;            //경험치 Experience
    public static int g_Level = 0;          //레벨

    //캐릭터 아이템 데이터 리스트(보유 아이템은 배열로 레벨만 갖고 있으면 된다. 
    //레벨0 이면 아직 보유 안한 것으로...)
    public static List<CharInfo> m_CrDataList = new List<CharInfo>();

    public static void LoadData()
    {
        InitData();

        g_NickName = PlayerPrefs.GetString("NickName", "초보영웅");
        g_BestScore = PlayerPrefs.GetInt("BestScore", 0);
        g_UserGold  = PlayerPrefs.GetInt("UserGold", 0);
        g_Exp   = PlayerPrefs.GetInt("UserExp", 0);
        g_Level = PlayerPrefs.GetInt("UserLevel", 0);

        //----- ItemLevel 로딩 하기
        string a_KeyBuff = "";
        int a_ItmCount = PlayerPrefs.GetInt("CrItem_Count", 0);
        if(0 < a_ItmCount)
        {
            for (int a_ii = 0; a_ii < a_ItmCount; a_ii++)
            {
                if (GlobalValue.m_CrDataList.Count <= a_ii)
                    continue;

                a_KeyBuff = string.Format("ChrItemLv_{0}", a_ii);
                GlobalValue.m_CrDataList[a_ii].m_Level = PlayerPrefs.GetInt(a_KeyBuff, 0);
            }
        }//if(0 < a_ItmCount)
        else
        {
            m_CrDataList.Clear();
            InitData();
        }
        //----- ItemLevel 로딩 하기
    }

    public static void SaveCrItem() //아이템 Level(구매상태) 저장하기
    {
        PlayerPrefs.SetInt("CrItem_Count", GlobalValue.m_CrDataList.Count);

        if (GlobalValue.m_CrDataList.Count <= 0)
            return;

        string a_KeyBuff = "";
        for (int a_ii = 0; a_ii < GlobalValue.m_CrDataList.Count; a_ii++)
        {
            a_KeyBuff = string.Format("ChrItemLv_{0}", a_ii);
            PlayerPrefs.SetInt(a_KeyBuff, GlobalValue.m_CrDataList[a_ii].m_Level);
        }//for (int a_ii = 0; a_ii < GlobalValue.m_CrDataList.Count; a_ii++)
    }

    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll(); //모두 초기화 
    }

    public static void InitData()
    {
        if (0 < m_CrDataList.Count)
            return;

        CharInfo a_CrItemNd;
        for (int ii = 0; ii < (int)CharType.CrCount; ii++)
        {
            a_CrItemNd = new CharInfo();
            a_CrItemNd.SetType((CharType)ii);
            m_CrDataList.Add(a_CrItemNd);
        }
    }//public static void InitData()

}
