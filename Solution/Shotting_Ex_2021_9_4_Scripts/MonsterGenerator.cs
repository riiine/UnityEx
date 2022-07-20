using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    public GameObject[] MonPrefab;

    float m_SpDelta = 0.0f;
    float m_DiffSpawn = 1.0f;     //난이도에 따른 몬스터 스폰주기 변수

    float m_DiffTick = 0.0f;
    int m_DiffLevel = 0;          //난이도 단계
    int m_FlowCount = 0;
    int m_FlowMinus = 9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_DiffTick += Time.deltaTime;
        if (5.0f <= m_DiffTick)
        {
            m_DiffLevel++;
            if (20 < m_DiffLevel)
                m_DiffLevel = 20;
            else
            {
                m_FlowCount++;
                if (12 <= m_FlowCount)
                {
                    m_DiffLevel -= m_FlowMinus; //처음 -9칸 레벨 다운, 다음 -6칸 레벨 다운, 다음 -3칸 레벨 다운
                    m_FlowMinus -= 3;
                    if (m_FlowMinus < 0)
                        m_FlowMinus = 0;
                    m_FlowCount = 0; //-100; <--한번만 적용하겠다는 뜻이 된다.
                }
            }

            m_DiffSpawn = 1.0f - (m_DiffLevel * 0.03f);

            m_DiffTick = 0.0f;
        }


        m_SpDelta -= Time.deltaTime;
        if (m_SpDelta < 0.0f)
        {
            GameObject go = null;

            int dice = Random.Range(1, 11);     //1 ~ 10 랜덤값 발생
            if (1 < dice) 
            {
                go = Instantiate(MonPrefab[0]) as GameObject;
            }
            else
            {
                go = Instantiate(MonPrefab[1]) as GameObject;
            }

            float py = Random.Range(-3.0f, 3.0f);    
            go.transform.position =
                            new Vector3(InGameMgr.m_SceenWMax.x + 1.0f, py, 0);

            MonsterCtrl a_Enemy = go.gameObject.GetComponent<MonsterCtrl>();
            if (a_Enemy != null)
            {
                a_Enemy.m_Level = m_DiffLevel;
            }

            m_SpDelta = m_DiffSpawn;
        }//if (m_SpDelta < 0.0f)
    } //void Update()
}
