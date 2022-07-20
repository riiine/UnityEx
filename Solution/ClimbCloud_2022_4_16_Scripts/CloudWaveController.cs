using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWaveController : MonoBehaviour
{
    GameObject player;
    float destroyDistance = 10.0f;

    public GameObject[] Cloud;
    public GameObject Fish;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;

        //일정 거리아래 파괴
        if (transform.position.y < playerPos.y - destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetHideCloud(int a_Count)
    {  //a_Count 몇개를 보이지 않게 할 건지 개수

        List<int> active = new List<int>();
        for (int i = 0; i < Cloud.Length; i++)
            active.Add(i);

        for (int i = 0; i < a_Count; i++)
        {
            int r = Random.Range(0, active.Count); //0 ~ 2 = 0
            Cloud[active[r]].SetActive(false);

            active.RemoveAt(r);
        }

        active.Clear();


        //--- 물고기 스폰 시키기...
        int range = 20 - GameMgr.Inst.DiffLevel;  
        //20분의 1확률로 구름위에 아이템 생성
        if (range < 10) //난이도에 따라서 확률은 점 점 늘어나도록...
            range = 10; //10분의 1확률까지 ...

        SpriteRenderer[] a_CloudObj = GetComponentsInChildren<SpriteRenderer>();
        //위의 코드로 Active가 활성화 되어 있는 구름 목록만 가져오게 된다.
        for (int i = 0; i < a_CloudObj.Length; i++)
        {
            if (Random.Range(0, range) == 0)
                SpawnFish(a_CloudObj[i].transform.position);
        }//for (int i = 0; i < CloudObj.Length; i++)
        //--- 물고기 스폰 시키기...

    }//public void SetHideCloud(int a_Count)

    void SpawnFish(Vector3 a_Pos)
    {
        GameObject go = Instantiate(Fish);
        go.SetActive(true);
        go.transform.position = a_Pos + Vector3.up * 0.8f;
    }
}
