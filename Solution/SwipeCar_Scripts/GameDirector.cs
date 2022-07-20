using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Ready = 0,
    MoveIng = 1,
    GameEnd = 2
}

public class CarInfo
{
    public int m_PyIndex = 0;      //자동차 유저 번호
    public float m_SvLen = 0.0f;   //각 유저의 기록  
    public Text m_PlayerUI = null; //각 유저의 Text UI 의 레퍼런스
}

public class GameDirector : MonoBehaviour
{
    public static GameState s_State = GameState.Ready;

    GameObject car;
    GameObject flag;
    GameObject distance;

    public Text[] PlayerUI;

    int PlayCount = 0;
    //0 일때 Player_1 이 플레이 중, 
    //1 일때 Player_2 이 플레이 중,
    //2 일때 Player_3 이 플레이 중   의미로 사용할 변수

    Text ResultTxt;
    float m_Length = 0.0f; //거리 저장용 변수

    // float[] m_SvLen = new float[3];
    //각 플레이어 별로 이동 거리를 저장하고 랭킹 정렬을 위한 리스트
    List<CarInfo> m_CarList = new List<CarInfo>();

    public Button ReSetBtn;

    //검색 단축키 : Ctrl + < 

    // Start is called before the first frame update
    void Start()
    {
        s_State = GameState.Ready;

        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.distance = GameObject.Find("Distance");
        ResultTxt = this.distance.GetComponent<Text>();

        if (ReSetBtn != null)
            ReSetBtn.onClick.AddListener(ReSetMethod);
    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flag.transform.position.x - 
                            this.car.transform.position.x;

        length = Mathf.Abs(length); //절대값 함수

        if (ResultTxt != null)
            ResultTxt.text = "목표 지점까지 " + length.ToString("F2") + "m";

        m_Length = length;
    }

    //-- 각 플레이어가 멈추면 기록을 화면에 표시하고 저장해 놓기 위한 함수
    public void RecordLength()
    {
        if (PlayCount < PlayerUI.Length)
        {
            PlayerUI[PlayCount].text =
                "Player " + (PlayCount + 1).ToString() +
                " : " + m_Length.ToString("F2") + "m";

            CarInfo a_CarNode = new CarInfo();
            a_CarNode.m_PyIndex  = PlayCount;
            a_CarNode.m_SvLen    = m_Length;
            a_CarNode.m_PlayerUI = PlayerUI[PlayCount];
            m_CarList.Add(a_CarNode);

            PlayCount++;
        }//if (PlayCount < PlayerUI.Length)

        CheckGameEnd();
    }//public void RecordLength()

    public void CheckGameEnd()
    {
        if (3 <= PlayCount) //게임 종료 조건
        {
            s_State = GameState.GameEnd;

            RankingAlgorithm();

            if (ReSetBtn != null)
                ReSetBtn.gameObject.SetActive(true);  //리셋 버튼 활성화
        }//게임 종료 조건 판단
    }//public void CheckGameEnd()

    int CompLen(CarInfo a, CarInfo b)
    {   // 정렬 조건 함수
        return a.m_SvLen.CompareTo(b.m_SvLen); //오름차순 정렬 1, 2, 3, 4, ... 
    }

    void RankingAlgorithm()
    {
        if (m_CarList.Count != 3)
            return;

        m_CarList.Sort( CompLen );

        int a_RankCount = 1;
        for (int ii = 0; ii < m_CarList.Count; ii++)
        {
            m_CarList[ii].m_PlayerUI.text =
                "Player " + (m_CarList[ii].m_PyIndex + 1).ToString() + 
                " : " +
                m_CarList[ii].m_SvLen.ToString("F2") + "m  " +
                a_RankCount.ToString() + " 등";
            a_RankCount++;
        }

    }//void RankingAlgorithm()

    void ReSetMethod()
    {
        SceneManager.LoadScene("GameScene");
    }
}
