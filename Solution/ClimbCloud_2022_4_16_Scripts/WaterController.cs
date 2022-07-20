using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    GameObject player;
    float distanceDiff = 8.0f;
    float speed = 1.0f; //1�� 1m�� �����̰� �Ѵٴ� �ӵ��� �ǹ�

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    float a_FollowHeight = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.gameState != GameState.GameIng)
            return;

        //player�� �Ÿ��� �ʹ� �� ��� ����
        a_FollowHeight = player.transform.position.y - distanceDiff;
        if (transform.position.y < a_FollowHeight)
            transform.position = new Vector3(0, a_FollowHeight, 0);

        //�����ӵ��� ���� 
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
