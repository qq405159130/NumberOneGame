using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICharacter
{
    
}
public partial class Character :  MonoBehaviour,ICharacter
{
    /// <summary>
    /// data
    /// </summary>
    public class Prop
    {
        public static int ConstA = 1;             
        public int      m_id = 0                ;
        public string   m_name                  ;
        public string   m_des                   ;
        public int      m_modelId               ;
        public int      m_headId                ;
        public int      m_atkType               ;
        public int      m_atkClass              ;
        public int      m_hp                    ;
        public int      m_speed                 ;
        public int      m_atkSpeed              ;
        public int      m_atkRange              ;
        public int      m_phyAtk                ;
        public int      m_magAtk                ;
        public int      m_phyDef                ;
        public int      m_magDef                ;
        public int      m_hit                   ;
        public int      m_dodge                 ;
        public int      m_crit                  ;
        public int      m_antiCrit              ;
        public int      m_Skill1Id              ;          	
        public int      m_Skill2Id              ;          	
        public int      m_Skill3Id              ;          	
        public int      m_Skill4Id              ;          	
        public int      m_Skill5Id              ;          	
        public int      m_IsBoss	            ;            
        public int      m_ArmyId	            ;            
        public int      m_ArmyCount             ;
        public int      m_hpMax                 ;

        public void SetHp(int hp)
        {
            m_hp = hp;
        }
        public void SetHp(int hp, int hpMax)
        {
            m_hp = hp;
            m_hpMax = hpMax;
        }
        public static void CopyFrom(Prop a, Prop b)
        {
            a.m_id            =    b.m_id           ;
            a.m_name          =    b.m_name         ;
            a.m_des           =    b.m_des          ;
            a.m_modelId       =    b.m_modelId      ;
            a.m_headId        =    b.m_headId       ;
            a.m_atkType       =    b.m_atkType      ;
            a.m_atkClass      =    b.m_atkClass     ;
            a.m_hp            =    b.m_hp           ;
            a.m_speed         =    b.m_speed        ;
            a.m_atkSpeed      =    b.m_atkSpeed     ;
            a.m_atkRange      =    b.m_atkRange     ;
            a.m_phyAtk        =    b.m_phyAtk       ;
            a.m_magAtk        =    b.m_magAtk       ;
            a.m_phyDef        =    b.m_phyDef       ;
            a.m_magDef        =    b.m_magDef       ;
            a.m_hit           =    b.m_hit          ;
            a.m_dodge         =    b.m_dodge        ;
            a.m_crit          =    b.m_crit         ;
            a.m_antiCrit      =    b.m_antiCrit     ;
            a.m_Skill1Id      =    b.m_Skill1Id     ;
            a.m_Skill2Id      =    b.m_Skill2Id     ;
            a.m_Skill3Id      =    b.m_Skill3Id     ;
            a.m_Skill4Id      =    b.m_Skill4Id     ;
            a.m_Skill5Id      =    b.m_Skill5Id     ;
            a.m_IsBoss	      =    b.m_IsBoss	    ;
            a.m_ArmyId	      =    b.m_ArmyId	    ;
            a.m_ArmyCount     =    b.m_ArmyCount    ;
            a.m_hpMax         =    b.m_hpMax        ;

            Debug.Log("Prop.CopyFrom()..b id :" + b.m_id + " a id:" + a.m_id);
        }

        public float MagicalDamageReduce
        {
            get
            {
                int temp = m_phyDef*ConstA;
                return (float) (temp) / (temp + 1);
            }
        }
        public float PhysicalDamageReduce
        {
            get
            {
                int temp = m_magDef * ConstA;
                return (float)(temp) / (temp + 1);
            }
        }

        public float GetAtkSpeed
        {
            get { return (float)m_atkSpeed/ StaticData.AtkSpeedScale; }
        }

        public float GetAtkRange
        {
            get { return (float)m_atkRange / StaticData.AtkRangeScale; }
        }

        public float GetSpeed
        {
            get { return (float)m_speed / StaticData.SpeedScale; }
        }

        public static float CalHitRate(int aHit,int bDodge)
        {
            return (float)(aHit + ConstA)/(aHit + bDodge + ConstA);
        }

        public static float CalCriticalRate(int aCri, int bCriResistance)
        {
            return (float) (aCri + ConstA)/(bCriResistance + aCri + ConstA);
        }

