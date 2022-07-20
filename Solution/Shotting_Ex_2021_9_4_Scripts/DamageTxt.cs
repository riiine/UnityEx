using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
    float m_EffTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_EffTime -= Time.deltaTime;

        if (m_EffTime <= 0.0f)
            Destroy(this.gameObject);
    }

    public void InitDamage(float a_Damage, Color a_Color)
    {
        Text a_ThisText = this.GetComponentInChildren<Text>();
        a_ThisText.text = "- " + (int)a_Damage; 
        a_ThisText.color = a_Color;
    }
}
