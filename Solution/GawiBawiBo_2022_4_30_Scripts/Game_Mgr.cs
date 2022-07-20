using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState //������ ��ſ� ���Ǵ� ��Ī
{
    UserPlay_Ing = 0,  //������ ������ ����ϴ� ����
    Result_Ing = 1,    //��� �����ֱ� ����
    GameEnd = 2        //���� ���� ����
}

public class Game_Mgr : MonoBehaviour
{
    public Button m_Gawi_Btn;   //������ư
    public Button m_Bawi_Btn;   //������ư
    public Button m_Bo_Btn;     //����ư

    public Text m_UserInfo_Text; //���� ���� �ؽ�Ʈ
    public Text m_Result_Text;   //��� �ؽ�Ʈ

    int m_Money = 1000;   //������ ���� �ݾ�
    int m_WinCount = 0;   //�¸� Ƚ��
    int m_LostCount = 0;  //�й� Ƚ��

    [Header("--- Gameble ---")]
    public Text m_Gameble_Text;
    public Slider m_Gameble_Slider;
    int m_Gameble = 100;

    [Header("--- Direction ---")] //����
    public Sprite[] m_IconImg;
    public Image m_UesrGBB_Img;
    public Image m_ComGBB_Img;

    public Text m_ShowResult;
    float m_ResultDelay = 0.0f;

    [Header("--- Auto Button ---")]
    public Toggle m_AutoToggle = null;
    public Button m_NextStartBtn = null; //�ٽ�����

    [Header("-------- DamageText --------")]
    public Transform m_HUD_Canvas = null;
    public GameObject m_DamagePrefab = null;
    public Transform m_SpawnTxtPos = null;

    GameState m_GameState = GameState.UserPlay_Ing;
    float m_DirecCount = 0.0f;   //��ǻ�� �ִϸ��̼� ����� ����

    // Start is called before the first frame update
    void Start()
    {
        if (m_Gawi_Btn != null)
            m_Gawi_Btn.onClick.AddListener(GawiBtnFunc);

        if (m_Bawi_Btn != null)
            m_Bawi_Btn.onClick.AddListener(BawiBtnFunc);

        if (m_Bo_Btn != null)
            m_Bo_Btn.onClick.AddListener(BoBtnFunc);

        if (m_AutoToggle != null)
            m_AutoToggle.onValueChanged.AddListener(AutoToggleFunc);
        //onValueChanged �븮���Լ�(SoundOnOff) ���

        if (m_NextStartBtn != null)
            m_NextStartBtn.onClick.AddListener(NextStartFunc);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameState == GameState.GameEnd)
        {
            m_Gameble_Text.text = "�ݾװɱ� : 0"; // + 0.ToString();
            return;
        }

        if (1.0f <= m_Gameble_Slider.value || m_Money < 100)
            m_Gameble = m_Money;
        else
            m_Gameble = 100 + (int)(m_Gameble_Slider.value * (m_Money - 100));

        m_Gameble_Text.text = "�ݾװɱ� : " + m_Gameble;

