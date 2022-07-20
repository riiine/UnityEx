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

        //���� �Ÿ��Ʒ� �ı�
        if (transform.position.y < playerPos.y - destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetHideCloud(int a_Count)
    {  //a_Count ��� ������ �ʰ� �� ���� ����

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


        //--- ����� ���� ��Ű��...
        int range = 20 - GameMgr.Inst.DiffLevel;  
        //20���� 1Ȯ���� �������� ������ ����
        if (range < 10) //���̵��� ���� Ȯ���� �� �� �þ����...
            range = 10; //10���� 1Ȯ������ ...

        SpriteRenderer[] a_CloudObj = GetComponentsInChildren<SpriteRenderer>();
        //���� �ڵ�� Active�� Ȱ��ȭ �Ǿ� �ִ� ���� ��ϸ� �������� �ȴ�.
        for (int i = 0; i < a_CloudObj.Length; i++)
        {
            if (Random.Range(0, range) == 0)
                SpawnFish(a_CloudObj[i].transform.position);
        }//for (int i = 0; i < CloudObj.Length; i++)
        //--- ����� ���� ��Ű��...

    }//public void SetHideCloud(int a_Count)

    void SpawnFish(Vector3 a_Pos)
    {
        GameObject go = Instantiate(Fish);
        go.SetActive(true);
        go.transform.position = a_Pos + Vector3.up * 0.8f;
    }
}
