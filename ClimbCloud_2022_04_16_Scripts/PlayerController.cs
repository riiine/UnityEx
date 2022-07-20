using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    Animator animator;
    float threshold = 0.2f;
    
    public GameObject GameOverPanel;
    
    GameObject water;
    GameObject FishGenerator;
    GameObject ArrowGenerator;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        time += Time.deltaTime;

        int key = 0;

        if (time % 60 < 5) { //게임오버패널 나타나지 않게..
            GameOverPanel.SetActive(false);
        }

        if (time % 60 >= 5)
        { //5초 경과 후 움직일 수 있게 함
            //좌우이동
            if (Input.GetKey(KeyCode.RightArrow)) key = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

            //점프한다.
            if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
            {
                this.animator.SetTrigger("JumpTrigger");
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }

        }

        //플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //스피드 제한
        if (speedx < this.maxWalkSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //움직이는 방향에 따라 이미지 반전(추가)
        if (key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //플레이어의 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else {
            this.animator.speed = 1.0f;
        }
        

        //플레이어가 화면 밖으로 나갔다면 처음부터
        if (transform.position.y < -10) {
            SceneManager.LoadScene("GameScene");
        }

        //화면밖으로 못나가게 하기
        Vector3 pos = transform.position;
        if (pos.x < -2.5f) pos.x = -2.5f;
        if (pos.x > 2.5f) pos.x = 2.5f;
        transform.position = pos;

        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FishWave")
        {//플레이어랑 부딪히면
            //물고기 파괴
            collision.gameObject.SetActive(false);

            // 감독 스크립트에 플레이어와 물고기가 충돌했다고 전달한다.
            GameObject director = GameObject.Find("GameMgr");
            director.GetComponent<GameMgr>().IncreaseLife(); //하트 증가
        }

        if (collision.gameObject.tag == "arrow")
        {//플레이어랑 부딪히면
            //물고기 파괴
            collision.gameObject.SetActive(false);

            // 감독 스크립트에 플레이어와 물고기가 충돌했다고 전달한다.
            GameObject director = GameObject.Find("GameMgr");
            director.GetComponent<GameMgr>().DecreaseLife(); //하트 감소
        }

        if (collision.gameObject.tag == "water")
        {//물이랑 부딪히면
            GameOverPanel.SetActive(true); //게임오버패널 나타내기
            
        }

    }
}
