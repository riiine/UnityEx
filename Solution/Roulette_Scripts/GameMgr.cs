using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState //������ ��ſ� ���Ǵ� ��Ī
{
    PowerIng = 0,    //�Ŀ��� ���� ��
    RotateIng = 1,   //�귿�� ���� �ִ� ��
    GameEnd = 2      //���� ���� ���¸� �ǹ���
}

public class GameMgr : MonoBehaviour
{
    static public GameState s_State = GameState.PowerIng;

    int GameCount = 0;  //�ζ� ��ȣ �ε��� ī��Ʈ�� ����
    public Text[] NumberTexts;   //�ζ� ��ȣ ǥ�� UI ����� ����

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
                //���� ����
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
