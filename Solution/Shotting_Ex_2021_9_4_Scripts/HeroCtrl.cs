using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCtrl : MonoBehaviour
{
    float m_MaxHP = 200.0f;
    float m_CurHP = 200.0f;
    public Image m_HPSdBar = null; //using UnityEngine.UI; 필요

    //---------- 키보드 입력값 변수 선언
    private float h = 0.0f;
    private float v = 0.0f;

    private float moveSpeed = 7.0f;
    Vector3 moveDir = Vector3.zero;
    //---------- 키보드 입력값 변수 선언

    //----------------주인공이 지형 밖으로 나갈 수 없도록 막기 위한 변수 void LimitMove()
    Vector3 HalfSize = Vector3.zero;
    Vector3 m_CacCurPos = Vector3.zero;

    float a_LmtBdLeft = 0;
    float a_LmtBdTop = 0;
    float a_LmtBdRight = 0;
    float a_LmtBdBottom = 0;
    //----------------주인공이 지형 밖으로 나갈 수 없도록 막기 위한 변수 void LimitMove()

    //---------- 총알 발사 관련 변수 선언
    public GameObject m_BulletObj = null;
    float m_AttSpeed = 0.15f;   //주인공 공속
    float m_CacAtTick = 0.0f;   //기관총 발사 틱 만들기....
    GameObject a_NewObj = null;
    BulletCtrl a_BulletSC = null;
    //---------- 총알 발사 관련 변수 선언

    // Start is called before the first frame update
    void Start()
    {
        //------ 캐릭터의 가로 반사이즈, 세로 반사이즈 구하기
        //월드에 그려진 스프라이트 사이즈 얻어오기
        SpriteRenderer sprRend = 
                gameObject.GetComponentInChildren<SpriteRenderer>();
        //sprRend.transform.localScale <-- 스프라이트는 이걸로 사이즈를 구하면 안된다.
        HalfSize.x = sprRend.bounds.size.x / 2.0f - 0.23f;
        //나중에 주인공 캐릭터 외형을 바꾸면 다시 계산해 준다.
        HalfSize.y = sprRend.bounds.size.y / 2.0f - 0.05f;
        //여백이 커서 조금 줄여 주자
        HalfSize.z = 1.0f;
        //------ 캐릭터의 가로 반사이즈, 세로 반사이즈 구하기
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (h != 0.0f || v != 0.0f)
        {
            moveDir = new Vector3(h, v, 0);
            if (1.0f < moveDir.magnitude)
                moveDir.Normalize();
            transform.position +=
                moveDir * moveSpeed * Time.deltaTime;
        }

        LimitMove();

        //--------------- 총알 발사 코드
        if (0.0f < m_CacAtTick)
            m_CacAtTick = m_CacAtTick - Time.deltaTime;

        //if (Input.GetMouseButton(0))
        {
            if (m_CacAtTick <= 0.0f)
            {
                a_NewObj = (GameObject)Instantiate(m_BulletObj);
                //오브젝트의 클론(복사체) 생성 함수   
                a_BulletSC = a_NewObj.GetComponent<BulletCtrl>();
                a_BulletSC.BulletSpawn(this.transform, Vector3.right);

                m_CacAtTick = m_AttSpeed;
            }
        }//if (Input.GetMouseButton(1))
        //--------------- 총알 발사 코드
    } //void Update()

    void LimitMove()
    {
        m_CacCurPos = transform.position;

        a_LmtBdLeft = InGameMgr.m_SceenWMin.x + HalfSize.x;
        a_LmtBdTop = InGameMgr.m_SceenWMin.y  + HalfSize.y;
        a_LmtBdRight = InGameMgr.m_SceenWMax.x - HalfSize.x;
        a_LmtBdBottom = InGameMgr.m_SceenWMax.y - HalfSize.y;

        if (m_CacCurPos.x < a_LmtBdLeft)
            m_CacCurPos.x = a_LmtBdLeft;

        if (a_LmtBdRight < m_CacCurPos.x)
            m_CacCurPos.x = a_LmtBdRight;

        if (m_CacCurPos.y < a_LmtBdTop)
            m_CacCurPos.y = a_LmtBdTop;

        if (a_LmtBdBottom < m_CacCurPos.y)
            m_CacCurPos.y = a_LmtBdBottom;

        transform.position = m_CacCurPos;
    }

    public void TakeDamage(float a_Value)
    {
        if (m_CurHP <= 0.0f) //이렇게 하면 사망처리는 한번만 될 것이다.
            return;

        InGameMgr.Inst.DamageTxt(a_Value, transform, Color.blue);

        m_CurHP = m_CurHP - a_Value;
        if (m_CurHP < 0.0f)
            m_CurHP = 0.0f;

        if (m_HPSdBar != null)
            m_HPSdBar.fillAmount = m_CurHP / m_MaxHP;

        if (m_CurHP <= 0.0f)
        {
            Time.timeScale = 0.0f; //일시정지
            InGameMgr.Inst.GameOverFunc();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            TakeDamage(50);
            Destroy(col.gameObject);
        }
        else if (col.tag == "EnemyBullet")
        {
            float a_Damage = 20.0f;
            TakeDamage(a_Damage);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name.Contains("CoinItem") == true)
        {
            InGameMgr.Inst.AddGold();
            Destroy(col.gameObject);
        }
    }

    public void UseItem(CharType a_CrType)
    {
        if (a_CrType < 0 || CharType.CrCount <= a_CrType)//선택이없는 상태
            return;

        bool isHeal = false;
        if (a_CrType == CharType.Char_0)
        {
            m_CurHP += m_MaxHP * 0.3f;
            isHeal = true;

        }
        else if (a_CrType == CharType.Char_1)
        {
            m_CurHP += m_MaxHP * 0.5f;
            isHeal = true;
        }
        else //if (a_CrType == CharType.Char_2)
        {
            m_CurHP = m_MaxHP;
            isHeal = true;
        }

        if (isHeal == true)
        {
            if (m_MaxHP < m_CurHP)
                m_CurHP = m_MaxHP;
            if (m_HPSdBar != null)
                m_HPSdBar.fillAmount = m_CurHP / m_MaxHP;
        }

        GlobalValue.m_CrDataList[(int)a_CrType].m_CurSkillCount--;

    }

}
