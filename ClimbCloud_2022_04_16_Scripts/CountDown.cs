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
        {// 3�ʺ��� 0�ʱ��� 1�ʾ� �پ��鼭 ī��Ʈ�ٿ�
            countDown.text = ((int)time % 60).ToString();
            //player.SetActive(false);
        }
        else
        { // 0�ʱ��� �پ��� Start
            countDown.text = "Start";
            //1�� �Ŀ� GameStart �Լ� ȣ��
            Invoke("GameStart", 1f);
            
        }

    }

    

    void GameStart()
    {
        //�г��� ���ְ� ���ӽ���
        CountDownPanel.SetActive(false);
        //player.SetActive(true);
    }
}
