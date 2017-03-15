using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour {

    public enum AnimType
    {
        Animation,
        Animator,
    }

    public AnimType m_animType;
    public enum State
    {
        stand   ,
        run     ,
        atk     ,
        critAtk ,
        death   ,
        skill1  ,
        skill2  ,
        skill3  ,
    }
    [SerializeField, Range(0, 1f)]
    private float m_crossfadeLength = 0.1f;

    //待机，移动，攻击，暴击，死亡，技能1，技能2，技能3
    private string m_stand  = "stand";
    private string m_run     = "run";
    private string m_atk     = "pingci";
    private string m_critAtk = "pikan";
    private string m_death  = "death";
    private string m_skill1 = "chongfeng";
    private string m_skill2 = "chongfeng";
    private string m_skill3 = "chongfeng";

    private State _state = State.stand;
    private Animation m_animation;
    private Animator m_animator;

    public State GetState
    {
        get { return _state; }
    }

    private string _curAnim;

    void Start ()
    {
        m_animation = GetComponent<Animation>();
        m_animator = GetComponent<Animator>();
        _curAnim = _curAnim ?? m_stand;

        
    }

    void Update ()
    {
        UpdateState();

        ControlTest();
    }

    public void ChangeState(State newState)
    {
        if (_state != newState)
        {
            _state = newState;

            switch (_state)
            {
                case State.stand: _curAnim = m_stand;     break;
                case State.run: _curAnim = m_run;         break;
                case State.atk: _curAnim = m_atk;         break;
                case State.critAtk: _curAnim = m_critAtk; break;
                case State.death: _curAnim = m_death;     break;
                case State.skill1: _curAnim = m_skill1;   break;
                case State.skill2: _curAnim = m_skill2;   break;
                case State.skill3: _curAnim = m_skill3;   break;
                //case State.skill3    : break;
            }
        }
    }

    public void UpdateState()
    {
        switch (_state)
        {
            case State.stand     : ;  break;
            case State.run       : 
                transform.position   += transform.forward * 3 * Time.deltaTime;  break;
            case State.atk       : ;  break;
            case State.critAtk   : ;  break;
            case State.death     : ;  break;
            case State.skill1    : ;  break;
            case State.skill2    : ;  break;
            case State.skill3    : ;  break;
            //case State.skill3    : break;
        }
        if (m_animType == AnimType.Animation)
        {
            //这个不加crossfadeLength，就是动作切换闪动的缘由！加了之后，Animation和Animator的表现基本一致（测试了0.1，视觉上一致，测试了1，视觉上有不一致的）
            //m_animation.CrossFade(m_curAnim);
            
            //注意，crossfadeLength = 0 和不加crossfadeLength的效果是不一样的。前者完全没有切换帧，后者有切换帧但还是有闪动（可能是测试的某两个动作幅度太大）；
            m_animation.CrossFade(_curAnim, m_crossfadeLength);
        }
        else
        {
            int typeIndex = 3;
            switch (typeIndex)
            {
                case 0:
                    //No 0 只动了一帧
                    m_animator.CrossFade(_curAnim, m_crossfadeLength, 0);
                    break;
                case 1:
                    //No 1 只能持续播完一个动作
                    if (!m_animator.IsInTransition(0))
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0);
                    break;
                case 2:
                    //No 2 只能持续播完一个动作
                    if (!m_animator.IsInTransition(0) && !m_animator.GetAnimatorTransitionInfo(0).IsName(_curAnim))
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0);
                    break;
                case 3:
                    //Yes 3 持续播完一个动作，并能持续下一遍，且能相应切换，视觉无任何问题。
                    float curAnimProgressTime = m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    if (!m_animator.IsInTransition(0) && curAnimProgressTime >= 1)
                    {
                        //若无最后一个参数，则貌似是不行？？
                        //最后一个参数若为0，则跟Animation相比，每播放一遍都会落后一帧
                        //最后一个参数若为Time.deltaTime，则跟Animation相比，每一帧的起始结束帧的播放时刻是一致的，视觉上有区别的地方在于Animator起始的一两帧处于融合阶段，视觉位置上回落后一点点；
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0, Time.deltaTime);
                    }
                    break;
                case 4:
                    //No 4 只能持续播放一个动作，如果对需要持续的动作进行Loop选项的勾选，则貌似还可以。
                    if (!m_animator.GetAnimatorTransitionInfo(0).IsName(_curAnim) && !m_animator.IsInTransition(0))
                    {
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0);
                    }
                    break;

                case 5:
                    //综合？
                    if (!m_animator.GetAnimatorTransitionInfo(0).IsName(_curAnim) && !m_animator.IsInTransition(0))//保证快速进入？
                    {
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0);
                    }
                    else if (!m_animator.IsInTransition(0) && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)//保证持续下去？
                    {
                        m_animator.CrossFade(_curAnim, m_crossfadeLength, 0, Time.deltaTime);
                    }
                    break;
            }
            

        }
        //Animation Animator比较
        //Animation有isPlay，可以不设置loop，而直接CrossFade当前，形成持续动画；
        //Animator有speed，以及单个state的speedMultiply
        //另外，如果给Animator中除stand以外的动作都连一个结束的线到stand，则切换长的动作表现正常（0.5秒切1.2秒），切换短的动作则有明显的跳动（0.5秒切0.5秒）

    }

    float GetClipLength(string curAnim)
    {
        AnimatorClipInfo[] acs = m_animator.GetCurrentAnimatorClipInfo(0);
        foreach (var ac in acs)
        {
            if (ac.clip.name == curAnim)
                return ac.clip.length;
        }
        return -1;
    }
    void ControlTest()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) ChangeState(State.stand      );
        if (Input.GetKeyDown(KeyCode.Keypad1)) ChangeState(State.run        );
        if (Input.GetKeyDown(KeyCode.Keypad2)) ChangeState(State.atk        );
        if (Input.GetKeyDown(KeyCode.Keypad3)) ChangeState(State.critAtk    );
        if (Input.GetKeyDown(KeyCode.Keypad4)) ChangeState(State.death      );
        if (Input.GetKeyDown(KeyCode.Keypad5)) ChangeState(State.skill1     );
        if (Input.GetKeyDown(KeyCode.Keypad6)) ChangeState(State.skill2     );
        if (Input.GetKeyDown(KeyCode.Keypad7)) ChangeState(State.skill3     );
//        if (Input.GetKeyDown(KeyCode.Keypad8)) ChangeState(State.    );
//        if (Input.GetKeyDown(KeyCode.Keypad9)) ChangeState(State.    );
    }
}
