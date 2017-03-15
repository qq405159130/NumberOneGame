using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    public UISlider m_slider;
    public UILabel m_text;

    public void Set(float v1, float v2)
    {
        m_slider.value = v1 / v2;
        m_text.text = v1 + "/" + v2;
    }
}
