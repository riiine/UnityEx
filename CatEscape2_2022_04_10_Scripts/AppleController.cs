using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    float movingDistance = 0;
    float speed = -0.2f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        movingDistance += speed * Time.deltaTime;

        // �����Ӹ��� ��� ���Ͻ�Ų��.
        transform.Translate(0, movingDistance, 0);


        // ȭ�� ������ ������ ������Ʈ�� �Ҹ��Ų��.
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        // �浹 ����(�߰�)
        Vector2 p1 = transform.position;  // ����� �߽� ��ǥ
        Vector2 p2 = this.player.transform.position;  // �÷��̾��� �߽� ��ǥ
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;  // ��� �ݰ�
        float r2 = 1.0f;  // �÷��̾� �ݰ�

        if (d < r1 + r2)
        {
            // �浹�ϸ� ����� �Ҹ��Ų��.
            Destroy(gameObject);

            // ���� ��ũ��Ʈ�� �÷��̾�� ����� �浹�ߴٰ� �����Ѵ�.
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().IncreaseGold();
        }
    }
}
