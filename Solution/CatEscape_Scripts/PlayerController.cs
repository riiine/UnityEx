using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Button m_R_Btn;
    public Button m_L_Btn;

    bool isRBtnDown = false;
    bool isLBtnDown = false;

    float m_MvX = 0.0f; 
    //�̹� �÷��ӿ� ĳ���Ͱ� �������� �� x�Ÿ��� ���ϱ� ���� ����
    //�ӵ� = �Ÿ� / �ð�;   �ӵ� * �ð� = �Ÿ�;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ���
        ////ĳ���� ���۽� ������ ������ �� �ִ�.

        //if (m_R_Btn != null)
        //    m_R_Btn.onClick.AddListener(RButton);

        //if (m_L_Btn != null)
        //    m_L_Btn.onClick.AddListener(LButton);

        //-----------Right Button ó�� �κ�
        // Inspector���� GameObject.Find("Button");
        // �� �� AddComponent--> EventTrigger �� �Ǿ� �־�� �Ѵ�.
        EventTrigger a_trigger = m_R_Btn.GetComponent<EventTrigger>();
        EventTrigger.Entry a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerDown;
        a_entry.callback.AddListener((data) => { OnRBtnDown((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);

        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerUp;
        a_entry.callback.AddListener((data) => { OnRBtnUp((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);
        //-----------Right Button ó�� �κ� 

        //-----------Left Button ó�� �κ�
        // Inspector���� GameObject.Find("Button"); �� ��
        // AddComponent--> EventTrigger �� �Ǿ� �־�� �Ѵ�.
        a_trigger = m_L_Btn.GetComponent<EventTrigger>(); 
        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerDown;
        a_entry.callback.AddListener((data) => { OnLBtnDown((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);

        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerUp;
        a_entry.callback.AddListener((data) => { OnLBtnUp((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);
        //-----------Left Button ó�� �κ�
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ȭ��ǥ�� ������ ��
        if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)
            || isLBtnDown == true )
        {
            //transform.Translate(-3, 0, 0); //�������� [3] �����δ�.
            //�ӵ� : -7.0f
            m_MvX = Time.deltaTime * -7.0f;
            transform.Translate(m_MvX, 0, 0);
        }

        // ������ ȭ��ǥ�� ������ ��
        if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)
            || isRBtnDown == true )
        {
            //transform.Translate(3, 0, 0); //���������� [3] �����δ�.
            //�ӵ� : 7.0f
            m_MvX = Time.deltaTime * 7.0f;
            transform.Translate(m_MvX, 0, 0);
        }

        //-- ĳ���Ͱ� ���� ��� ȭ���� ����� ���ϰ� ���� ó��
        Vector3 a_vPos = transform.position;
        if (8.0f < a_vPos.x)
            a_vPos.x = 8.0f;

        if (a_vPos.x < -8.0f)
            a_vPos.x = -8.0f;
        transform.position = a_vPos;
        //-- ĳ���Ͱ� ���� ��� ȭ���� ����� ���ϰ� ���� ó��

    }//void Update()

    public void LButton()
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButton()
    {
        transform.Translate(3, 0, 0);
    }

    //��ư�� ������ �� �ѹ� �߻��Ǵ� �Լ� 
    public void OnRBtnDown(PointerEventData a_data)
    {
        //���콺 ���� ��ư ������ �ִ� ���ȿ��� �ߵ��ǰ�...
        if (a_data.button == PointerEventData.InputButton.Left)
            isRBtnDown = true;
    }

    //��ư�� ���� �ѹ� �߻��Ǵ� �Լ� 
    public void OnRBtnUp(PointerEventData a_data)
    {
        //���콺 ���� ��ư ������ �ִ� ���ȿ��� �ߵ��ǰ�...
        if (a_data.button == PointerEventData.InputButton.Left)
            isRBtnDown = false;
    }

    //��ư�� ������ �� �ѹ� �߻��Ǵ� �Լ� 
    public void OnLBtnDown(PointerEventData a_data)
    {
        //���콺 ���� ��ư ������ �ִ� ���ȿ��� �ߵ��ǰ�...
        if (a_data.button == PointerEventData.InputButton.Left)
            isLBtnDown = true;
    }

    //��ư�� ���� �ѹ� �߻��Ǵ� �Լ� 
    public void OnLBtnUp(PointerEventData a_data)
    {
        //���콺 ���� ��ư ������ �ִ� ���ȿ��� �ߵ��ǰ�...
        if (a_data.button == PointerEventData.InputButton.Left)
            isLBtnDown = false;
    }

}//public class PlayerController
