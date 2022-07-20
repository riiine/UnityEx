using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    public Text height;
    public Text MaxHeight;
    GameObject player;
    public GameObject fire;
    public GameObject GameOverPanel;

    private string KeyName = "최고 높이";
    private float bestScore = 0;

    //Awake메서드는 Start메서드보다 앞서 실행된다. 딱 한번 호출
    //게임을 초기화하기위해 사용
    void Awake()
    {//PlayerPrefs로 저장되어있는 최고점수를 불러와서 보여줌
        //PlayerPrefs.DeleteAll();
        bestScore = PlayerPrefs.GetFloat(KeyName, 0);
        MaxHeight.text = $"최고 높이 : {bestScore.ToString("F2")}";
    }


    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 60; //실행 프레임 속도 60프레임으로 고정 시키기.. 코드
        QualitySettings.vSyncCount = 0;
        //모니터 주사율(플레임율)이 다른 컴퓨터일 경우 캐릭터 조작시 빠르게 움직일 수 있다.

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.player = GameObject.Find("cat");
    }
    

    // Update is called once per frame
    void Update()
    {
        // 점프한다.
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0) {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //좌우 이동
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 플레이어 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 따라 이미지 반전 (추가)
        if (key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어의 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else {
            this.animator.speed = 1.0f;
        }
        
        // 플레이어가 화면 밖으로 나가면 처음부터 시작
        if (this.player.transform.position.y < -5) {
            SceneManager.LoadScene("GameScene");
        }

        // 현재 플레이어의 높이가 실시간으로 화면에 출력
        Vector3 playerPos = this.player.transform.position;
        height.text = "높이 : " + string.Format("{0:0.##}", playerPos.y);

        if (playerPos.y < 0) //높이가 음수이면 0으로 처리
        {
            playerPos.y = 0;
            height.text = "높이 : " + string.Format("{0:0.##}", playerPos.y);
        }

        if (playerPos.y > bestScore) //저장된 높이보다 지금 위치가 더 높으면 최고높이로 저장
        {
            PlayerPrefs.SetFloat(KeyName, playerPos.y);
        }

        //캐릭터가 게임 배경 화면을 벗어나지 못하게 막는 처리
        Vector3 a_vPos = transform.position;
        if (2.85f < a_vPos.x)
            a_vPos.x = 2.85f;

        if (a_vPos.x < -2.85f)
            a_vPos.x = -2.85f;
        transform.position = a_vPos;
        
        // 시간이 지나면 불이 점점 올라오게 하기
        fire.transform.position = fire.transform.position
            + Vector3.up*1.2f*Time.deltaTime;
 
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        //점프 할 때는 구름 통과
        GetComponent<CircleCollider2D>().isTrigger = true;

        //불이랑 부딪히면 게임끝
        if (collision.gameObject.name == "fire") {
            player.SetActive(false);
            GameOverPanel.SetActive(true);
        }

        //깃발이랑 부딪히면 게임클리어
        if (collision.gameObject.name == "flag")
        {
            SceneManager.LoadScene("ClearScene");
        }

    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        // 점프 끝나면 구름에 서있음
        GetComponent<CircleCollider2D>().isTrigger = false;
    }




}
