using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    GameObject player;
    float startY = 12.0f;  //백그라운드의 시작 y높이 위치
    float scroll = 0.2f;   //스크롤 속도

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        float scrollPos = startY - this.player.transform.position.y * scroll;
        if (scrollPos > 12.0f)
            scrollPos = 12.0f;
        else if (scrollPos < -12.0f)
            scrollPos = -12.0f;

        transform.position = new Vector3(0.0f,
                this.player.transform.position.y + scrollPos, 0.0f);
    }
}
