using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    //GameObject player;
    public Text countDown;
    float time = 4;
    public GameObject CountDownPanel;

    // Start is called before the first frame update
    void Start()
    {
        //this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time % 60 >= 0)
        {// 3초부터 0초까지 1초씩 줄어들면서 카운트다운
            countDown.text = ((int)time % 60).ToString();
            //player.SetActive(false);
        }
        else
        { // 0초까지 줄어들면 Start
            countDown.text = "Start";
            //1초 후에 GameStart 함수 호출
            Invoke("GameStart", 1f);
            
        }

    }

    

    void GameStart()
    {
        //패널은 없애고 게임시작
        CountDownPanel.SetActive(false);
        //player.SetActive(true);
    }
}
