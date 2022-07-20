using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; // 회전 속도
    float m_MaxPower = 80.0f; //최대 속도를 위하 최대 힘

    public Image m_PwBarImg = null;
    public Text m_PwText = null;

    bool IsRotIng = false; //상태값 false(룰렛정지상태) true(회전상태)

    GameMgr m_GameMgr = null;

    // Start is called before the first frame update
    void Start()
    {
        //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        Application.targetFrameRate = 60; 
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터
        //조작시 빠르게 움직일 수 있다.

        GameObject a_GObj = GameObject.Find("GameMgr");
        if (a_GObj != null)
            m_GameMgr = a_GObj.GetComponent<GameMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        // 클릭하면 회전 속도를 설정한다.

        if (GameMgr.s_State == GameState.PowerIng) //if (IsRotIng == false) 
        {//룰렛이 멈춰 있을 때만
            if (Input.GetMouseButton(0) == true)
            {  //마우스를 누르고 있는 동안 반응하는 함수
               //인수가 0이면 왼쪽 클릭, 1이면 오른쪽 클릭, 2이면 가운데 버튼 클릭

                //마우스가 UI를 위에 있다면...
                if (IsPointerOverUIObject() == true) 
                    return;

                this.rotSpeed += (Time.deltaTime * 50.0f);
                //Time.deltaTime : 이전 Update()에서 이번 Update()가
                //호출될 때까지 걸린 시간
                if (m_MaxPower < rotSpeed)
                    rotSpeed = m_MaxPower;
            }//if (Input.GetMouseButton(0) == true)

            if(Input.GetMouseButtonUp(0) == true)
            { //마우스를 누르고 있다가 손가락을 떼는 순간
                GameMgr.s_State = GameState.RotateIng; //IsRotIng = true;
            }
        }
        else if (GameMgr.s_State == GameState.RotateIng) // if (IsRotIng == true) 
        { //룰렛이 돌아가고 있을 때라는 의미

            // 회전 속도만큼 룰렛을 회전시킨다.
            transform.Rotate(0, 0, this.rotSpeed);

            // 룰렛을 감속시킨다.
            this.rotSpeed *= 0.98f;

            if(rotSpeed <= 0.1f) //룰렛이 멈춘 상태로 판단하겠다는 뜻
            {
                rotSpeed = 0.0f;
                GameMgr.s_State = GameState.PowerIng; //IsRotIng = false;
                //파워를 다시 당길 수 있는 상태로 만들어 놓는다.

                FindCurNumber();

            }//if(rotSpeed <= 0.1f)

        }//else  // if (IsRotIng == true) 룰렛이 돌아가고 있을 때라는 의미

        m_PwBarImg.fillAmount = rotSpeed / m_MaxPower;  // 0.0f ~ 1.0f
        m_PwText.text = (int)(m_PwBarImg.fillAmount * 100.0f) + " / 100";

    }// void Update()

    void FindCurNumber()
    {
        //---- 현재 룰렛의 z축 회전 각도 얻어오기
        float a_Angle = transform.eulerAngles.z; //0 ~ 360
        if (a_Angle < 0.0f) //각도가 마이너스값이 나오는 경우의 예외처리
            a_Angle += 360.0f; //이 부분은 생략 가능함
        //어차피 transform.eulerAngles.z 값은 0 ~ 360 값만 나오니까...

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
        //--- 현재 룰렛의 z축 회전 각도 얻어오기

        //Debug.Log(num);
        if (m_GameMgr != null)
            m_GameMgr.SetNumber(num);

    }//void FindCurNumber()

    PointerEventData a_EDCurPos; // using UnityEngine.EventSystems;
    public bool IsPointerOverUIObject() 
    {   //마우스가 UI를 위에 있는지? 아닌지? 를 확인 하는 함수
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
