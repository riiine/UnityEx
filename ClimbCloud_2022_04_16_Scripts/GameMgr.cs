using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    GameObject player;

    public Text HeightTxt;
    public Text BestHeightTxt;
    public Button ReplayBtn;

    float m_CurHeight = 0.0f;  //현재높이
    public static float m_CurStageHeight = 0.0f;  //현 스테이지에서의 최고높이
    public static float m_BestHeight = 0.0f;      //최고기록높이

    GameObject Life_1; //첫번째 하트
    GameObject Life_2; //두번째 하트
    GameObject Life_3; //세번째 하트

    GameObject water;

    public GameObject GameOverPanel;

    private string KeyName = "최고 높이";
    private float bestScore = 0;

    //Awake메서드는 Start메서드보다 앞서 실행된다. 딱 한 번 호출
    //게임을 초기화하기위해 사용
    void Awake()
    {//PlayerPrefs로 저장되어있는 최고점수를 불러와서 보여줌
        //PlayerPrefs.DeleteAll(); //최고점수 0으로 초기화
        bestScore = PlayerPrefs.GetFloat(KeyName, 0);
        BestHeightTxt.text = $"최고 높이 : {bestScore.ToString("F2")}";
    }

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("cat");
        m_CurStageHeight = 0.0f;

        this.Life_1 = GameObject.Find("Life_1");
        this.Life_2 = GameObject.Find("Life_2");
        this.Life_3 = GameObject.Find("Life_3");

        this.water = GameObject.Find("water");

        if (ReplayBtn != null)
            ReplayBtn.onClick.AddListener(ReplayBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        //높이값 기록
        m_CurHeight = player.transform.position.y;

        if (m_CurHeight < 0)
            m_CurHeight = 0.0f;

        if (m_CurStageHeight < m_CurHeight)
            m_CurStageHeight = m_CurHeight;

        if (HeightTxt != null)
            HeightTxt.text = string.Format("높이 : {0:N}", m_CurStageHeight);

        if (m_CurStageHeight > bestScore) //저장된 높이보다 지금 위치가 더 높으면 최고높이로 저장
        {
            PlayerPrefs.SetFloat(KeyName, m_CurStageHeight);
        }

        Invoke("Water", 6f); //6초 후 Water 호출
        
    }

    void Water() { //물이 점점 올라옴
        water.transform.position = water.transform.position
            + Vector3.up * 2.5f * Time.deltaTime;
    }

    public void DecreaseLife() { //세번째 하트부터 순차적으로 줄어듦
        this.Life_3.GetComponent<Image>().fillAmount -= 0.5f;

        if (this.Life_3.GetComponent<Image>().fillAmount == 0) {
            this.Life_2.GetComponent<Image>().fillAmount -= 0.5f;
        } 
        
        if (this.Life_2.GetComponent<Image>().fillAmount == 0) {
            this.Life_1.GetComponent<Image>().fillAmount -= 0.5f;
        }

        if (this.Life_1.GetComponent<Image>().fillAmount == 0) {
            GameOverPanel.SetActive(true); //하트가 모두 소진되면 게임종료
        }
    }

    public void IncreaseLife() { //첫번째 하트부터 채움
        this.Life_1.GetComponent<Image>().fillAmount += 0.25f;

        if (this.Life_3.GetComponent<Image>().fillAmount == 0 &&
            this.Life_1.GetComponent<Image>().fillAmount == 1) 
        {
            this.Life_2.GetComponent<Image>().fillAmount += 0.25f;
        } 

        if (this.Life_2.GetComponent<Image>().fillAmount == 1 &&
            this.Life_1.GetComponent<Image>().fillAmount == 1) 
        {
            this.Life_3.GetComponent<Image>().fillAmount += 0.25f;
        }
    }
    void ReplayBtnClick()
    { //리플레이 누르면 GameScene으로 
        SceneManager.LoadScene("GameScene");
    }
}
