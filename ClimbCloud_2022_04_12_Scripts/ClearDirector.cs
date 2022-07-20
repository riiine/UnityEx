using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // LoadScene을 사용할 때 쓰임


public class ClearDirector : MonoBehaviour
{

    public Button ReplayBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (ReplayBtn != null)
            ReplayBtn.onClick.AddListener(ReplayBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        //화면 터치하면 GameScene으로
        if (Input.GetMouseButtonDown(0)) { 
            SceneManager.LoadScene("GameScene");
        }

    }

    void ReplayBtnClick()
    { //리플레이 누르면 GameScene으로 
        SceneManager.LoadScene("GameScene");
    }
}
