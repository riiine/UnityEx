using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    GameReady,
    GameIng,
    GameOver
}

public class GameMgr : MonoBehaviour
{
    [HideInInspector] public GameState gameState = GameState.GameReady;
    [HideInInspector] public int DiffLevel = 0; //Difficult

    GameObject player;

    public Text HeightTxt;
    public Text BestHeightTxt;

    float m_CurHeight = 0.0f;      //�������
    [HideInInspector] public float m_CurStageHeight = 0.0f; //�� �������������� �ְ����
    [HideInInspector] public float m_BestHeight = 0.0f;     //�ְ��ϳ���

    public GameObject m_GameOverPanel;

    public GameObject readyPanel;
    public Text countTxt;
    float  countDown = 4.0f;

    public static GameMgr Inst = null; // GameManager�� Instance ����

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f; //�Ͻ����� Ǯ���ֱ�...
        gameState = GameState.GameReady;   //GameState.GameIng;

        this.player = GameObject.Find("cat");
        m_CurStageHeight = 0.0f;

        Load();
    }

    // Update is called once per frame
    void Update()
    { // Ctrl + < : �˻� ����Ű
        if (gameState == GameState.GameReady)
        {
            countDown -= Time.deltaTime;
            if (0 <= countDown)
                countTxt.text = ((int)countDown).ToString();
            else if (-1.0f <= countDown)
                countTxt.text = "Start!";
            else
            {
                gameState = GameState.GameIng;
                readyPanel.SetActive(false);
            }
        } //if (gameState == GameState.GameReady)


        if (gameState == GameState.GameOver)
            return;

        DiffLevel = (int)(player.transform.position.y / 15.0f);

        //--- ���̰� ���
        m_CurHeight = player.transform.position.y;

        if (m_CurHeight < 0)
            m_CurHeight = 0.0f;

        if (m_CurStageHeight < m_CurHeight)
            m_CurStageHeight = m_CurHeight;

        if (m_BestHeight < m_CurStageHeight)
        {
            m_BestHeight = m_CurStageHeight;
            Save();
        }

        if (HeightTxt != null)
            HeightTxt.text = string.Format("���� : {0:N}", m_CurStageHeight);

        if (BestHeightTxt != null)
            BestHeightTxt.text = string.Format("�ְ��� : {0:N}", m_BestHeight);
        //--- ���̰� ���
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("HighScore", m_BestHeight);
    }

    public void Load()
    {
        m_BestHeight = PlayerPrefs.GetFloat("HighScore", 0.0f);
    }

    public void GameOver()
    {
        if (m_BestHeight < m_CurStageHeight)
        {
            m_BestHeight = m_CurStageHeight;
            Save();
        }

        gameState = GameState.GameOver;

        Time.timeScale = 0.0f; //�Ͻ�����
        if (m_GameOverPanel != null)
            m_GameOverPanel.SetActive(true);
    }
}
