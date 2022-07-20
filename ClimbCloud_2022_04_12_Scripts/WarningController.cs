using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    float time;

    public float DestroyTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //2ÃÊ ÈÄ¿¡ ÆÄ±«
        Destroy(gameObject, DestroyTime);

        time = 0;
        time += Time.deltaTime;

        if (time % 60 >= 0 && time % 60 < 0.5)
        {
            gameObject.SetActive(true);
        }
        else if (time % 60 >= 0.5 && time % 60 < 1)
        {
            gameObject.SetActive(false);
        }
        else if (time % 60 >= 1 && time % 60 < 1.5)
        {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
            time = 0;
        }
    }
}
