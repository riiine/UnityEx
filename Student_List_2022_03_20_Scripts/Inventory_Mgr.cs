using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Mgr : MonoBehaviour
{
    List<Item_Info> m_Item_List = new List<Item_Info>();

    [Header("AddEditMode")]
    public GameObject m_AddNode_Root = null;
    public InputField m_Name_Ifd = null;
    public InputField m_Korean_Ifd = null;
    public InputField m_English_Ifd = null;
    public InputField m_Math_Ifd = null;

    public Button Add_Btn = null;
    public Button Sort_Btn = null;
    public Button Show_Btn = null;
    public Button ClearListBtn = null;
    public Button Del_Btn = null;
    public Button Search_Btn = null;
    public Button ReturnMain_Btn = null;

    public Text PrintList = null;
    public Text ShowStudentInfo = null;

    public Text Name_Label = null;
    public Text Korean_Label = null;
    public Text English_Label = null;
    public Text Math_Label = null;


    // Start is called before the first frame update
    void Start()
    {
        if (Add_Btn != null)
            Add_Btn.onClick.AddListener(AddBtnFunc);

        if (Sort_Btn != null)
            Sort_Btn.onClick.AddListener(SortBtnFunc);

        if (Show_Btn != null)
            Show_Btn.onClick.AddListener(ShowBtnFunc);

        if (ClearListBtn != null)
            ClearListBtn.onClick.AddListener(ClearListFunc);

        if (Del_Btn != null)
            Del_Btn.onClick.AddListener(DelBtnFunc);

        if (Search_Btn != null)
            Search_Btn.onClick.AddListener(SearchBtnFunc);
        
        if (ReturnMain_Btn != null)
            ReturnMain_Btn.onClick.AddListener(ReturnMainBtnFunc);

        Del_Btn.gameObject.SetActive(false);
        ReturnMain_Btn.gameObject.SetActive(false);

        ShowStudentInfo.text = ""; //�˻� �� ���� �ʱ�ȭ

        LoadList();
        RefreshUIList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    int CompTotal(Item_Info a, Item_Info b) //������ ���� ���� �Լ�
    {
        //��������(DESC) : ���������� ���������� �����ϴ� �� 10, 9, 8, 7, 6, ...
        return -a.m_Total.CompareTo(b.m_Total);
    }

    void AddBtnFunc()
    {
        

        if (m_Name_Ifd == null || m_Korean_Ifd == null ||
            m_English_Ifd == null || m_Math_Ifd == null)
            return;  //����ó��

        //StName_IFd.text <-- InputField(�Է»���)�� ���� ���ڿ��� ������ ���
        string a_StName = m_Name_Ifd.text.Trim(); //.Trim() �� �� ���� ���� �Լ�
        string a_StKorean = m_Korean_Ifd.text.Trim();
        string a_StEnglish = m_English_Ifd.text.Trim();
        string a_StMath = m_Math_Ifd.text.Trim();

        if (string.IsNullOrEmpty(a_StName) == true ||
            string.IsNullOrEmpty(a_StKorean) == true ||
            string.IsNullOrEmpty(a_StEnglish) == true ||
            string.IsNullOrEmpty(a_StMath) == true)
            return;  //����ó��

        int a_Korean = 0; bool a_isKoreanOK = false;
        int a_English = 0; bool a_isEnglishOK = false;
        int a_Math = 0; bool a_isMathOK = false;

        a_isKoreanOK = int.TryParse(a_StKorean, out a_Korean);
        a_isEnglishOK = int.TryParse(a_StEnglish, out a_English);
        a_isMathOK = int.TryParse(a_StMath, out a_Math);

        if (a_isKoreanOK == false || a_isEnglishOK == false || a_isMathOK == false)
            return; //����ó��

        int a_Index = m_Item_List.FindIndex(x => x.m_Name == a_StName);
        if (a_Index < 0) //���� �̸��� �л��� �������� ���� �� 
        {
            Item_Info a_Node = new Item_Info(a_StName, a_Korean, a_English, a_Math);
            m_Item_List.Add(a_Node);
        }
        else //if(0 <= a_Index) //���� �̸��� �̹� ��ϵǾ� �ִٸ�...
        {
            m_Item_List[a_Index].m_Korean = a_Korean;
            m_Item_List[a_Index].m_English = a_English ;
            m_Item_List[a_Index].m_Math = a_Math;
            m_Item_List[a_Index].m_Total =
              (m_Item_List[a_Index].m_Korean + m_Item_List[a_Index].m_English
              + m_Item_List[a_Index].m_Math);
            m_Item_List[a_Index].m_Avg = (m_Item_List[a_Index].m_Total)/3;
        }

        SaveList();
        RefreshUIList();

        //------ �Է� ���� �ʱ�ȭ
        m_Name_Ifd.text = "";
        m_Korean_Ifd.text = "";
        m_English_Ifd.text = "";
        m_Math_Ifd.text = "";
        //------ �Է� ���� �ʱ�ȭ
    }

    void RefreshUIList()
    { //------------------- ������ ����Ʈ ���

        PrintList.text = "";

        if (m_Item_List.Count <= 0)
            return;

        string a_StrBuff = "";
        for (int ii = 0; ii < m_Item_List.Count; ii++)
        {
            a_StrBuff +=
                string.Format("{0} : ����({1}) ����({2}) ����({3}) ����({4:N2}) ���({5:N2})",
                    m_Item_List[ii].m_Name,
                    m_Item_List[ii].m_Korean,
                    m_Item_List[ii].m_English,
                    m_Item_List[ii].m_Math,
                    m_Item_List[ii].m_Total,
                    m_Item_List[ii].m_Avg
                    );

            a_StrBuff += "\n";  //"\n" �ٹٲ� ��ȣ(���� ����)
        }//for(int ii = 0; ii < m_Item_List.Count; ii++)

        PrintList.text = a_StrBuff;
    }//void RefreshUIList()
    void SortBtnFunc()
    {
        List<Item_Info> a_CopyList = m_Item_List.ToList(); //����Ʈ���� using System.Linq;
        a_CopyList.Sort(CompTotal);

        //------ ���ĵ� ��� �����ֱ�
        PrintList.text = "";

        if (a_CopyList.Count <= 0)
            return;

        string a_StrBuff = "";
        for (int ii = 0; ii < a_CopyList.Count; ii++)
        {
            a_StrBuff +=
                string.Format("{0} : ����({1}) ����({2}) ����({3}) ����({4:N2}) ���({5:N2})",
                    a_CopyList[ii].m_Name,
                    a_CopyList[ii].m_Korean,
                    a_CopyList[ii].m_English,
                    a_CopyList[ii].m_Math,
                    a_CopyList[ii].m_Total,
                    a_CopyList[ii].m_Avg);

            a_StrBuff += "\n";  //"\n" �ٹٲ� ��ȣ(���� ����)
        }//for(int ii = 0; ii < a_CopyList.Count; ii++)

        PrintList.text = a_StrBuff;
        //------ ���ĵ� ��� �����ֱ�
    }

    void SaveList()
    {
        PlayerPrefs.DeleteAll(); // ���� ���¸� ��� ����

        if (m_Item_List.Count <= 0)
            return;

        PlayerPrefs.SetInt("Item_Count", m_Item_List.Count); //������ ��

        Item_Info a_Node;
        string a_KeyBuff = "";
        for (int ii = 0; ii < m_Item_List.Count; ii++)
        {
            a_Node = m_Item_List[ii];

            a_KeyBuff = string.Format("Item_{0}_Name", ii);
            PlayerPrefs.SetString(a_KeyBuff, a_Node.m_Name);

            a_KeyBuff = string.Format("Item_{0}_Korean", ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Korean);

            a_KeyBuff = string.Format("Item_{0}_English", ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_English);

            a_KeyBuff = string.Format("Item_{0}_Math", ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Math);

            a_KeyBuff = string.Format("Item_{0}_Total", ii);
            PlayerPrefs.SetFloat(a_KeyBuff, a_Node.m_Total);

            a_KeyBuff = string.Format("Item_{0}_Avg", ii);
            PlayerPrefs.SetFloat(a_KeyBuff, a_Node.m_Avg);

        }//for(int ii = 0; ii < m_Item_List.Count; ii++)
    }

    void LoadList()
    {
        int a_ItemCount = PlayerPrefs.GetInt("Item_Count", 0);

        if (a_ItemCount <= 0)
            return;

        Item_Info a_Node;
        string a_KeyBuff = "";
        for (int ii = 0; ii < a_ItemCount; ii++)
        {
            a_KeyBuff = string.Format("Item_{0}_Name", ii);
            string a_ItName = PlayerPrefs.GetString(a_KeyBuff, "");

            a_KeyBuff = string.Format("Item_{0}_Korean", ii);
            int a_ItKorean = PlayerPrefs.GetInt(a_KeyBuff, 0);

            a_KeyBuff = string.Format("Item_{0}_English", ii);
            int a_ItEnglish = PlayerPrefs.GetInt(a_KeyBuff, 0);

            a_KeyBuff = string.Format("Item_{0}_Math", ii);
            int a_ItMath = PlayerPrefs.GetInt(a_KeyBuff, 0);

            a_KeyBuff = string.Format("Item_{0}_Total", ii);
            float a_ItTotal = PlayerPrefs.GetFloat(a_KeyBuff, 0.0f);

            a_KeyBuff = string.Format("Item_{0}_Avg", ii);
            float a_ItAvg = PlayerPrefs.GetFloat(a_KeyBuff, 0.0f);

            a_Node = new Item_Info(a_ItName, a_ItKorean, a_ItEnglish, a_ItMath);
            a_Node.m_Total = a_ItTotal;
            a_Node.m_Avg = a_ItAvg;


            m_Item_List.Add(a_Node);

        }//for(int ii = 0; ii < a_ItemCount; ii++)
    }

    void ShowBtnFunc() //�Է��� ������� ����
    {
        RefreshUIList();
    }

    void ClearListFunc()
    {
        m_Item_List.Clear();
        SaveList();
        RefreshUIList();
    }

    void DelBtnFunc()
    {
        if (m_Name_Ifd == null)
            return;

        string a_StName = m_Name_Ifd.text.Trim();
        if (string.IsNullOrEmpty(a_StName) == true)
            return;

        int a_Index = m_Item_List.FindIndex(x => x.m_Name == a_StName);
        if (a_Index < 0) //���� �̸��� �л��� �������� ���� �� 
            return;

        m_Item_List.RemoveAt(a_Index);

        //------ �Է� ���� �ʱ�ȭ
        m_Name_Ifd.text = "";
        m_Korean_Ifd.text = "";
        m_English_Ifd.text = "";
        m_Math_Ifd.text = "";
        //------ �Է� ���� �ʱ�ȭ

        SaveList();
        RefreshUIList();
    }

    void SearchBtnFunc()
    {
        //��ư ���̱�
        Del_Btn.gameObject.SetActive(true);
        ReturnMain_Btn.gameObject.SetActive(true);

        //��ư ���߱�
        Sort_Btn.gameObject.SetActive(false);
        Add_Btn.gameObject.SetActive(false);
        Show_Btn.gameObject.SetActive(false);
        ClearListBtn.gameObject.SetActive(false);
        Search_Btn.gameObject.SetActive(false);

        //���ʿ��� ���� ���߱�
        m_Name_Ifd.gameObject.SetActive(false);
        m_Korean_Ifd.gameObject.SetActive(false);
        m_English_Ifd.gameObject.SetActive(false);
        m_Math_Ifd.gameObject.SetActive(false);
        Name_Label.gameObject.SetActive(false);
        Korean_Label.gameObject.SetActive(false);
        English_Label.gameObject.SetActive(false);
        Math_Label.gameObject.SetActive(false);

        if (m_Name_Ifd == null)
            return;

        string a_StName = m_Name_Ifd.text.Trim();

        if (string.IsNullOrEmpty(a_StName) == true)
            return;

        int a_Index = m_Item_List.FindIndex(x => x.m_Name == a_StName);

        if (a_Index < 0) //���� �̸��� �л��� �������� ���� �� 
            return;

        else
        { // ���� �̸��� �л��� ������ ��
            ShowStudentInfo.text = ""; //�ʱ�ȭ

            ShowStudentInfo.text +=
                string.Format("{0} : ����({1}) ����({2}) ����({3}) ����({4:N2}) ���({5:N2})",
                    m_Item_List[a_Index].m_Name,
                    m_Item_List[a_Index].m_Korean,
                    m_Item_List[a_Index].m_English,
                    m_Item_List[a_Index].m_Math,
                    m_Item_List[a_Index].m_Total,
                    m_Item_List[a_Index].m_Avg
                    );
        }
    }

    void ReturnMainBtnFunc()
    {
        //�ʱ�ȭ
        ShowStudentInfo.text = "";

        //��ư �����
        Del_Btn.gameObject.SetActive(false);
        ReturnMain_Btn.gameObject.SetActive(false);

        //��ư ���̱�
        Sort_Btn.gameObject.SetActive(true);
        Add_Btn.gameObject.SetActive(true);
        Show_Btn.gameObject.SetActive(true);
        ClearListBtn.gameObject.SetActive(true);
        Search_Btn.gameObject.SetActive(true);

        //���� ���̱�
        m_Name_Ifd.gameObject.SetActive(true);
        m_Korean_Ifd.gameObject.SetActive(true);
        m_English_Ifd.gameObject.SetActive(true);
        m_Math_Ifd.gameObject.SetActive(true);
        Name_Label.gameObject.SetActive(true);
        Korean_Label.gameObject.SetActive(true);
        English_Label.gameObject.SetActive(true);
        Math_Label.gameObject.SetActive(true);

        SaveList();
        RefreshUIList();
    }
}