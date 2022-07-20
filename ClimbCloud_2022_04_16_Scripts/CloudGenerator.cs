using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    GameObject player;

    public GameObject cloudWave;
    float createHeight = 10.0f;
    float recentHeight = -2.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
        //for (int ii = 0; ii < 50; ii++) 
        //{
        //    SpawnCloud(recentHeight);
        //    recentHeight += 2.5f;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;

        //일정 높이에 구름 생성
        if (recentHeight < playerPos.y + createHeight) 
        {
            SpawnCloud(recentHeight);
            recentHeight += 2.5f;
        }
    }

    void SpawnCloud(float a_Height)
    {
        int a_HideCount = 0;

        int a_Level = (int)(a_Height / 15.0f);
        if (a_Level <= 0)
            a_HideCount = 0;
        else if (a_Level == 1)
            a_HideCount = Random.Range(0, 2); //0 ~ 1
        else if (a_Level == 2)
            a_HideCount = Random.Range(0, 3); //0 ~ 2
        else if (a_Level == 3)
            a_HideCount = Random.Range(1, 3); //1 ~ 2
        else if (a_Level == 4)
            a_HideCount = Random.Range(1, 4); //1 ~ 3
        else //if (a_Level == 5)
            a_HideCount = Random.Range(2, 4); //2 ~ 3


        //구름 층 생성
        GameObject go = Instantiate(cloudWave);
        go.transform.position = new Vector3(0, a_Height, 0);
        go.GetComponent<CloudWaveController>().SetHideCloud(a_HideCount);
    }
}
