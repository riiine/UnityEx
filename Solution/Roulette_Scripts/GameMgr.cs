using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState //정수값 대신에 사용되는 별칭
{
    PowerIng = 0,    //파워를 당기는 중
    RotateIng = 1,   //룰렛이 돌고 있는 중
    GameEnd = 2      //게임 종료 상태를 의미함
}

public class GameMgr : MonoBehaviour
{
    static public GameState s_State = GameState.PowerIng;

    int GameCount = 0;  //로또 번호 인덱스 카운트용 변수
    public Text[] NumberTexts;   //로또 번호 표시 UI 연결용 변수

    public Button Btn_Reset = null;

    // Start is called before the first frame update
    void Start()
    {
        s_State = GameState.PowerIng;
        if (Btn_Reset != null)
            Btn_Reset.onClick.AddListener(ResetFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNumber(int a_Num)
    {
        if(GameCount < NumberTexts.Length)
        {
            NumberTexts[GameCount].text = a_Num.ToString();
            GameCount++;

            if(NumberTexts.Length <= GameCount)
            {
                //게임 종료
                s_State = GameState.GameEnd;
            }
        }//if(GameCount < NumberTexts.Length)
    }//public void SetNumber(int a_Num)

    public void ResetFunc()
    {
        s_State = GameState.GameEnd;
        SceneManager.LoadScene("GameScene");
    }

}
