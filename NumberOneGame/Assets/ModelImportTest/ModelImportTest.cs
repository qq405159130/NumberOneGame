using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelImportTest : MonoBehaviour
{
    public string[] m_animNames = new string[]
    {
        "(fbx)",
        "(fbx)_es1(1cm=001m)",
        "(fbx)_es2(1=001)",
        "(fbx)_es2(1=100)",
        "(fbx)_mu(m~m)",
        "(max)"
    };
    [SerializeField,Range(0,1f)]private float _crossfadeLength;

    private int _curAnimIndex;
    private Animator m_animator;
    private string _curAnim;


	void Start ()
	{
	    m_animator = GetComponent<Animator>();
	}
	
	void Update () {
        float curAnimProgressTime = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (!m_animator.IsInTransition(0) && curAnimProgressTime >= 1)
        {
            //最后一个参数若为0，则跟Animation相比，每播放一遍都会落后一帧
            //最后一个参数若为Time.deltaTime，则跟Animation相比，每一帧的起始结束帧的播放时刻是一致的，视觉上有区别的地方在于Animator起始的一两帧处于融合阶段，视觉位置上回落后一点点；
            m_animator.CrossFade(_curAnim, _crossfadeLength, 0, Time.deltaTime);
        }
	    if (Input.GetMouseButtonDown(0))
	    {
            _curAnimIndex++;
            _curAnimIndex %= m_animNames.Length;
	        if (!string.IsNullOrEmpty(m_animNames[_curAnimIndex]))
	        {
                _curAnim = m_animNames[_curAnimIndex];
	            Debug.Log(string.Format("切换到动作{0} {1}", _curAnimIndex, _curAnim));
	        }
	    }
	}
   
}
