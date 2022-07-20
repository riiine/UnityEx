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

    void OnEnable() //Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ�
    {
        if (highScoreTxt != null)
        {
            highScoreTxt.text = "�ְ��� : " +
                GameMgr.Inst.m_BestHeight.ToString("N2");
        }
        if (currentScoreTxt != null)
        {
            currentScoreTxt.text = "�̹���� : " +
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

        if (Input.GetKeyDown(KeyCode.K)) //���� �� ��� �ʱ�ȭ �ϱ�
        {
            PlayerPrefs.DeleteAll(); //ġƮŰ(���� ���� �ʱ�ȭ)
            GameMgr.Inst.m_BestHeight = 0.0f;
            if (highScoreTxt != null)
            {
                highScoreTxt.text = "�ְ��� : " +
                    GameMgr.Inst.m_BestHeight.ToString("N2");
            }
        }//if (Input.GetKeyDown(KeyCode.K))
    }
}
