using System;
using UnityEngine;
using System.Collections;

public class NX_TimerTest : MonoBehaviour {

	#region === 字段 === 

    private NX_Timer m_timer;
    private float m_duration = 5;
    private bool m_isLoop;

	#endregion
	
	#region === 属性 === 
	
	#endregion
	
	#region === 方法 === 
	
	#region === Unity默认函数 === 
	void Awake () 
	{
		
	}
	
	void Start ()
	{
	  
	}
	
	void Update () 
	{
	    
	}

    void OnGUI()
    {
        m_duration = int.Parse(GUILayout.TextField(m_duration.ToString(), 3));
        if (GUILayout.Button("创建定时器"))
        {
            NewTimer();
        }
        if (m_timer != null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("启动时刻：" + m_timer.m_StartTime);
            GUILayout.Label("定时器进度：" + m_timer.GetProgress());
            GUILayout.Label("定时器剩余：" + m_timer.GetRemain());
            GUILayout.Label("定时器时间:" + m_timer.m_Duration);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("是否重选周期:" + m_timer.m_IsRandomTime))
            {
                m_timer.m_IsRandomTime = !m_timer.m_IsRandomTime;
            }
            if (m_timer.m_IsRandomTime)
            {
                GUILayout.Label(string.Format("重选周期为：{0}（{1} ~ {2}）", m_timer.m_Duration, m_timer.m_RandomMin,
               m_timer.m_RandomMax));
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("重启"))
            {
                m_timer.ReStart();
            }
            if (GUILayout.Button("暂停"))
            {
                m_timer.Pause();
            }
            if (GUILayout.Button("继续"))
            {
                m_timer.Continue();
            }
            if (m_isLoop)
            {
                if (GUILayout.Button("循环"))
                {
                    m_timer.SetLoop(false);
                    m_isLoop = false;
                }
            }
            else
            {
                if (GUILayout.Button("单次"))
                {
                    m_timer.SetLoop(true);
                    m_isLoop = true;
                }
            }
            if (m_timer.IsTimeOut())
            {
                Debug.Log("到时");
                GUILayout.Button("到时！");
            }
           
        }
    }

    
	#endregion
	
	#region === 公有函数 === 
    [ContextMenu("创建 定时器")]
    public void NewTimer()
    {
        m_timer = new NX_Timer();
        m_timer.Set(m_duration);

        m_timer.m_RandomMin = 1f;
        m_timer.m_RandomMax = 3f;
    }

    [ContextMenu("取得定时器 进度")]
    public void GetTiemrProgress()
    {
        Debug.Log("定时器进度:" + m_timer.GetProgress());
    }
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion
}
