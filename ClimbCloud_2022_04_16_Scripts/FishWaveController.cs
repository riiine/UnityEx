using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWaveController : MonoBehaviour
{
    GameObject player;
    float destroyDistance = 10.0f;

    public GameObject[] Fish;

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

    public void SetHideFish(int a_Count)
    {  //a_Count �� ���� �������ʰ� �Ұ��� ����
        List<int> active = new List<int>();
        for (int i = 0; i < Fish.Length; i++)
            active.Add(i);

        for (int i = 0; i < a_Count; i++)
        {
            int r = Random.Range(0, active.Count); //0 ~ 2 = 0
            Fish[active[r]].SetActive(false);

            active.RemoveAt(r);
        }

        active.Clear();
    }

    
}
