using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject warningPrefab;
    float span = 2.0f;
    float delta = 0;
    float time = 0;

    public float DestroyTime = 2.0f;

    //int ratio = 3;
    //float m_DwSpeedCtrl = -0.1f; //전체를 제어하는 낙하 속도

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time % 60 >= 4)
        {// 게임 시작 후 4초 이후부터 화살이 떨어지게 함
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                this.delta = 0;
                GameObject go = Instantiate(arrowPrefab) as GameObject;
                GameObject warn = Instantiate(warningPrefab) as GameObject;
                int px = Random.Range(-3, 4);
                Vector3 playerPos = this.player.transform.position;
                //플레이어보다 y좌표가 4보다 큰 위치에서 화살이 스폰됨
                go.transform.position = new Vector3(px, playerPos.y + 4, transform.position.z);
                //플레이어보다 y좌표가 4보다 큰 위치에서 warn이 스폰됨
                warn.transform.position = new Vector3(px, playerPos.y + 4, transform.position.z);
                //2초 후에 warn 파괴
                Destroy(warn, DestroyTime);


            }
        }

    }
}
