using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    GameObject player;
    float distanceDiff = 8.0f;
    float speed = 1.0f; //1초 1m를 움직이게 한다는 속도의 의미

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

        //player와 거리가 너무 먼 경우 보정
        a_FollowHeight = player.transform.position.y - distanceDiff;
        if (transform.position.y < a_FollowHeight)
            transform.position = new Vector3(0, a_FollowHeight, 0);

        //일정속도로 위로 
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
