using UnityEngine;
using System.Collections;

public class NX_DisableTime : MonoBehaviour
{

    public float m_time;

    void Start()
    {
        Invoke("Disable", m_time);
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
