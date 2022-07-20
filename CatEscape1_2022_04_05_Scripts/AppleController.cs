using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        // 프레임마다 등속으로 낙하시킨다.
        transform.Translate(0, -0.1f, 0);

        // 화면 밖으로 나오면 오브젝트를 소멸시킨다.
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        // 충돌 판정(추가)
        Vector2 p1 = transform.position;  // 사과의 중심 좌표
        Vector2 p2 = this.player.transform.position;  // 플레이어의 중심 좌표
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;  // 사과 반경
        float r2 = 1.0f;  // 플레이어 반경

        if (d < r1 + r2)
        {
            // 충돌하면 사과를 소멸시킨다.
            Destroy(gameObject);
        }
    }
}
