using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GameState //정수값 대신에 사용되는 별칭
{ 
    PowerIng = 0,    //자동차를 당기는 중
    RotateIng = 1,   //자동차가 달리고있는 중
    GameEnd = 2      //게임 종료 상태를 의미함
}

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;

    public static GameState s_State = GameState.PowerIng;

    int GameCount = 0; //게임 진행 횟수 카운트용 변수

    public Text[] NumberTexts; // 각각의 플레이어 표시 UI 연결용 변수
    public float m_distance; // 각각의 플레이어가 간 거리 저장용 변수
    float[] m_playerDistance = new float[3]; // 배열에 저장

    public Button Btn_Reset;

    // Start is called before the first frame update
    void Start()
    {
        this.car = GameObject.Find("car");
        this.flag = GameObject.Find("flag");
        this.distance = GameObject.Find("Distance");

        if (Btn_Reset != null)
            Btn_Reset.onClick.AddListener(ResetFunc);
    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flag.transform.position.x -
                       this.car.transform.position.x;
        this.distance.GetComponent<Text>().text =
            "목표 지점까지" + length.ToString("F2") + "m";

        m_distance = length;
    }

    public void SetNumber() //게임 진행 횟수에 대한 함수
    {
        if(m_distance < 0) // 거리가 0보다 작으면 절댓값으로 처리한다.
            m_distance = Mathf.Abs(m_distance);
        
        if (GameCount < NumberTexts.Length) //게임 진행 횟수가 3회 미만일 경우
        {
            NumberTexts[GameCount].text =
                "Player " + (GameCount + 1).ToString() + " : " + m_distance.ToString("F2") + "m";
            m_playerDistance[GameCount] = m_distance; //거리를 플레이어마다 저장

        }
        GameCount++;

        if (GameCount >= NumberTexts.Length) // 게임 진행 횟수가 3회 이상일 경우
        {
            //게임 종료
            s_State = GameState.GameEnd;

            //플레이어들의 순위 판정하기
            int[] rankings = Enumerable.Repeat(1, 3).ToArray(); //모두 1로 초기화

            for (int i = 0; i < m_playerDistance.Length; i++)
            {
                rankings[i] = 1; //1등으로 초기화, 순위 배열을 매 회전마다 1등으로 초기화
                for (int j = 0; j < m_playerDistance.Length; j++)
                {
                    if (m_playerDistance[i] > m_playerDistance[j]) //현재(i)와 나머지(j) 비교
                    {
                        rankings[i]++;         //RANK: 나보다 큰 점수가 나오면 순위 1증가
                    }
                }
            }
            for (int i = 0; i < m_playerDistance.Length; i++)
            {

                NumberTexts[i].text =
                "Player " + (i + 1).ToString() + " : " + m_playerDistance[i].ToString("F2") + "m   " + rankings[i].ToString() + "등";
            }

        }
        
    }

    public void ResetFunc()
    {
        s_State = GameState.GameEnd;
        SceneManager.LoadScene("GameScene");
        
        //0.001초 후에 자동차를 대기상태로 만들어놓는다.
        Invoke("PowerIng", 0.001f);
    }
    void PowerIng()
    {
        s_State = GameState.PowerIng;
    }

}






