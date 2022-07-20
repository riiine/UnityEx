using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hpGage;
    Image m_hpImg = null;

    public Text Game_Gold_Txt;
    int Gold = 0;

    public GameObject GameOverPannel;
    public Text Result_Gd_Txt;
    public Button ReplayBtn;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;  //원래 게임 속도로 돌려놓기...

        this.hpGage = GameObject.Find("hpGage");
        if (hpGage != null)
            m_hpImg = this.hpGage.GetComponent<Image>();

        if (ReplayBtn != null)
            ReplayBtn.onClick.AddListener(ReplayBtnClick);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void DecreaseHp()
    {
        if (m_hpImg == null)
            return;

        //주의 : Update 가 아닙니다.
        m_hpImg.fillAmount -= 0.1f;

        if (m_hpImg.fillAmount <= 0.0f) //게임 종료 조건
        {
            GameOverPannel.SetActive(true);
            Result_Gd_Txt.text = "Gold : " + Gold;

            Time.timeScale = 0.0f;  //일시정지
        }
    }

    public void Add_Gold()
    {
        if (m_hpImg == null || Game_Gold_Txt == null)
            return;

        if (m_hpImg.fillAmount <= 0.0f)
            return;  //게임 종료 조건

        Gold += 10;

        if (Game_Gold_Txt != null)
            Game_Gold_Txt.text = "Gold : " + Gold;
    }

    void ReplayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

} //public class GameDirector
