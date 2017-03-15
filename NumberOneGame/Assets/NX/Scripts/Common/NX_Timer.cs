using UnityEngine;
using System.Collections;

///
///
///

/// <summary>
/// 定时器功能：设置，暂停，继续，重启
/// </summary>

public class NX_Timer
{
    //寄存器
    private bool _isInit;//似乎没用了
    //设置
    private float _randomMin;
    private float _randomMax;
    private bool _isRandomTime;//设置重启的时候是否重设周期
    private float _duration;
    private bool m_isLoop;
    private bool m_isEnable;
    //暂存量
    private float m_progressSave;
    //实时量
    private float _startTime;//set：启动，继续，读取到时
    private float m_progress;
    private float m_remain;

    public float m_Duration
    {
        get { return _duration; }
    }

    public bool m_IsRandomTime
    {
        get { return _isRandomTime; }
        set { _isRandomTime = value; }
    }

    public float m_RandomMax
    {
        get { return _randomMax; }
        set { _randomMax = value; }
    }

    public float m_RandomMin
    {
        get { return _randomMin; }
        set { _randomMin = value; }
    }

    public bool m_IsInit
    {
        get { return _isInit; }
        set { _isInit = value; }
    }

    public float m_StartTime
    {
        get { return _startTime; }
    }

    public float GetProgress()
    {
        if (!m_isEnable)
        {
            return m_progressSave;
        }
        m_progress = Time.time - _startTime;
        if (m_progress >= _duration)
        {
            m_progress = _duration;
        }
        return m_progress;
    }

    public float GetRemain()
    {
        m_remain = _duration - GetProgress();

        return m_remain;
    }
    public void Set(float daration)
    {
        m_isEnable = true;
        _duration = daration;
        _startTime = Time.time;
    }
    public void Set(float daration,bool isLoop)
    {
        m_isLoop = isLoop;
        m_isEnable = true;
        _duration = daration;
        _startTime = Time.time;
    }
    public void Set(float daration, float randomMin, float randomMax,bool isLoop = true,bool isRandomTime = true)
    {
        m_isEnable = true;
        _duration = daration;
        _startTime = Time.time;

        m_RandomMin = randomMin;
        m_RandomMax = randomMax;
        m_isLoop = isLoop;
        m_IsRandomTime = isRandomTime;
    }

    public bool IsTimeOut()
    {
        if (m_isEnable)
        {
            if (GetProgress() >= _duration)
            {
                if (m_isLoop)
                {
                    m_isEnable = true;
                    _startTime = Time.time;

                    m_progressSave = 0;//需要吗?

                    if (_isRandomTime)
                    {
                        _duration = Random.Range(_randomMin, _randomMax);
                    }
                }
                else
                {
                    m_isEnable = false;
                    m_progressSave = 0;
                }
                return true;
            }
        }
        return false;
    }

    public void Pause()
    {
        if (m_isEnable)
        {
            m_isEnable = false;
            m_progressSave = Time.time - _startTime;
            Debug.Log("暂停");
        }
    }

    public void Continue()
    {
        if (!m_isEnable)
        {
            m_isEnable = true;
            Debug.Log("继续");
            _startTime = Time.time - m_progressSave;
        }
       
    }

    public void SetEnable(bool b)
    {
        if (m_isEnable != b)
        {
            m_isEnable = b;
            if (b)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ReStart()
    {
        m_isEnable = true;
        _startTime = Time.time;
    }

    public void SetLoop(bool b)
    {
        m_isLoop = b;
    }
}
