using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowController : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        //�����Ӹ��� ������� ���Ͻ�Ų��.
        transform.Translate(0, -0.1f, 0);

        // ȭ�� ������ ������ ������Ʈ�� �Ҹ��Ų��.
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            
        }

        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cat")
        {//�÷��̾�� �ε�����
            
            //ȭ�� �ı�
            Destroy(gameObject);

            // ���� ��ũ��Ʈ�� �÷��̾�� ȭ���� �浹�ߴٰ� �����Ѵ�.
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
        }

    }
}
