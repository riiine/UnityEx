using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CarController : MonoBehaviour
{

    float speed = 0;
    Vector2 startPos;

    GameDirector m_GameDirector;

    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.
        GameObject a_GObj = GameObject.Find("GameDirector");
        if (a_GObj != null)
            m_GameDirector = a_GObj.GetComponent<GameDirector>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.s_State == GameState.GameEnd)
            return;
        
        //if (Input.GetMouseButtonDown(0)) //���콺�� Ŭ���ϸ�
        //{
        //    this.speed = 0.2f;           // ó�� �ӵ��� ����
        //}
        // �������� ���̸� ���Ѵ�.

        //Ŭ���ϸ� �ڵ����� �ӵ��� �����Ѵ�.

        if (GameDirector.s_State == GameState.PowerIng)
        {//�ڵ����� ���� ���� ����
            if (Input.GetMouseButtonDown(0))
            {
                // ���콺 ���߸� Ŭ���� ��ǥ
                this.startPos = Input.mousePosition;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                //���콺 ��ư���� �հ����� ������ �� ��ǥ
                Vector2 endPos = Input.mousePosition;
                float swipeLength = (endPos.x - this.startPos.x);

                //�������� ���̸� ó�� �ӵ��� �����Ѵ�.
                this.speed = swipeLength / 500.0f;

                //ȿ������ ���
                GetComponent<AudioSource>().Play();
                GameDirector.s_State = GameState.RotateIng;
            }
        }

        transform.Translate(this.speed, 0, 0);    // �̵�
        this.speed *= 0.98f;        // ����

        if (GameDirector.s_State == GameState.RotateIng) 
        { //�ڵ����� �޸��� ���� ����� �ǹ�
            if (speed <= 0.001f) //�ڵ����� ���� ���·� �Ǵ��ϰڴٴ� ��
            { 
                this.speed = 0.0f;
                this.transform.position = new Vector3(-7.15f,-3.1f,0.0f);

                //�ڵ����� �ٽ� ��߽�ų �� �ִ� ���·� ����� ���´�.
                GameDirector.s_State = GameState.PowerIng; 
                                                           
                if (m_GameDirector != null)
                    m_GameDirector.SetNumber();
            }
        }
    }
}
