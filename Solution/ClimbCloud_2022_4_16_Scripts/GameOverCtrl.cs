using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCtrl : MonoBehaviour
{
    public Text highScoreTxt;
    public Text currentScoreTxt;
    public Button RestartBtn;

    void OnEnable() //활성화 될 때마다 호출되는 함수
    {
        if (highScoreTxt != null)
        {
            highScoreTxt.text = "최고기록 : " +
                GameMgr.Inst.m_BestHeight.ToString("N2");
        }
        if (currentScoreTxt != null)
        {
            currentScoreTxt.text = "이번기록 : " +
                GameMgr.Inst.m_CurStageHeight.ToString("N2");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RestartBtn != null)
        {
            RestartBtn.onClick.AddListener(() => {
                SceneManager.LoadScene("GameScene");
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (Input.GetKeyDown(KeyCode.K)) //저장 값 모두 초기화 하기
        {
            PlayerPrefs.DeleteAll(); //치트키(저장 정보 초기화)
            GameMgr.Inst.m_BestHeight = 0.0f;
            if (highScoreTxt != null)
            {
                highScoreTxt.text = "최고기록 : " +
                    GameMgr.Inst.m_BestHeight.ToString("N2");
            }
        }//if (Input.GetKeyDown(KeyCode.K))
    }
}
