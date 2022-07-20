using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonType //MonsterType
{
    MT_Zombi,
    MT_Missile,   
};

public class MonsterCtrl : MonoBehaviour
{
    public MonType m_MonType = MonType.MT_Zombi;
    [HideInInspector] public int m_Level = 0;

    float m_MaxHP = 200.0f;  //최대 체력치
    float m_CurHP = 200.0f;  //현재 체력
    public Image m_HPSdBar = null; //using UnityEngine.UI; 필요

    float m_Speed = 4.0f;   //이동 속도
    Vector3 m_CurPos;       //현재 위치
    Vector3 m_SpawnPos;     //처음 스폰 위치

    Vector3 m_DirVec;           //이동 방향
    float m_CacPosY = 0.0f;     //이동 패턴 계산용 변수
    float m_Rand_Y = 0.0f;      //이동 패턴 계산용 변수

    //---------- 총알 발사 관련 변수 선언
    public GameObject m_BulletObj = null;
    GameObject a_NewObj = null;
    BulletCtrl a_BulletSC = null;
    float shoot_Time = 0.0f;
    float shoot_Delay = 1.5f;
    //---------- 총알 발사 관련 변수 선언

    float enemy_Att = 20.0f;     //공격력
    float BulletMvSpeed = 10.0f; //총알 이동 속도

    HeroCtrl m_Hero = null;

    // Start is called before the first frame update
    void Start()
    {
        m_SpawnPos = this.transform.position;
        m_Rand_Y = Random.Range(0.2f, 2.6f);  //Sin함수의 랜덤 진폭

        m_Hero = GameObject.FindObjectOfType<HeroCtrl>();

        m_Speed += m_Speed * m_Level * 0.05f;  //총 2배까지 증가될 수 있음
        m_MaxHP += m_MaxHP * m_Level * 0.1f;   //총 3배까지 증가될 수 있음
        enemy_Att += enemy_Att * m_Level * 0.1f;      //총 3배까지 증가될 수 있음
        BulletMvSpeed += BulletMvSpeed * m_Level * 0.05f; //총 2배까지 증가될 수 있음
    }

    // Update is called once per frame
    void Update()
    {
        m_CurPos = this.transform.position;

        if (m_MonType == MonType.MT_Zombi)
        {
            m_CurPos.x = m_CurPos.x + (-1.0f * Time.deltaTime * m_Speed);
            m_CacPosY += Time.deltaTime * (m_Speed / 2.2f);
            m_CurPos.y = m_SpawnPos.y + Mathf.Sin(m_CacPosY) * m_Rand_Y;
        }
        else if (m_MonType == MonType.MT_Missile)
        {
            Vector3 a_CacVec = m_Hero.transform.position - this.transform.position;
            //추적
            m_DirVec = a_CacVec;  //몬스터의 방향 벡터

            m_DirVec.Normalize();
            m_DirVec.x = -1.0f;
            m_DirVec.z = 0.0f;

            m_CurPos = m_CurPos + (m_DirVec * Time.deltaTime * m_Speed);
        }

        this.transform.position = m_CurPos;

        if (this.transform.position.x < InGameMgr.m_SceenWMin.x - 2.0f)
        {
            Destroy(gameObject);
        }

        //----- 총알 발사 
        if (m_MonType == MonType.MT_Zombi && m_BulletObj != null)
        {
            shoot_Time += Time.deltaTime;
            if (shoot_Delay <= shoot_Time)
            {
                a_NewObj = (GameObject)Instantiate(m_BulletObj);
                //오브젝트의 클론(복사체) 생성 함수   
                a_BulletSC = a_NewObj.GetComponent<BulletCtrl>();
                a_BulletSC.BulletSpawn(this.transform, Vector3.left,
                                        BulletMvSpeed);

                shoot_Time = 0.0f;
            }
        }//if (m_MonType == MonType.MT_Zombi && m_BulletObj != null)
        //----- 총알 발사 

    } //void Update()

    public void TakeDamage(float a_Value)
    {
        if (m_CurHP <= 0.0f) //이렇게 하면 사망 처리는 한번만 될 것이다.
            return;

        InGameMgr.Inst.DamageTxt(a_Value, transform, Color.red);

        m_CurHP = m_CurHP - a_Value;
        if (m_CurHP < 0.0f)
            m_CurHP = 0.0f;

        if (m_HPSdBar != null)
            m_HPSdBar.fillAmount = m_CurHP / m_MaxHP;

        if (m_CurHP <= 0.0f) //몬스터 사망 처리
        {
            InGameMgr.Inst.AddScore(); //점수올리기
            InGameMgr.Inst.ItemDrop(this.transform.position); //보상 주기

            Destroy(gameObject); //<--몬스터 GameObject 제거됨
        }//if (m_CurHP <= 0.0f) //몬스터 사망 처리
    }

    void OnTriggerEnter2D(Collider2D col) //몬스터에 뭔가 충돌 되었을 때 발생되는 함수
    {
        if (col.tag == "AllyBullet")
        {
            TakeDamage(80.0f);
            Destroy(col.gameObject);
        }
    }

}
