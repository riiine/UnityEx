using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    GameObject player;

    public GameObject fishWave;
    float createHeight = 10.0f;
    float recentHeight = -2f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;

        //일정 높이에 물고기 생성
        if (recentHeight < playerPos.y + createHeight)
        {
            SpawnFish(recentHeight);
            recentHeight += 2.5f;
        }
    }

    void SpawnFish(float a_Height)
    {
        int a_HideCount = 0;

        int a_Level = (int)(a_Height / 15.0f);
        if (a_Level <= 0)
            a_HideCount = Random.Range(2, 4); //2 ~ 3
        else if (a_Level == 1)
            a_HideCount = Random.Range(2, 4); //2 ~ 3
        else if (a_Level == 2)
            a_HideCount = Random.Range(3, 5); //3 ~ 4
        else if (a_Level == 3)
            a_HideCount = Random.Range(3, 5); //3 ~ 4
        else if (a_Level == 4)
            a_HideCount = Random.Range(4, 6); //4 ~ 5
        else //if (a_Level == 5)
            a_HideCount = Random.Range(4, 6); //4 ~ 5


        //물고기 생성
        GameObject go = Instantiate(fishWave);
        go.transform.position = new Vector3(0, a_Height, 0);
        go.GetComponent<FishWaveController>().SetHideFish(a_HideCount);
    }
}