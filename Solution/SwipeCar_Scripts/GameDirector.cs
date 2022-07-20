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
    public int m_PyIndex = 0;      //�ڵ��� ���� ��ȣ
    public float m_SvLen = 0.0f;   //�� ������ ���  
    public Text m_PlayerUI = null; //�� ������ Text UI �� ���۷���
}

public class GameDirector : MonoBehaviour
{
    public static GameState s_State = GameState.Ready;

    GameObject car;
    GameObject flag;
    GameObject distance;

    public Text[] PlayerUI;

    int PlayCount = 0;
    //0 �϶� Player_1 �� �÷��� ��, 
    //1 �϶� Player_2 �� �÷��� ��,
    //2 �϶� Player_3 �� �÷��� ��   �ǹ̷� ����� ����

    Text ResultTxt;
    float m_Length = 0.0f; //�Ÿ� ����� ����

    // float[] m_SvLen = new float[3];
    //�� �÷��̾� ���� �̵� �Ÿ��� �����ϰ� ��ŷ ������ ���� ����Ʈ
    List<CarInfo> m_CarList = new List<CarInfo>();

    public Button ReSetBtn;

    //�˻� ����Ű : Ctrl + < 

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

        length = Mathf.Abs(length); //���밪 �Լ�

        if (ResultTxt != null)
            ResultTxt.text = "��ǥ �������� " + length.ToString("F2") + "m";

        m_Length = length;
    }

    //-- �� �÷��̾ ���߸� ����� ȭ�鿡 ǥ���ϰ� ������ ���� ���� �Լ�
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
        if (3 <= PlayCount) //���� ���� ����
        {
            s_State = GameState.GameEnd;

            RankingAlgorithm();

            if (ReSetBtn != null)
                ReSetBtn.gameObject.SetActive(true);  //���� ��ư Ȱ��ȭ
        }//���� ���� ���� �Ǵ�
    }//public void CheckGameEnd()

    int CompLen(CarInfo a, CarInfo b)
    {   // ���� ���� �Լ�
        return a.m_SvLen.CompareTo(b.m_SvLen); //�������� ���� 1, 2, 3, 4, ... 
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
                a_RankCount.ToString() + " ��";
            a_RankCount++;
        }

    }//void RankingAlgorithm()

    void ReSetMethod()
    {
        SceneManager.LoadScene("GameScene");
    }
}
