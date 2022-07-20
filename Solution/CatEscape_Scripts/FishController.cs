using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    GameObject player;
    [HideInInspector] public float m_DownSpeed = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        // �����Ӹ��� ������� ���Ͻ�Ų��.
        transform.Translate(0, m_DownSpeed, 0);

        // ȭ�� ������ ������ ������Ʈ�� �Ҹ��Ų��.
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        // �浹 ����(�߰�)
        Vector2 p1 = transform.position; // ȭ���� �߽� ��ǥ
        Vector2 p2 = this.player.transform.position; // �÷��̾��� �߽� ��ǥ
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f; // ȭ�� �ݰ�
        float r2 = 1.0f; // �÷��̾� �ݰ�

        if (d < r1 + r2)
        {
            // �浹�ϸ� ȭ���� �Ҹ��Ų��.
            Destroy(gameObject);

            //���� ��ũ��Ʈ�� �÷��̾�� ȭ���� �浹�ߴٰ� �����Ѵ�.
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().Add_Gold();
        }//if (d < r1 + r2)        
    }//void Update()
}//public class FishController 
