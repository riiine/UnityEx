using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject fishPrefab;
    float span = 1.0f;
    float delta = 0;

    int ratio = 3;
    float m_DwSpeedCtrl = -0.1f; //��ü�� �����ϴ� ���� �ӵ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� �ӵ� �� �� �������� �ϱ�...
        m_DwSpeedCtrl -= (Time.deltaTime * 0.01f); 
        if (m_DwSpeedCtrl < -0.3f)
            m_DwSpeedCtrl = -0.3f;

        //���� �ֱ� �� �� ª������ �ϱ�...
        this.span -= (Time.deltaTime * 0.03f);  
        if (this.span < 0.1f)
            this.span = 0.1f;

        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            GameObject go = null;
            int dice = Random.Range(1, 11); //1 ~ 10 ������ �߻�
            if (dice <= ratio)
            {  // fish
                go = Instantiate(fishPrefab) as GameObject;
                go.GetComponent<FishController>().m_DownSpeed = m_DwSpeedCtrl;
            }
            else
            {   // arrow
                go = Instantiate(arrowPrefab) as GameObject;
                go.GetComponent<ArrowController>().m_DownSpeed = m_DwSpeedCtrl;
            }

            int px = Random.Range(-8, 9);  //-8 ~ 8 ���� ��
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
