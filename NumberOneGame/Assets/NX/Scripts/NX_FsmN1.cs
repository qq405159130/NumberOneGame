using System;
using UnityEngine;
using System.Collections;
/// <summary>
/// New一个进行使用。
/// 需自己新建ChangeState和UpdateFsm函数，本类仅做Fsm的属性容器而已；
/// 属性包括：Timer、Phase、IsChange。
/// 未包括的内容：Enum、curState、ChangeState()、UpdateFsm()
/// </summary>
public class NX_FsmN1
{
    public NX_Timer m_timer = new NX_Timer();
    public int m_phase;
    bool _isChangeState;

    public bool m_IsChangeState
    {
        get
        {
            bool b = _isChangeState;
            _isChangeState = false;
            return b;
        }
        set { _isChangeState = value; }
    }

    public NX_FsmN1()
    {
        _isChangeState = true;
        m_phase = 0;
        if(m_timer == null)
            m_timer = new NX_Timer();
    }
}