        public static float CalRealDamage(int aAttack,float bDamageReduce)
        {
            return aAttack * (1 - bDamageReduce);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">攻击方</param>
        /// <param name="b">防守方</param>
        /// <param name="atkType"></param>
        /// <param name="hitResult"></param>
        /// <returns></returns>
        public static float CalFinalDamage(Prop a, Prop b,int atkType, out int hitResult)
        {
            float f1 = Random.Range(0, 1f);
            float f2 = Random.Range(0, 1f);
            float f3 = Random.Range(0, 1f);
            hitResult = 0;
            if (f1 > CalHitRate(a.m_hit,b.m_dodge))
            {
                hitResult = 0;
                return 0;//未命中
            }else if (f2 > CalCriticalRate(a.m_crit, b.m_antiCrit))
            {
                hitResult = 1;
                //未暴击
                if (atkType == 1)
                    return CalRealDamage(a.m_phyAtk, b.PhysicalDamageReduce);
                else 
                    return CalRealDamage(a.m_magAtk, b.MagicalDamageReduce);
            }
            else
            {
                hitResult = 2;
                //暴击
                if (atkType == 1)
                    return CalRealDamage(a.m_phyAtk, b.PhysicalDamageReduce) * 2;
                else
                    return CalRealDamage(a.m_magAtk, b.MagicalDamageReduce) * 2;
            }
            //Debug.Log(string.Format("{0}/{1},"))
        }
    }

    public Prop m_prop;
    public int m_group;
    public Vector3 m_attackDir;



    //data calculate
    public void TakeDamage(int damage)
    {
        m_prop.m_hp -= damage;
        if (m_prop.m_hp < 0)
        {
            m_prop.m_hp = 0;
            //show die;
            BattleField.Instance.Remove(this, m_group);
            ChangeState(State.Death);
            m_hpBar.gameObject.SetActive(false);
        }
        //show damage
        //m_hudText.Add(damage * -1, Color.red, 1);
        m_hpBar.Set(m_prop.m_hp, m_prop.m_hpMax);
        //Debug.Log(string.Format("HP:{0}/{1}", m_prop.m_hp, m_prop.m_hpMax));
    }

    void OnAE_DeathDone()
    {
        Debug.Log("OnAE_DeathDone");
        if (IsDeath())
        {
            StartCoroutine(ShowDie());

            Destroy(m_hudText.gameObject);
            Destroy(m_hpBar.gameObject ,3);
            Destroy(gameObject, 3);
        }
    }

    IEnumerator ShowDie()
    {
        float time = 1;
        float speed = 0.2f / time;
        while (time > 0)
        {
            time += Time.deltaTime;
            transform.position -= Vector3.up * speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2);
    }
}

    //行为控制
    //状态：移动，攻击
public partial class Character
{
    public enum State
    {
        Move,
        Attack,
        Win,
        Death,
    }


    private NX_FsmN1 m_fsm = new NX_FsmN1();
    [SerializeField]private State m_state = State.Move;
    
    private HUDText m_hudText;
    private Transform m_hudFollowTarget;
    private HpBar m_hpBar;
    private Transform m_hpBarFollowTarget;



    private Animator m_animator;
    private string m_curAnimName;
    void Start()
    {
        BattleField.Instance.Add(this, m_group);
        m_animator = GetComponentInChildren<Animator>();
        transform.LookAt(transform.position + m_attackDir);

        InitHud();
        InitHpBar();
        m_attackTimer.Set(1 / m_prop.GetAtkSpeed, true);
    }

    void InitHud()
    {
        //m_hudFollowTarget = transform.FindChild("HudFollowTarget");
        GameObject go = new GameObject();
        m_hudFollowTarget = go.transform;
        m_hudFollowTarget.name = "HudFollowTarget";
        m_hudFollowTarget.SetParent(transform);
        m_hudFollowTarget.position = transform.position + new Vector3(0, 2.8f, 0);

        Object hudTextPrefab = Resources.Load("HUDText", typeof(GameObject));
        GameObject hudTextGo = NGUITools.AddChild(UIFollowManager.Instance.m_HudTextParent, (GameObject)hudTextPrefab) as GameObject;//注意，Load进来的是Object，需显示转为GameObject才能被NGUITools进行AddChild操作
        UIFollowTarget hudFollowTarget = hudTextGo.GetComponent<UIFollowTarget>();
        hudFollowTarget.target = m_hudFollowTarget;
        hudFollowTarget.gameCamera = Camera.main;
        m_hudText = hudTextGo.GetComponent<HUDText>();
    }

    void InitHpBar()
    {
        GameObject go = new GameObject();
        m_hpBarFollowTarget = go.transform;
        m_hpBarFollowTarget.name = "hpBarFollowTarget";
        m_hpBarFollowTarget.SetParent(transform);
        m_hpBarFollowTarget.position = transform.position + new Vector3(0, 2.1f, 0);

        Object hpBarPrefab = Resources.Load("HpBar", typeof (GameObject));
        GameObject hpBarGo = NGUITools.AddChild(UIFollowManager.Instance.m_HpBarParent,(GameObject) hpBarPrefab) as GameObject;
        UIFollowTarget hudFollowTarget = hpBarGo.GetComponent<UIFollowTarget>();
        hudFollowTarget.target = m_hpBarFollowTarget;
        hudFollowTarget.gameCamera = Camera.main;
        m_hpBar = hpBarGo.GetComponent<HpBar>();

        m_hpBar.Set(m_prop.m_hp, m_prop.m_hpMax);
    }
    void Update()
    {
        UpdateFsm();
    }

