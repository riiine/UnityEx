using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hpGage;
    public Text gold;

    public Button replayBtn;
    public Text gameover;
    public Button RButton;
    public Button LButton;
    public GameObject player;
    public GameObject ArrowGenerator;
    public GameObject AppleGenerator;

    int GoldCount; // 사과 먹었을 때 골드값 증가

    // Start is called before the first frame update
    void Start()
    {
        replayBtn.gameObject.SetActive(false);
        gameover.gameObject.SetActive(false);

        if (replayBtn != null)
            replayBtn.onClick.AddListener(replayBtnFunc);

        this.hpGage = GameObject.Find("hpGage");
    }

    public void DecreaseHp() {
        this.hpGage.GetComponent<Image>().fillAmount -= 0.1f;

        //게이지가 0이라면 게임오버 띄우기
        if (this.hpGage.GetComponent<Image>().fillAmount == 0) {
            replayBtn.gameObject.SetActive(true);
            gameover.gameObject.SetActive(true);
            RButton.gameObject.SetActive(false);
            LButton.gameObject.SetActive(false);
            player.SetActive(false);
            ArrowGenerator.SetActive(false);
            AppleGenerator.SetActive(false); 
        }
    }

    public void IncreaseGold()
    {
        // 사과 하나 먹을 때마다 골드 10 증가
        GoldCount = GoldCount + 10;

        this.gold.GetComponent<Text>().text =
            "Gold : " + GoldCount;
    }

    void replayBtnFunc() { //리플레이 눌렀을 때
        replayBtn.gameObject.SetActive(false);
        gameover.gameObject.SetActive(false);
        RButton.gameObject.SetActive(true);
        LButton.gameObject.SetActive(true);
        player.SetActive(true);
        ArrowGenerator.SetActive(true);
        AppleGenerator.SetActive(true);
        this.hpGage.GetComponent<Image>().fillAmount = 1f;

        GoldCount = 0;
        this.gold.GetComponent<Text>().text =
            "Gold : " + GoldCount;
    }
}
