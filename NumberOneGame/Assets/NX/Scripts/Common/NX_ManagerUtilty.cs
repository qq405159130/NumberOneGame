using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NX_ManagerUtilty {

    public static void AdjustTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale *= 2;
            if (Time.timeScale > 8)
                Time.timeScale = 8;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale /= 2;
            if (Time.timeScale < 0.25f)
                Time.timeScale = 0.25f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            Time.timeScale = 0.25f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 0;
        }
        if (Input.anyKeyDown)
        {
            Debug.Log("timescale:" + Time.timeScale);
        }
    }
}
