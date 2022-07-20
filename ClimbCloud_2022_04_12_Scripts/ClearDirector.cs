using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // LoadScene�� ����� �� ����


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
        //ȭ�� ��ġ�ϸ� GameScene����
        if (Input.GetMouseButtonDown(0)) { 
            SceneManager.LoadScene("GameScene");
        }

    }

    void ReplayBtnClick()
    { //���÷��� ������ GameScene���� 
        SceneManager.LoadScene("GameScene");
    }
}