    void OnDrawGizmos()
    {
        
    }
    private bool m_isAttackNow;
    private Transform m_curTarget;
    private Character m_curTargetScript;
    private NX_Timer m_attackTimer = new NX_Timer();
    private float m_atkHitTime = 0.5f;
    public void UpdateFsm()
    {
        switch (m_state)
        {
            case State.Move:
                if (m_fsm.m_IsChangeState)
                {
                }
                if (!BattleField.Instance.IsStart())
                {
                    KeepCrossFade("stand", 0.1f, 0);
                    return;
                }
                if (m_curTargetScript == null || m_curTargetScript.IsDeath())
                {
                    m_curTarget = BattleField.Instance.FindCloseEnemy(transform.position,m_group);
                    if (m_curTarget)
                        m_curTargetScript = m_curTarget.GetComponent<Character>();
                    else if (BattleField.Instance.IsEnd())
                    {
                        ChangeState(State.Win);
                    }

                    KeepCrossFade("stand", 0.1f, 0);
                }
                else
                {
                    float dis = Vector3.Distance(m_curTarget.position, transform.position);
                    if (dis < m_prop.GetAtkRange && IsFaceTo(m_curTarget,30))//进入攻击范围
                    {
                        ChangeState(State.Attack);
                    }else
                        if (dis < m_prop.GetAtkRange + 5)//进入视野范围
                    {
                        MoveTo(m_curTarget);
                        RotateTo(m_curTarget);
                    }
                    else
                    {
                        StraightMove();
                    }
                }
                break;
            case State.Attack:
                if (m_fsm.m_IsChangeState)
                {
                    m_isAttackNow = false;
                    //m_attackTimer.Set(1 / m_prop.GetAtkSpeed, true);
                    m_animator.CrossFade("stand", 0.1f, 0);
                }
                if (m_curTargetScript == null || m_curTargetScript.IsDeath())
                {
                    ChangeState(State.Move);
                }
                else
                {
                    if (!IsFaceTo(m_curTarget, 15))
                    {
                        RotateTo(m_curTarget);
                    }
                    else if (m_attackTimer.IsTimeOut())
                    {
                        //Debug.Log("攻击");
                        float dis = Vector3.Distance(m_curTarget.position, transform.position);
                        if (dis < m_prop.GetAtkRange + 1) //多出一点，以防止状态的剧烈震荡
                        {
                            //attack
                            m_isAttackNow = true;
                            m_animator.SetTrigger("attack");
                        }
                        else
                        {
                            ChangeState(State.Move);
                        }
                    }
                }
                break;
            case State.Win:
                KeepCrossFade("shengli", 0.1f, 0);
                break;
            case State.Death:
                OnceCrossFade("death", 0.1f, 0);
                break;
        }
    }
    void OnAE_Fire()
    {
        //m_curTarget = BattleField.Instance.FindCloseEnemy(transform.position, m_group);
        if (m_curTarget != null)
        {
            //Debug.Log("攻击伤害");
            if (m_prop.m_atkClass == 1)
            {
                //Debug.Log("近战攻击伤害");
                float dis = Vector3.Distance(m_curTarget.position, transform.position);
                if (dis < m_prop.GetAtkRange + 1) //多出一点，以防止状态的剧烈震荡
                {
                    Character c = m_curTarget.GetComponent<Character>();
                    int hitResult;
                    int damage = (int)Character.Prop.CalFinalDamage(m_prop, c.m_prop, m_prop.m_atkType, out hitResult);
                    c.TakeDamage(damage);
                    Vector3 pos = c.transform.position + Vector3.up * 2.5f + Random.insideUnitSphere * 0.2f;
                    c.ShowDamageText(pos, damage, hitResult);
                }
            }
            else if (m_prop.m_atkClass == 2)
            {
                //Debug.Log("远程攻击伤害");
                Character c = m_curTarget.GetComponent<Character>();
                int hitResult;
                int damage = (int)Character.Prop.CalFinalDamage(m_prop, c.m_prop, m_prop.m_atkType, out hitResult);

                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bullet.transform.position = transform.position + Vector3.up;
                bullet.transform.localScale = Vector3.one * 0.4f;
                StartCoroutine(ThrowBullet(bullet.transform, 10, c, damage, hitResult));
            }
        }
    }

