using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Ctrl : MonoBehaviour
{
    float speed = -4.0f;
    float endPos = -20.0f;  //한블럭을 돌아서 원래 모양이니까 초기 위치로 돌려 놓으면 된다.

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= endPos)
            transform.Translate(-transform.position.x, 0, 0);
    }
}
