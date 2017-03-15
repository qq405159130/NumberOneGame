using UnityEngine;
using System.Collections;
using System.Timers;

public class NX_SpawnObject : MonoBehaviour {


    //出现	动画，特效，音效，时长,姿态	
		
    //互动：爆炸	动画，特效，音效，时长，事件	
    //各类事件：延时，		
		
    //自然消失	动画，特效，音效，时长	
    public GameObject m_initAm;
    public GameObject m_initFx;
    public AudioClip m_initAc;
    public Timer m_initTimer;
    public Transform m_initTrans;


	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

    public void Init()
    {
        
    }

    public void Death()
    {
        
    }

    public void Explode()
    {
        //播放特效 动画 音效 
        //销毁
    }
}