    IEnumerator ThrowBullet(Transform  bullet,float speed,Character target , int damage , int hitResult)
    {
        Transform targetTrans = target.transform;
        while (true)
        {
            if (targetTrans == null)
                break;
            Vector3 offset = targetTrans.position - bullet.position;
            if (offset.magnitude < 0.5f)
                break;
            bullet.transform.position += offset.normalized * speed * Time.deltaTime;
            bullet.LookAt(targetTrans);
            yield return 0;
        }
        if (targetTrans != null && target != null)
        {
            target.TakeDamage(damage);
            Vector3 pos = target.transform.position + Vector3.up * 2.5f + Random.insideUnitSphere * 0.2f;
            target.ShowDamageText(pos, damage, hitResult);
            Debug.Log("远程攻击伤害2");
        }
        Destroy(bullet.gameObject);
    }
    public void ShowDamageText(Vector3 pos, int damage, int hitResult)
    {
        switch (hitResult)
        {
            case 0:
                ShowMiss(pos);
                break;
            case 1:
                //m_hudText.Add(damage * -1, Color.white, 1);
			ShowText(Vector3.up * 2,"-" + damage,20,Color.white);
                break;
            case 2:
                //m_hudText.Add(damage * -1, Color.red, 1);
			ShowText(Vector3.up * 4,"-" + damage,40,Color.red);
                break;
        }
    }

    void ShowMiss(Vector3 pos)
    {
        GameObject go;
        //go =NGUITools.AddChild(UIFollowManager.Instance.m_HudTextParent) as GameObject;
        UISprite sprite = NGUITools.AddSprite(UIFollowManager.Instance.m_HudTextParent, UIFollowManager.Instance.m_atlas, "AtkMiss01", 10);
        go = sprite.gameObject;
        go.name = "Miss";
        go.transform.localPosition = NGUIMath.WorldToLocalPoint(pos, Camera.main, UIFollowManager.Instance.m_uiCamera,
            UIFollowManager.Instance.m_HudTextParent.transform);
        go.transform.localScale *= 0.5f;
		iTween.ScaleFrom(go, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutBounce));
        Destroy(go, 1);
    }
	void ShowText(Vector3 pos,string text, int size, Color color)
	{
		GameObject go;
		Object prefab = Resources.Load ("Label",typeof(GameObject));
		go =NGUITools.AddChild(m_hpBar.gameObject,(GameObject) prefab) as GameObject;
		//UISprite sprite = NGUITools.AddSprite(UIFollowManager.Instance.m_HudTextParent, UIFollowManager.Instance.m_atlas, "AtkMiss01", 10);
		//go = sprite.gameObject;
		//go.name = "Miss";
		go.transform.localPosition = pos;
		//go.transform.localScale *= 0.5f;
		UILabel label = go.GetComponent<UILabel> ();
		label.text = text;
		label.fontSize = size;
		label.color = color;

		iTween.ScaleFrom(go, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "easetype", iTween.EaseType.easeOutBounce));
		Destroy(go, 1);
	}
    public void ChangeState(State state)
    {
        m_state = state;
        m_fsm.m_IsChangeState = true;
    }

    public bool IsDeath()
    {
        return m_state == State.Death;
    }
    public void InitDir()
    {
        transform.LookAt(transform.position + m_attackDir);
    }
    void StraightMove()
    {
        transform.LookAt(transform.position + m_attackDir);
        transform.position += transform.forward * m_prop.GetSpeed * Time.deltaTime;

        KeepCrossFade("run", 0.1f, 0);
    }
    void MoveTo(Transform target)
    {
        Vector3 offset = target.position - transform.position;
        transform.position += offset.normalized * m_prop.GetSpeed * Time.deltaTime;

        KeepCrossFade("run", 0.1f, 0);
    }

    void KeepCrossFade(string animName,float fadeTime,int Layer)
    {
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName(animName) && !m_animator.IsInTransition(0))
        {
            m_animator.CrossFade(animName, fadeTime, Layer, Time.deltaTime);
        }else if (!m_animator.IsInTransition(0) && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            m_animator.CrossFade(animName, fadeTime, Layer, Time.deltaTime);
        }
    }
    void OnceCrossFade(string animName, float fadeTime, int Layer)
    {
         if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName(animName) && !m_animator.IsInTransition(0))
        {
            m_animator.CrossFade(animName, fadeTime, Layer);
        }else  if (!m_animator.IsInTransition(0) && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            m_animator.CrossFade(animName, fadeTime, Layer);
        }
    }
    void RotateTo(Transform target)
    {
        Quaternion pre = transform.rotation;
        transform.LookAt(target);
        transform.rotation = Quaternion.Slerp(pre, transform.rotation, 10f * Time.deltaTime);
    }

    bool IsFaceTo(Transform target,float angleView)
    {
        return Vector3.Angle(transform.forward, target.position - transform.position) < angleView;
    }

    bool IsCloseTo(Transform target, float disThreshold)
    {
        return Vector3.Distance(target.position, transform.position) < disThreshold;
    }

    
}