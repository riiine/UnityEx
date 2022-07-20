using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GameState //������ ��ſ� ���Ǵ� ��Ī
{ 
    PowerIng = 0,    //�ڵ����� ���� ��
    RotateIng = 1,   //�ڵ����� �޸����ִ� ��
    GameEnd = 2      //���� ���� ���¸� �ǹ���
}

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;

    public static GameState s_State = GameState.PowerIng;

    int GameCount = 0; //���� ���� Ƚ�� ī��Ʈ�� ����

    public Text[] NumberTexts; // ������ �÷��̾� ǥ�� UI ����� ����
    public float m_distance; // ������ �÷��̾ �� �Ÿ� ����� ����
    float[] m_playerDistance = new float[3]; // �迭�� ����

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
            "��ǥ ��������" + length.ToString("F2") + "m";

        m_distance = length;
    }

    public void SetNumber() //���� ���� Ƚ���� ���� �Լ�
    {
        if(m_distance < 0) // �Ÿ��� 0���� ������ �������� ó���Ѵ�.
            m_distance = Mathf.Abs(m_distance);
        
        if (GameCount < NumberTexts.Length) //���� ���� Ƚ���� 3ȸ �̸��� ���
        {
            NumberTexts[GameCount].text =
                "Player " + (GameCount + 1).ToString() + " : " + m_distance.ToString("F2") + "m";
            m_playerDistance[GameCount] = m_distance; //�Ÿ��� �÷��̾�� ����

        }
        GameCount++;

        if (GameCount >= NumberTexts.Length) // ���� ���� Ƚ���� 3ȸ �̻��� ���
        {
            //���� ����
            s_State = GameState.GameEnd;

            //�÷��̾���� ���� �����ϱ�
            int[] rankings = Enumerable.Repeat(1, 3).ToArray(); //��� 1�� �ʱ�ȭ

            for (int i = 0; i < m_playerDistance.Length; i++)
            {
                rankings[i] = 1; //1������ �ʱ�ȭ, ���� �迭�� �� ȸ������ 1������ �ʱ�ȭ
                for (int j = 0; j < m_playerDistance.Length; j++)
                {
                    if (m_playerDistance[i] > m_playerDistance[j]) //����(i)�� ������(j) ��
                    {
                        rankings[i]++;         //RANK: ������ ū ������ ������ ���� 1����
                    }
                }
            }
            for (int i = 0; i < m_playerDistance.Length; i++)
            {

                NumberTexts[i].text =
                "Player " + (i + 1).ToString() + " : " + m_playerDistance[i].ToString("F2") + "m   " + rankings[i].ToString() + "��";
            }

        }
        
    }

    public void ResetFunc()
    {
        s_State = GameState.GameEnd;
        SceneManager.LoadScene("GameScene");
        
        //0.001�� �Ŀ� �ڵ����� �����·� �������´�.
        Invoke("PowerIng", 0.001f);
    }
    void PowerIng()
    {
        s_State = GameState.PowerIng;
    }

}






