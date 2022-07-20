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

        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.
        GameObject a_GObj = GameObject.Find("GameDirector");
        if (a_GObj != null)
            m_GameDirector = a_GObj.GetComponent<GameDirector>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.s_State == GameState.GameEnd)
            return;
        
        //if (Input.GetMouseButtonDown(0)) //마우스를 클릭하면
        //{
        //    this.speed = 0.2f;           // 처음 속도를 설정
        //}
        // 스와이프 길이를 구한다.

        //클릭하면 자동차의 속도를 설정한다.

        if (GameDirector.s_State == GameState.PowerIng)
        {//자동차가 멈춰 있을 때만
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 단추를 클릭한 좌표
                this.startPos = Input.mousePosition;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                //마우스 버튼에서 손가락을 떼었을 때 좌표
                Vector2 endPos = Input.mousePosition;
                float swipeLength = (endPos.x - this.startPos.x);

                //스와이프 길이를 처음 속도로 변경한다.
                this.speed = swipeLength / 500.0f;

                //효과음을 재생
                GetComponent<AudioSource>().Play();
                GameDirector.s_State = GameState.RotateIng;
            }
        }

        transform.Translate(this.speed, 0, 0);    // 이동
        this.speed *= 0.98f;        // 감속

        if (GameDirector.s_State == GameState.RotateIng) 
        { //자동차가 달리고 있을 때라는 의미
            if (speed <= 0.001f) //자동차가 멈춘 상태로 판단하겠다는 뜻
            { 
                this.speed = 0.0f;
                this.transform.position = new Vector3(-7.15f,-3.1f,0.0f);

                //자동차를 다시 출발시킬 수 있는 상태로 만들어 놓는다.
                GameDirector.s_State = GameState.PowerIng; 
                                                           
                if (m_GameDirector != null)
                    m_GameDirector.SetNumber();
            }
        }
    }
}
