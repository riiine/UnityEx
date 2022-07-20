using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //--이동 관련 변수들
    Vector3 m_DirTgVec = Vector3.right; //날아 가야할 방향 계산용 변수
    Vector3 a_StartPos = Vector3.zero;  //시작 위치 계산용 변수
    private float m_MoveSpeed = 15.0f;  //한플레임당 이동 시키고 싶은 거리 (이동속도)
    //--이동 관련 변수들

    [HideInInspector] public float bullet_Att = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_DirTgVec * Time.deltaTime * m_MoveSpeed;

        if (InGameMgr.m_SceenWMax.x + 0.5f < this.transform.position.x)
        {
            Destroy(gameObject);
        }

        if (this.transform.position.x < InGameMgr.m_SceenWMin.x - 1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void BulletSpawn(Transform a_OwnTr, Vector3 a_DirTgVec,
                            float a_MvSpeed = 15.0f, float att = 20.0f)
    {
        m_DirTgVec = a_DirTgVec;
        a_StartPos = a_OwnTr.position + (m_DirTgVec * 0.5f);
        transform.position = new Vector3(a_StartPos.x,
                                         a_StartPos.y, 0.0f);

        m_MoveSpeed = a_MvSpeed;
        bullet_Att = att;
    }
}
