using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    bool a, b; // UI에서 화살표버튼을 눌렀는지 판정

    //플레이어가 이동가능한 범위는 좌우로 8.0f만큼
    Vector2 m_moveLimit = new Vector2(8.0f, 0);

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.
    }

    public void LButtonDown() { // 왼쪽 버튼을 누를 때
            transform.Translate(-0.3f, 0, 0);  // 왼쪽으로 [-0.3] 움직인다.

    }

    public void RButtonDown() { // 오른쪽 버튼을 누를 때
            transform.Translate(0.3f, 0, 0);  // 오른쪽으로 [0.3] 움직인다.
    }

    public void LeftUp() // UI왼쪽버튼 떼짐
    {
        a = false;
    }

    public void LeftDown()  // UI왼쪽버튼 눌림
    {
        a = true;
    }

    public void RightUp() // UI오른쪽버튼 떼짐
    {
        b = false;
    }

    public void RightDown()  // UI오른쪽버튼 눌림
    {
        b = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = ClampPosition(transform.localPosition);

        if (a) {
            LButtonDown();
        }

        if (b) {
            RButtonDown();
        }

        // 왼쪽 화살표를 누르고 있는 동안
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.3f, 0, 0);  // 왼쪽으로 [-0.3] 움직인다.
        }

        // 오른쪽 화살표를 누르고 있는 동안
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.3f, 0, 0);  // 오른쪽으로 [0.3] 움직인다.
        }
    }

    public Vector3 ClampPosition(Vector3 position) 
    { //플레이어의 이동범위를 제한하는 함수
        return new Vector3
        (
            // 좌우로 움직이는 이동범위
            Mathf.Clamp(position.x, -m_moveLimit.x, m_moveLimit.x),
            -3.6f,
            0
        );
    }
}
