using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCtrl : MonoBehaviour
{
    [HideInInspector] public float speed = 5.0f;
    GameObject player = null;

    public Image warningImg;
    float waitTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.gameState != GameState.GameIng)
        {
            Destroy(gameObject);
            return;
        }//if (GameMgr.gameState != GameState.GameIng)

        if (0 < waitTime)
        {
            waitTime -= Time.deltaTime;
            WarningDirect(); //워닝 깜빡임 연출
            return;
        }

        if (warningImg.enabled == true)
            warningImg.enabled = false;

        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < player.transform.position.y - 10.0f)
        {
            Destroy(gameObject);
        }

    }//void Update()

    public void SetInit(float a_PosX)
    {
        this.player = GameObject.Find("cat");
        // * 1.1f 화살이 떨어지는 위치를 구름의 중앙에 맞춰 주기 위해서...
        transform.position = new Vector3(a_PosX * 1.1f,
                                player.transform.position.y + 10.0f, 0);


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        warningImg.transform.position = new Vector3(screenPos.x,
                warningImg.transform.position.y, warningImg.transform.position.z);
    }

    float alpha = 0.0f; //투명도 변화 속도 
    void WarningDirect() //깜빡임 투명도 변화 연출 함수
    {
        if (warningImg.color.a >= 1.0f)
            alpha = -6.0f;
        else if (warningImg.color.a <= 0.0f)
            alpha = 6.0f;

        warningImg.color = new Color(1.0f, 1.0f, 1.0f,
                    warningImg.color.a + alpha * Time.deltaTime);
    }//void WarningDirect() 
}
