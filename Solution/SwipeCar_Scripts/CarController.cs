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
        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.

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
            // 스와이프 길이를 구한다.
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

                // 효과음을 재생
                GetComponent<AudioSource>().Play();

                GameDirector.s_State = GameState.MoveIng;
            }//else if (Input.GetMouseButtonUp(0))
        }//if (GameDirector.s_State == GameState.Ready)
        else if (GameDirector.s_State == GameState.MoveIng)
        {
            transform.Translate(this.speed, 0, 0);  // 이동
            this.speed *= 0.98f;            // 감속

            if (this.speed < 0.0005f) //자동차 도착 조건
            {
                this.speed = 0.0f;

                //----- 다음번 플레이어를 위한 초기화 
                this.transform.position = new Vector3(-7.0f, -3.7f, 0.0f);
                GameDirector.s_State = GameState.Ready;
                //----- 다음번 플레이어를 위한 초기화 

                if (m_GDirector != null)
                    m_GDirector.RecordLength(); 
                //지금 플레이가 끝난 유저의 기록을 저장해 줘~

            }//if(this.speed < 0.0005f) //자동차 도착 조건

        }//else if (GameDirector.s_State == GameState.MoveIng)

    }//void Update()
}//public class CarController
