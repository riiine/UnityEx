using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cat")
        {//�÷��̾�� �ε�����
            
            //����� �ı�
            Destroy(gameObject);

            // ���� ��ũ��Ʈ�� �÷��̾�� ����Ⱑ �浹�ߴٰ� �����Ѵ�.
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseHp();
        }

    }

    
}
