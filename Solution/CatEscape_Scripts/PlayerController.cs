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
    //이번 플레임에 캐릭터가 움직여야 할 x거리를 구하기 위한 변수
    //속도 = 거리 / 시간;   속도 * 시간 = 거리;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우
        ////캐릭터 조작시 빠르게 움직일 수 있다.

        //if (m_R_Btn != null)
        //    m_R_Btn.onClick.AddListener(RButton);

        //if (m_L_Btn != null)
        //    m_L_Btn.onClick.AddListener(LButton);

        //-----------Right Button 처리 부분
        // Inspector에서 GameObject.Find("Button");
        // 에 꼭 AddComponent--> EventTrigger 가 되어 있어야 한다.
        EventTrigger a_trigger = m_R_Btn.GetComponent<EventTrigger>();
        EventTrigger.Entry a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerDown;
        a_entry.callback.AddListener((data) => { OnRBtnDown((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);

        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerUp;
        a_entry.callback.AddListener((data) => { OnRBtnUp((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);
        //-----------Right Button 처리 부분 

        //-----------Left Button 처리 부분
        // Inspector에서 GameObject.Find("Button"); 에 꼭
        // AddComponent--> EventTrigger 가 되어 있어야 한다.
        a_trigger = m_L_Btn.GetComponent<EventTrigger>(); 
        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerDown;
        a_entry.callback.AddListener((data) => { OnLBtnDown((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);

        a_entry = new EventTrigger.Entry();
        a_entry.eventID = EventTriggerType.PointerUp;
        a_entry.callback.AddListener((data) => { OnLBtnUp((PointerEventData)data); });
        a_trigger.triggers.Add(a_entry);
        //-----------Left Button 처리 부분
    }

    // Update is called once per frame
    void Update()
    {
        // 왼쪽 화살표가 눌렸을 때
        if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)
            || isLBtnDown == true )
        {
            //transform.Translate(-3, 0, 0); //왼쪽으로 [3] 움직인다.
            //속도 : -7.0f
            m_MvX = Time.deltaTime * -7.0f;
            transform.Translate(m_MvX, 0, 0);
        }

        // 오른쪽 화살표가 눌렸을 때
        if ( Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)
            || isRBtnDown == true )
        {
            //transform.Translate(3, 0, 0); //오른쪽으로 [3] 움직인다.
            //속도 : 7.0f
            m_MvX = Time.deltaTime * 7.0f;
            transform.Translate(m_MvX, 0, 0);
        }

        //-- 캐릭터가 게임 배경 화면을 벗어나지 못하게 막는 처리
        Vector3 a_vPos = transform.position;
        if (8.0f < a_vPos.x)
            a_vPos.x = 8.0f;

        if (a_vPos.x < -8.0f)
            a_vPos.x = -8.0f;
        transform.position = a_vPos;
        //-- 캐릭터가 게임 배경 화면을 벗어나지 못하게 막는 처리

    }//void Update()

    public void LButton()
    {
        transform.Translate(-3, 0, 0);
    }

    public void RButton()
    {
        transform.Translate(3, 0, 0);
    }

    //버튼이 눌려질 때 한번 발생되는 함수 
    public void OnRBtnDown(PointerEventData a_data)
    {
        //마우스 왼쪽 버튼 누르고 있는 동안에만 발동되게...
        if (a_data.button == PointerEventData.InputButton.Left)
            isRBtnDown = true;
    }

    //버튼을 뗄때 한번 발생되는 함수 
    public void OnRBtnUp(PointerEventData a_data)
    {
        //마우스 왼쪽 버튼 누르고 있는 동안에만 발동되게...
        if (a_data.button == PointerEventData.InputButton.Left)
            isRBtnDown = false;
    }

    //버튼이 눌려질 때 한번 발생되는 함수 
    public void OnLBtnDown(PointerEventData a_data)
    {
        //마우스 왼쪽 버튼 누르고 있는 동안에만 발동되게...
        if (a_data.button == PointerEventData.InputButton.Left)
            isLBtnDown = true;
    }

    //버튼을 뗄때 한번 발생되는 함수 
    public void OnLBtnUp(PointerEventData a_data)
    {
        //마우스 왼쪽 버튼 누르고 있는 동안에만 발동되게...
        if (a_data.button == PointerEventData.InputButton.Left)
            isLBtnDown = false;
    }

}//public class PlayerController
