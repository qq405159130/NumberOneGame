using UnityEngine;
using System.Collections;

public class HudTextParent : MonoBehaviour {
    public static HudTextParent Instance;
    void Awake()
    {
        Instance = this;
    }
}
