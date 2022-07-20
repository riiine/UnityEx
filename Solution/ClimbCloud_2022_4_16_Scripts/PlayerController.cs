using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float walkSpeed = 3.0f;
    Animator animator;

    float hp = 3.0f;
    public Image[] hpImage;

    GameObject m_OverlapBlock = null;
    //보상이나 화살 두세번 연속 충돌 방지 변수

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;   //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;     //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.gameState != GameState.GameIng)
        {
            this.animator.speed = 0.0f;
            return;
        }

        // 점프한다.
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 좌우이동
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //// 스피드 제한
        //if (speedx < this.maxWalkSpeed)
        //{
        //    this.rigid2D.AddForce(transform.right * key * this.walkForce);
        //}

        //캐릭터 이동
        this.rigid2D.velocity = 
            new Vector2((key * this.walkSpeed), this.rigid2D.velocity.y);

        // 움직이는 방향에 따라 이미지 반전(추가)
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어의 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // 플레이어가 화면 밖으로 나갔다면 처음부터
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

        //화면밖으로 못나가게 하기
        Vector3 pos = transform.position;
        if (pos.x < -2.5f) pos.x = -2.5f;
        if (pos.x > 2.5f) pos.x = 2.5f;
        transform.position = pos;
        //화면밖으로 못나가게 하기

    }// void Update()

    // 골 도착
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("골");
        //SceneManager.LoadScene("ClearScene");

        if (other.gameObject.name.Contains("WaterRoot") == true)
        {
            GameMgr.Inst.GameOver();
        } //if (other.gameObject.name.Contains("WaterRoot") == true)
        else if (other.gameObject.name.Contains("arrow") == true)
        {
            if (m_OverlapBlock != other.gameObject)
            {
                hp -= 1.0f;
                CheckHPImg();
                if (hp <= 0.0f)
                    GameMgr.Inst.GameOver();

                m_OverlapBlock = other.gameObject;
            }//if (m_OverlapBlock != other.gameObject)

            Destroy(other.gameObject);
        }
        else if (other.gameObject.name.Contains("fish") == true)
        {
            if (m_OverlapBlock != other.gameObject)
            {
                //에너지 조금 회복
                hp += 0.5f;
                if (3.0f < hp)
                    hp = 3.0f;
                CheckHPImg();

                m_OverlapBlock = other.gameObject;
            }

            Destroy(other.gameObject);
        }//else if (other.gameObject.name.Contains("fish") == true)

    }//void OnTriggerEnter2D(Collider2D other)

    void CheckHPImg()
    {
        float a_CacHp = 0.0f;
        for (int i = 0; i < hpImage.Length; i++)
        {
            a_CacHp = hp - (float)i;
            if (a_CacHp < 0.0f)
                a_CacHp = 0.0f;

            if (0.45f < a_CacHp && a_CacHp < 0.55f)
                a_CacHp = 0.445f;

            hpImage[i].fillAmount = a_CacHp; //1.0f <= fillAmount 꽉 차게된다.
        }
    }//void CheckHPImg()

}
