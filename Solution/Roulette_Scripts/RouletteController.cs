using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; // ȸ�� �ӵ�
    float m_MaxPower = 80.0f; //�ִ� �ӵ��� ���� �ִ� ��

    public Image m_PwBarImg = null;
    public Text m_PwText = null;

    bool IsRotIng = false; //���°� false(�귿��������) true(ȸ������)

    GameMgr m_GameMgr = null;

    // Start is called before the first frame update
    void Start()
    {
        //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        Application.targetFrameRate = 60; 
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ����
        //���۽� ������ ������ �� �ִ�.

        GameObject a_GObj = GameObject.Find("GameMgr");
        if (a_GObj != null)
            m_GameMgr = a_GObj.GetComponent<GameMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ŭ���ϸ� ȸ�� �ӵ��� �����Ѵ�.

        if (GameMgr.s_State == GameState.PowerIng) //if (IsRotIng == false) 
        {//�귿�� ���� ���� ����
            if (Input.GetMouseButton(0) == true)
            {  //���콺�� ������ �ִ� ���� �����ϴ� �Լ�
               //�μ��� 0�̸� ���� Ŭ��, 1�̸� ������ Ŭ��, 2�̸� ��� ��ư Ŭ��

                //���콺�� UI�� ���� �ִٸ�...
                if (IsPointerOverUIObject() == true) 
                    return;

                this.rotSpeed += (Time.deltaTime * 50.0f);
                //Time.deltaTime : ���� Update()���� �̹� Update()��
                //ȣ��� ������ �ɸ� �ð�
                if (m_MaxPower < rotSpeed)
                    rotSpeed = m_MaxPower;
            }//if (Input.GetMouseButton(0) == true)

            if(Input.GetMouseButtonUp(0) == true)
            { //���콺�� ������ �ִٰ� �հ����� ���� ����
                GameMgr.s_State = GameState.RotateIng; //IsRotIng = true;
            }
        }
        else if (GameMgr.s_State == GameState.RotateIng) // if (IsRotIng == true) 
        { //�귿�� ���ư��� ���� ����� �ǹ�

            // ȸ�� �ӵ���ŭ �귿�� ȸ����Ų��.
            transform.Rotate(0, 0, this.rotSpeed);

            // �귿�� ���ӽ�Ų��.
            this.rotSpeed *= 0.98f;

            if(rotSpeed <= 0.1f) //�귿�� ���� ���·� �Ǵ��ϰڴٴ� ��
            {
                rotSpeed = 0.0f;
                GameMgr.s_State = GameState.PowerIng; //IsRotIng = false;
                //�Ŀ��� �ٽ� ��� �� �ִ� ���·� ����� ���´�.

                FindCurNumber();

            }//if(rotSpeed <= 0.1f)

        }//else  // if (IsRotIng == true) �귿�� ���ư��� ���� ����� �ǹ�

        m_PwBarImg.fillAmount = rotSpeed / m_MaxPower;  // 0.0f ~ 1.0f
        m_PwText.text = (int)(m_PwBarImg.fillAmount * 100.0f) + " / 100";

    }// void Update()

    void FindCurNumber()
    {
        //---- ���� �귿�� z�� ȸ�� ���� ������
        float a_Angle = transform.eulerAngles.z; //0 ~ 360
        if (a_Angle < 0.0f) //������ ���̳ʽ����� ������ ����� ����ó��
            a_Angle += 360.0f; //�� �κ��� ���� ������
        //������ transform.eulerAngles.z ���� 0 ~ 360 ���� �����ϱ�...

        int num = 0;
        if (0 <= a_Angle && a_Angle < 18.0f)
            num = 7;
        else if (18.0f <= a_Angle && a_Angle < 54.0f)
            num = 8;
        else if (54.0f <= a_Angle && a_Angle < 90.0f)
            num = 9;
        else if (90.0f <= a_Angle && a_Angle < 126.0f)
            num = 0;
        else if (126.0f <= a_Angle && a_Angle < 162.0f)
            num = 1;
        else if (162.0f <= a_Angle && a_Angle < 198.0f)
            num = 2;
        else if (198.0f <= a_Angle && a_Angle < 234.0f)
            num = 3;
        else if (234.0f <= a_Angle && a_Angle < 270.0f)
            num = 4;
        else if (270.0f <= a_Angle && a_Angle < 306.0f)
            num = 5;
        else if (306.0f <= a_Angle && a_Angle < 342.0f)
            num = 6;
        else if (342.0f <= a_Angle)
            num = 7;
        //--- ���� �귿�� z�� ȸ�� ���� ������

        //Debug.Log(num);
        if (m_GameMgr != null)
            m_GameMgr.SetNumber(num);

    }//void FindCurNumber()

    PointerEventData a_EDCurPos; // using UnityEngine.EventSystems;
    public bool IsPointerOverUIObject() 
    {   //���콺�� UI�� ���� �ִ���? �ƴ���? �� Ȯ�� �ϴ� �Լ�
        a_EDCurPos = new PointerEventData(EventSystem.current);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)

			List<RaycastResult> results = new List<RaycastResult>();
			for (int i = 0; i < Input.touchCount; ++i)
			{
				a_EDCurPos.position = Input.GetTouch(i).position;  
				results.Clear();
				EventSystem.current.RaycastAll(a_EDCurPos, results);
                if (0 < results.Count)
                    return true;
			}

			return false;
#else
        a_EDCurPos.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(a_EDCurPos, results);
        return (0 < results.Count);
#endif
    }//public bool IsPointerOverUIObject() 

}
