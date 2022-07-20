using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float speed = 0;
    Vector2 startPos;

    GameDirector m_GDirector;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.

        GameObject a_GDObj = GameObject.Find("GameDirector");
        if (a_GDObj != null)
            m_GDirector = a_GDObj.GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.s_State == GameState.GameEnd)
            return;

        if (GameDirector.s_State == GameState.Ready)
        {
            // �������� ���̸� ���Ѵ�.
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

                // ȿ������ ���
                GetComponent<AudioSource>().Play();

                GameDirector.s_State = GameState.MoveIng;
            }//else if (Input.GetMouseButtonUp(0))
        }//if (GameDirector.s_State == GameState.Ready)
        else if (GameDirector.s_State == GameState.MoveIng)
        {
            transform.Translate(this.speed, 0, 0);  // �̵�
            this.speed *= 0.98f;            // ����

            if (this.speed < 0.0005f) //�ڵ��� ���� ����
            {
                this.speed = 0.0f;

                //----- ������ �÷��̾ ���� �ʱ�ȭ 
                this.transform.position = new Vector3(-7.0f, -3.7f, 0.0f);
                GameDirector.s_State = GameState.Ready;
                //----- ������ �÷��̾ ���� �ʱ�ȭ 

                if (m_GDirector != null)
                    m_GDirector.RecordLength(); 
                //���� �÷��̰� ���� ������ ����� ������ ��~

            }//if(this.speed < 0.0005f) //�ڵ��� ���� ����

        }//else if (GameDirector.s_State == GameState.MoveIng)

    }//void Update()
}//public class CarController
