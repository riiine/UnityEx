using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject hpGage;
    public GameObject GameOverPannel;
    public Button ReplayBtn;

    // Start is called before the first frame update
    void Start()
    {
        this.hpGage = GameObject.Find("hpGage");

        if (ReplayBtn != null)
            ReplayBtn.onClick.AddListener(ReplayBtnClick);
    }

    public void DecreaseHp() {
        this.hpGage.GetComponent<Image>().fillAmount -= 0.1f;
    }

    public void IncreaseHp()
    {
        this.hpGage.GetComponent<Image>().fillAmount += 0.1f;
    }

    void ReplayBtnClick()
    { //리플레이 누르면 GameScene으로 
        SceneManager.LoadScene("GameScene");
    }

}
