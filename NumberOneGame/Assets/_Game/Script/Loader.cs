using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    void Awake()
    {
        CharacterPropInfo.ReadInfo();
    }

	void Start ()
	{
	    
	}
	
	void Update ()
	{
	    NX_ManagerUtilty.AdjustTimeScale();
	}
}

public static class StaticData
{
    public const int SpeedScale = 1000;
    public const int AtkRangeScale = 1000;
    public const int AtkSpeedScale = 1000;
}