        if (m_GameState == GameState.UserPlay_Ing)
        { //������ ������ ����ϴ� ����
            m_DirecCount += (Time.deltaTime * 8.0f);
            if (3.0f <= m_DirecCount)
                m_DirecCount = 0.0f;
            ComImgChange((int)m_DirecCount);
        }
        else if (m_GameState == GameState.Result_Ing)
        { //����� ������� �ϴ� ����

            //------ ShowResult ���� ����
            if (m_AutoToggle.isOn == true) //�ڵ��϶�
            if (0.0f < m_ResultDelay)
            {
                m_ResultDelay -= Time.deltaTime;
                //Time.deltaTime : ���������� ���µ� �ɸ��� �ð� (������ ��)

                if (m_ResultDelay <= 0.0f) //Ÿ�Ӿƿ� ����
                {
                    m_GameState = GameState.UserPlay_Ing;
                    m_UesrGBB_Img.enabled = false;
                    m_ShowResult.enabled = false;
                }
            }//if (0.0f < m_ResultDelay)
            //------ ShowResult ���� ����

        }//else if (m_GameState == GameState.Result_Ing)



    } //void Update()

    void GawiBtnFunc()
    {
        //m_Result_Text.text = "��������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 1;  //1 �̸� ������ �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "����";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if(a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if(a_UserSel == 1 && a_ComSel == 3) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                                    new Color32(130, 130, 255, 255));
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble; //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                                    new Color32(255, 130, 130, 255));

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����

        Refresh_UI(a_UserSel, a_ComSel);
    } //void GawiBtnFunc()

    void BawiBtnFunc()
    {
        //m_Result_Text.text = "��������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 2;  //2 �̸� ������ �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "����";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if (a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if (a_UserSel == 2 && a_ComSel == 1) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2); //100;

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                        new Color32(130, 130, 255, 255));
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                            new Color32(255, 130, 130, 255));

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����

        Refresh_UI(a_UserSel, a_ComSel);
    }

    void BoBtnFunc()
    {
        //m_Result_Text.text = "������";
        if (m_Money <= 0)
            return;     //<-- �Լ��� ��� ���� ������ �ڵ�

        int a_UserSel = 3;  //3 �̸� ���� �ǹ���
        int a_ComSel = Random.Range(1, 4);  // 1 ~ 3 ������ ���� ���´�. (4�� ����)
        string a_strUser = "��";

        string a_strCom = "����";
        if (a_ComSel == 2)
            a_strCom = "����";
        else if (a_ComSel == 3)
            a_strCom = "��";

        //--- ����
        if (a_UserSel == a_ComSel) //��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �����ϴ�.";
        }
        else if (a_UserSel == 3 && a_ComSel == 2) //�̱� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                " �¸��ϼ̽��ϴ�.";
            m_WinCount++;
            m_Money += (m_Gameble * 2);  //100;

            DamageTxt((m_Gameble * 2), m_SpawnTxtPos.position,
                        new Color32(130, 130, 255, 255));
        }
        else //�й��� ���
        {
            m_Result_Text.text = "User(" + a_strUser + ") : Com(" + a_strCom + ")" +
                                 " �й��ϼ̽��ϴ�.";
            m_LostCount++;
            m_Money -= m_Gameble;  //200;

            DamageTxt(-m_Gameble, m_SpawnTxtPos.position,
                            new Color32(255, 130, 130, 255));

            if (m_Money <= 0)
            {
                m_Money = 0;
                m_Result_Text.text = "Game Over";
            }
        }
        //--- ����

        //--- �������� UI ����
        m_UserInfo_Text.text = "������ �����ݾ� : " + m_Money +
                                " : ��(" + m_WinCount + ")" +
                                " : ��(" + m_LostCount + ")";
        //--- �������� UI ����

        Refresh_UI(a_UserSel, a_ComSel);
    }

    void Refresh_UI(int a_U_Sel, int a_C_Sel) //����� UI ���ſ� �Լ�
    {
        a_U_Sel--;
        a_C_Sel--;

        if (a_U_Sel < 0 || m_IconImg.Length <= a_U_Sel)
            return;

        if (a_C_Sel < 0 || m_IconImg.Length <= a_C_Sel)
            return;

        if (m_UesrGBB_Img != null)
            m_UesrGBB_Img.sprite = m_IconImg[a_U_Sel];

        if (m_ComGBB_Img != null)
            m_ComGBB_Img.sprite = m_IconImg[a_C_Sel];

        m_GameState = GameState.Result_Ing;
        m_UesrGBB_Img.enabled = true;
        m_ShowResult.enabled = true;
        m_ResultDelay = 3.0f;

        //������ �¸�, �й踦 Ȯ�� �ϱ� ���ؼ� �ε��� ���� ���� 
        a_U_Sel++;
        a_C_Sel++;

        if (a_U_Sel == a_C_Sel) //���º�
        {
            m_ShowResult.color = new Color32(90, 90, 90, 255);
            m_ShowResult.text = "���º�";
        }
        else if ( (a_U_Sel == 1 && a_C_Sel == 3) ||
                    (a_U_Sel == 2 && a_C_Sel == 1) ||
                    (a_U_Sel == 3 && a_C_Sel == 2) ) //"�¸�!!"
        {
            m_ShowResult.color = new Color32(0, 0, 255, 255);
            m_ShowResult.text = "�¸�!!";
        }
        else   //�й�..
        {
            m_ShowResult.color = new Color32(255, 0, 0, 255);
            m_ShowResult.text = "�й�..";
        }

        if (m_Money <= 0)
            m_GameState = GameState.GameEnd;

    }//void Refresh_UI(int a_U_Sel, int a_C_Sel) //����� UI ���ſ� �Լ�

    void ComImgChange(int a_Sel)
    {
        if (m_ComGBB_Img == null || m_IconImg == null)
            return;

        if (a_Sel < 0 || m_IconImg.Length <= a_Sel)
            return;

        m_ComGBB_Img.sprite = m_IconImg[a_Sel];
    }//void ComImgChange(int a_Sel)

    public void AutoToggleFunc(bool value)
    { //üũ ���°� ����Ǿ��� �� ȣ��ǰ� �� �Լ�
        Text a_Label = m_AutoToggle.GetComponentInChildren<Text>();

        if (value == true) //On
        {
            if (a_Label != null)
                a_Label.text = "�ڵ�";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(false);

            if (m_GameState == GameState.GameEnd)
                return;

            m_ResultDelay = 0.0f; //���� ��� ���� �ǵ���...
            m_GameState = GameState.UserPlay_Ing;
            m_UesrGBB_Img.enabled = false;
            m_ShowResult.enabled = false;
        }
        else   //Off
        {
            if (a_Label != null)
                a_Label.text = "����";

            if (m_NextStartBtn != null)
                m_NextStartBtn.gameObject.SetActive(true);
        }
    }//public void AutoToggleFunc(bool value)

    void NextStartFunc()
    { //�ٽ����� ��ư Ŭ���� ȣ��ǰ� �� �Լ�
        if (m_GameState != GameState.Result_Ing)
            return;

        m_GameState = GameState.UserPlay_Ing;
        m_UesrGBB_Img.enabled = false;
        m_ShowResult.enabled = false;
    }

    public void DamageTxt(float a_Value, Vector3 a_TxtPos, Color a_Color)
    {
        if (m_DamagePrefab == null || m_HUD_Canvas == null)
            return;

        GameObject a_DamClone = (GameObject)Instantiate(m_DamagePrefab);
        a_DamClone.transform.SetParent(m_HUD_Canvas, false);
        a_DamClone.transform.position = a_TxtPos;

        DamageTxt a_DamageTx = a_DamClone.GetComponent<DamageTxt>();
        if (a_DamageTx != null)
            a_DamageTx.InitDamage(a_Value, a_Color);
    }

} //public class Game_Mgr : MonoBehaviour
