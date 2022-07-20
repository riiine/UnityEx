using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    bool a, b; // UI���� ȭ��ǥ��ư�� �������� ����

    //�÷��̾ �̵������� ������ �¿�� 8.0f��ŭ
    Vector2 m_moveLimit = new Vector2(8.0f, 0);

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.
    }

    public void LButtonDown() { // ���� ��ư�� ���� ��
            transform.Translate(-0.3f, 0, 0);  // �������� [-0.3] �����δ�.

    }

    public void RButtonDown() { // ������ ��ư�� ���� ��
            transform.Translate(0.3f, 0, 0);  // ���������� [0.3] �����δ�.
    }

    public void LeftUp() // UI���ʹ�ư ����
    {
        a = false;
    }

    public void LeftDown()  // UI���ʹ�ư ����
    {
        a = true;
    }

    public void RightUp() // UI�����ʹ�ư ����
    {
        b = false;
    }

    public void RightDown()  // UI�����ʹ�ư ����
    {
        b = true;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = ClampPosition(transform.localPosition);

        if (a) {
            LButtonDown();
        }

        if (b) {
            RButtonDown();
        }

        // ���� ȭ��ǥ�� ������ �ִ� ����
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.3f, 0, 0);  // �������� [-0.3] �����δ�.
        }

        // ������ ȭ��ǥ�� ������ �ִ� ����
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.3f, 0, 0);  // ���������� [0.3] �����δ�.
        }
    }

    public Vector3 ClampPosition(Vector3 position) 
    { //�÷��̾��� �̵������� �����ϴ� �Լ�
        return new Vector3
        (
            // �¿�� �����̴� �̵�����
            Mathf.Clamp(position.x, -m_moveLimit.x, m_moveLimit.x),
            -3.6f,
            0
        );
    }
}
