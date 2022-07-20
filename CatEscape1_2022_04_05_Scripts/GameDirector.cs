using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameDirector : MonoBehaviour
{
    GameObject hpGage;

    public GameObject Replay_btn;

    public Text Gameover;

    // Start is called before the first frame update
    void Start()
    {
        this.hpGage = GameObject.Find("hpGage");
    }


    public void DecreaseHp() {
        this.hpGage.GetComponent<Image>().fillAmount -= 0.1f;

    }

}
