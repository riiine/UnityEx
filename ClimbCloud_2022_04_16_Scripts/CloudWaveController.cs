using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWaveController : MonoBehaviour
{
    GameObject player;
    float destroyDistance = 10.0f;

    public GameObject[] Cloud;

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
    {  //a_Count 몇 개를 보이지않게 할건지 개수
        List<int> active = new List<int>();
        for (int i = 0; i < Cloud.Length; i++)
            active.Add(i);

        for (int i=0; i<a_Count; i++) 
        {
            int r = Random.Range(0, active.Count); //0 ~ 2 = 0
            Cloud[active[r]].SetActive(false);

            active.RemoveAt(r);
        }

        active.Clear();
    }
}
