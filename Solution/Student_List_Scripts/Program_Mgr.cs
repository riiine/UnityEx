using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Program_Mgr : MonoBehaviour
{
    List<Student> std_List = new List<Student>();

    [Header("AddEditMode")]
    public GameObject AddModeRoot = null;
    public InputField StName_IFd = null;
    public InputField StKor_IFd  = null;
    public InputField StEng_IFd  = null;
    public InputField StMath_IFd = null;

    public Button Add_Btn = null;

    public Text PrintList = null;

    public Button Find_Btn = null;
    public Button Sort_Btn = null;
    public Button ShowList_Btn = null;
    public Text AddMd_InfoLabel = null;

    [Header("DelEditMode")]
    public GameObject DelModeRoot = null;
    public Button Back_Btn = null;
    public Button Del_Btn = null;
    public Text DelMdFindInfo = null;

    float m_ShowTime = 0.0f;
    int m_FIndex = -1;

    int CompTotal(Student a, Student b) //총점순 정렬 조건 함수
    {
        //내림차순(DESC) : 높은값에서 낮은 값으로 정렬하는 것  10, 9, 8, 7, 6, 5, ....
        return -a.m_Total.CompareTo(b.m_Total);
    }

    // Start is called before the first frame update
    void Start()
    {
        //--------------------------- [Header("AddEditMode")]
        if (Add_Btn != null)
            Add_Btn.onClick.AddListener(AddBtnFunc);

        if (Find_Btn != null)
            Find_Btn.onClick.AddListener(FindBtnFunc);

        if (Sort_Btn != null)
            Sort_Btn.onClick.AddListener(SortBtnFunc);

        if (ShowList_Btn != null)
            ShowList_Btn.onClick.AddListener(ShowListBtnFunc);
        //--------------------------- [Header("AddEditMode")]

        //--------------------------- [Header("DelEditMode")]
        if (Back_Btn != null)
            Back_Btn.onClick.AddListener(BackBtnFunc);

        if (Del_Btn != null)
            Del_Btn.onClick.AddListener(DelBtnFunc);
        //--------------------------- [Header("DelEditMode")]

        LoadList();
        RefreshUIList(std_List);
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < m_ShowTime)
        {
            m_ShowTime -= Time.deltaTime;

            if (m_ShowTime <= 0.0f)
            {
                if (AddMd_InfoLabel != null)
                    AddMd_InfoLabel.gameObject.SetActive(false);
            }
        }//if (0.0f < m_ShowTime)
    }

    void AddBtnFunc()
    {
        if (StName_IFd == null || StKor_IFd == null ||
            StEng_IFd == null || StMath_IFd == null)
            return;  //예외처리

        //StName_IFd.text <-- InputField(입력상자)의 값을 문자열로 얻어오는 방법
        string a_StName = StName_IFd.text.Trim(); //.Trim()  앞 뒤 공백 제거 함수
        string a_StKor  = StKor_IFd.text.Trim();
        string a_StEng  = StEng_IFd.text.Trim();
        string a_StMath = StMath_IFd.text.Trim();

        if (string.IsNullOrEmpty(a_StName) == true ||
            string.IsNullOrEmpty(a_StKor) == true ||
            string.IsNullOrEmpty(a_StEng) == true ||
            string.IsNullOrEmpty(a_StMath) == true)
        return;  //예외처리

        int a_Kor = 0; bool a_isKorOK = false;
        int a_Eng = 0; bool a_isEngOK = false;
        int a_Math = 0; bool a_isMathOK = false;

        a_isKorOK  = int.TryParse(a_StKor, out a_Kor);
        a_isEngOK  = int.TryParse(a_StEng, out a_Eng);
        a_isMathOK = int.TryParse(a_StMath, out a_Math);

        if (a_isKorOK == false || a_isEngOK == false || a_isMathOK == false)
            return; //예외처리

        if ((a_Kor < 0 || 100 < a_Kor) ||
            (a_Eng < 0 || 100 < a_Eng) ||
            (a_Math < 0 || 100 < a_Math))
            return;   //예외처리

        //검색 1번 방법
        //int a_Index = std_List.FindIndex(x => x.m_Name == a_StName);
        //if (a_Index < 0)
        //{
        //    Student a_Node = new Student();
        //    a_Node.InputData(a_StName, a_Kor, a_Eng, a_Math);
        //    std_List.Add(a_Node);
        //}
        //else //if(0 <= a_Index) // 같은 이름이 이미 등록되어 있다면...
        //{
        //    std_List[a_Index].InputData(std_List[a_Index].m_Name, a_Kor, a_Eng, a_Math);
        //}

        //검색 2번 방법
        //Student a_FNode = null;
        //for (int ii = 0; ii < std_List.Count; ii++)
        //{
        //    if (std_List[ii].m_Name == a_StName)
        //    {
        //        a_FNode = std_List[ii];
        //        break;
        //    }
        //}

        //검색 3번 방법
        Student a_FNode = std_List.Find(a_NN => a_NN.m_Name == a_StName);

        if (a_FNode == null) //a_StName 이름의 학생 리스트에 존재하지 않는다는 뜻
        {
            Student a_Node = new Student();
            a_Node.InputData(a_StName, a_Kor, a_Eng, a_Math);
            std_List.Add(a_Node);
        }
        else  // a_StName 이름이 이미 등록되어 있다면...
        {
            a_FNode.InputData(a_FNode.m_Name, a_Kor, a_Eng, a_Math);
        }

        SaveList();
        RefreshUIList(std_List);

        //------ 입력 상자 초기화 
        StName_IFd.text = "";
        StKor_IFd.text = "";
        StEng_IFd.text = "";
        StMath_IFd.text = "";
        //------ 입력 상자 초기화 
    } //void AddBtnFunc()

    void SaveList()
    {
        PlayerPrefs.DeleteAll();   //저장 상태를 모두 제거

        if (std_List.Count <= 0)
            return;

        PlayerPrefs.SetInt("St_Count", std_List.Count); //학생 수 저장

        Student a_Node;
        string a_KeyBuff = "";
        for (int a_ii = 0; a_ii < std_List.Count; a_ii++)
        {
            a_Node = std_List[a_ii];
            a_KeyBuff = string.Format("ST_{0}_Name", a_ii);
            PlayerPrefs.SetString(a_KeyBuff, a_Node.m_Name);
            a_KeyBuff = string.Format("ST_{0}_Kor", a_ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Kor);
            a_KeyBuff = string.Format("ST_{0}_Eng", a_ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Eng);
            a_KeyBuff = string.Format("ST_{0}_Math", a_ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Math);
            a_KeyBuff = string.Format("ST_{0}_Total", a_ii);
            PlayerPrefs.SetInt(a_KeyBuff, a_Node.m_Total);
            a_KeyBuff = string.Format("ST_{0}_Avg", a_ii);
            PlayerPrefs.SetFloat(a_KeyBuff, a_Node.m_Avg);
        }
    }

    void LoadList()
    {
        int a_StCount = PlayerPrefs.GetInt("St_Count", 0);

        if (a_StCount <= 0)
            return;

        Student a_Node;
        string a_KeyBuff = "";
        for (int a_ii = 0; a_ii < a_StCount; a_ii++)
        {
            a_Node = new Student();
            a_KeyBuff = string.Format("ST_{0}_Name", a_ii);
            a_Node.m_Name = PlayerPrefs.GetString(a_KeyBuff, "");
            a_KeyBuff = string.Format("ST_{0}_Kor", a_ii);
            a_Node.m_Kor = PlayerPrefs.GetInt(a_KeyBuff, 0);
            a_KeyBuff = string.Format("ST_{0}_Eng", a_ii);
            a_Node.m_Eng = PlayerPrefs.GetInt(a_KeyBuff, 0);
            a_KeyBuff = string.Format("ST_{0}_Math", a_ii);
            a_Node.m_Math = PlayerPrefs.GetInt(a_KeyBuff, 0);
            a_KeyBuff = string.Format("ST_{0}_Total", a_ii);
            a_Node.m_Total = PlayerPrefs.GetInt(a_KeyBuff, 0);
            a_KeyBuff = string.Format("ST_{0}_Avg", a_ii);
            a_Node.m_Avg = PlayerPrefs.GetFloat(a_KeyBuff, 0.0f);

            std_List.Add(a_Node);
        }
    }

    void RefreshUIList(List<Student> a_stdList)
    {
        PrintList.text = "";

        if (a_stdList.Count <= 0)
            return;

        string a_StrBuff = "";
        for (int ii = 0; ii < a_stdList.Count; ii++)
        {
            a_StrBuff += string.Format(
                "{0} : 국어({1}) 영어({2}) 수학({3}) 총점({4}) 평균({5:N2})",
                a_stdList[ii].m_Name,
                a_stdList[ii].m_Kor,
                a_stdList[ii].m_Eng,
                a_stdList[ii].m_Math,
                a_stdList[ii].m_Total,
                a_stdList[ii].m_Avg);

            a_StrBuff += "\n";  //"\n"  줄바꿈 기호(엔터 역할)
        }//for(int ii = 0; ii < a_stdList.Count; ii++)

        PrintList.text = a_StrBuff;
    }//void RefreshUIList(List<Student> a_stdList)

    string m_FName = "";
    bool FindSt(Student x)
    {
        //if (x.m_Name == m_FName)
        //    return true;
        //else
        //    return false;

        return (x.m_Name == m_FName);
    }

    void FindBtnFunc() //학생검색 버튼 클릭시
    {
        if (StName_IFd == null)
            return;

        string a_StName = StName_IFd.text.Trim();

        if (string.IsNullOrEmpty(a_StName) == true)
            return;  //예외처리

        //m_FName = a_StName;
        //m_FIndex = std_List.FindIndex(FindSt);
        //m_FIndex = std_List.FindIndex(
        //                       (x) =>
        //                       {
        //                           return (x.m_Name == a_StName);
        //                       } 
        //); //람다식 표현 (무명함수)
        m_FIndex = std_List.FindIndex(x => x.m_Name == a_StName); //람다식을 줄인 표현
        if (m_FIndex < 0)  //a_StName 이름의 학생이 리스트에 존재하지 않는다는 뜻
        {
            if (AddMd_InfoLabel != null)
                AddMd_InfoLabel.gameObject.SetActive(true);
            AddMd_InfoLabel.text = "찾는 이름이 존재하지 않습니다.";
            m_ShowTime = 3.0f;
            return;
        }

        Student a_FNode = std_List[m_FIndex];

        //Debug.Log("학생검색 버튼 클릭시");
        if (AddModeRoot != null)
            AddModeRoot.SetActive(false);  //체크 끄기

        if (DelModeRoot != null)
            DelModeRoot.SetActive(true);   //체크 켜기

        //------------------- 검색 학생 정보 출력
        string a_StrBuff = string.Format("{0} : 국어({1}) 영어({2}) 수학({3}) 총점({4}) 평균({5:N2})",
           a_FNode.m_Name, a_FNode.m_Kor, a_FNode.m_Eng, a_FNode.m_Math,
           a_FNode.m_Total, a_FNode.m_Avg);

        DelMdFindInfo.text = a_StrBuff;
        //------------------- 검색 학생 정보 출력
    }

    void SortBtnFunc() //총점순 정렬 버튼 클릭시
    {
        //Debug.Log("총점순 정렬 버튼 클릭시");
        // std_List <-- 학생 리스트의 복사본 만들기...
        List<Student> a_CopyList = std_List.ToList(); //리스트 복사 using System.Linq;
        a_CopyList.Sort(CompTotal); //정렬
        RefreshUIList(a_CopyList);
    }

    void ShowListBtnFunc() //추가순 보기 버튼 클릭시
    {
        //Debug.Log("추가순 보기 버튼 클릭시");
        RefreshUIList(std_List);
    }

    void BackBtnFunc()
    {
        if (DelModeRoot != null)
            DelModeRoot.SetActive(false); //체크 끄기

        if (AddModeRoot != null)
            AddModeRoot.SetActive(true);  //체크 켜기
    }

    void DelBtnFunc()
    {
        if (m_FIndex < 0)
            return;

        std_List.RemoveAt(m_FIndex); //노드 삭제

        m_FIndex = -1;
        DelMdFindInfo.text = "";
        SaveList();
        RefreshUIList(std_List);
    }
}
