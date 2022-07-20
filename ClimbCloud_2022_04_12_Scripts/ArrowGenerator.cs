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
    //float m_DwSpeedCtrl = -0.1f; //��ü�� �����ϴ� ���� �ӵ�

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
        {// ���� ���� �� 4�� ���ĺ��� ȭ���� �������� ��
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                this.delta = 0;
                GameObject go = Instantiate(arrowPrefab) as GameObject;
                GameObject warn = Instantiate(warningPrefab) as GameObject;
                int px = Random.Range(-3, 4);
                Vector3 playerPos = this.player.transform.position;
                //�÷��̾�� y��ǥ�� 4���� ū ��ġ���� ȭ���� ������
                go.transform.position = new Vector3(px, playerPos.y + 4, transform.position.z);
                //�÷��̾�� y��ǥ�� 4���� ū ��ġ���� warn�� ������
                warn.transform.position = new Vector3(px, playerPos.y + 4, transform.position.z);
                //2�� �Ŀ� warn �ı�
                Destroy(warn, DestroyTime);


            }
        }

    }
}
