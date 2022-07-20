using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    float span = 1.0f;
    float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            GameObject go = Instantiate(applePrefab) as GameObject;

            int px = Random.Range(-6, 7);  //-6 ~ 6 ±îÁö °ª
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}
