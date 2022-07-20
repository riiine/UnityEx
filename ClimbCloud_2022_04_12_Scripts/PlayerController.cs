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

    private string KeyName = "�ְ� ����";
    private float bestScore = 0;

    //Awake�޼���� Start�޼��庸�� �ռ� ����ȴ�. �� �ѹ� ȣ��
    //������ �ʱ�ȭ�ϱ����� ���
    void Awake()
    {//PlayerPrefs�� ����Ǿ��ִ� �ְ������� �ҷ��ͼ� ������
        //PlayerPrefs.DeleteAll();
        bestScore = PlayerPrefs.GetFloat(KeyName, 0);
        MaxHeight.text = $"�ְ� ���� : {bestScore.ToString("F2")}";
    }


    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.player = GameObject.Find("cat");
    }
    

    // Update is called once per frame
    void Update()
    {
        // �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0) {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //�¿� �̵�
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // �÷��̾� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // ���ǵ� ����
        if (speedx < this.maxWalkSpeed) {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // �����̴� ���⿡ ���� �̹��� ���� (�߰�)
        if (key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // �÷��̾��� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else {
            this.animator.speed = 1.0f;
        }
        
        // �÷��̾ ȭ�� ������ ������ ó������ ����
        if (this.player.transform.position.y < -5) {
            SceneManager.LoadScene("GameScene");
        }

        // ���� �÷��̾��� ���̰� �ǽð����� ȭ�鿡 ���
        Vector3 playerPos = this.player.transform.position;
        height.text = "���� : " + string.Format("{0:0.##}", playerPos.y);

        if (playerPos.y < 0) //���̰� �����̸� 0���� ó��
        {
            playerPos.y = 0;
            height.text = "���� : " + string.Format("{0:0.##}", playerPos.y);
        }

        if (playerPos.y > bestScore) //����� ���̺��� ���� ��ġ�� �� ������ �ְ���̷� ����
        {
            PlayerPrefs.SetFloat(KeyName, playerPos.y);
        }

        //ĳ���Ͱ� ���� ��� ȭ���� ����� ���ϰ� ���� ó��
        Vector3 a_vPos = transform.position;
        if (2.85f < a_vPos.x)
            a_vPos.x = 2.85f;

        if (a_vPos.x < -2.85f)
            a_vPos.x = -2.85f;
        transform.position = a_vPos;
        
        // �ð��� ������ ���� ���� �ö���� �ϱ�
        fire.transform.position = fire.transform.position
            + Vector3.up*1.2f*Time.deltaTime;
 
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        //���� �� ���� ���� ���
        GetComponent<CircleCollider2D>().isTrigger = true;

        //���̶� �ε����� ���ӳ�
        if (collision.gameObject.name == "fire") {
            player.SetActive(false);
            GameOverPanel.SetActive(true);
        }

        //����̶� �ε����� ����Ŭ����
        if (collision.gameObject.name == "flag")
        {
            SceneManager.LoadScene("ClearScene");
        }

    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        // ���� ������ ������ ������
        GetComponent<CircleCollider2D>().isTrigger = false;
    }




}
