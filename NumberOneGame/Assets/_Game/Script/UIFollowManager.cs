using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowManager : MonoBehaviour
{
    public static UIFollowManager Instance;
    public GameObject m_HudTextParent;
    public GameObject m_HpBarParent;
    public Camera m_uiCamera;

    public UIAtlas m_atlas;

    void Awake()
    {
        Instance = this;

    }
}
