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
    //�����̳� ȭ�� �μ��� ���� �浹 ���� ����

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;   //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;     //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ 

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

        // �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // �¿��̵�
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // �÷��̾� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //// ���ǵ� ����
        //if (speedx < this.maxWalkSpeed)
        //{
        //    this.rigid2D.AddForce(transform.right * key * this.walkForce);
        //}

        //ĳ���� �̵�
        this.rigid2D.velocity = 
            new Vector2((key * this.walkSpeed), this.rigid2D.velocity.y);

        // �����̴� ���⿡ ���� �̹��� ����(�߰�)
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // �÷��̾��� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // �÷��̾ ȭ�� ������ �����ٸ� ó������
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

        //ȭ������� �������� �ϱ�
        Vector3 pos = transform.position;
        if (pos.x < -2.5f) pos.x = -2.5f;
        if (pos.x > 2.5f) pos.x = 2.5f;
        transform.position = pos;
        //ȭ������� �������� �ϱ�

    }// void Update()

    // �� ����
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("��");
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
                //������ ���� ȸ��
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

            hpImage[i].fillAmount = a_CacHp; //1.0f <= fillAmount �� ���Եȴ�.
        }
    }//void CheckHPImg()

}
