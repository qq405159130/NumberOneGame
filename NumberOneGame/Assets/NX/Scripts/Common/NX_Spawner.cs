using UnityEngine;
using System.Collections;

public enum NX_RangeType
{
    Sphere,
    Rectangle,
    PointArray
}
/// <summary>
/// 生成器：设置，单次生成，循环生成，
/// </summary>
public class NX_Spawner : MonoBehaviour
{

	#region === 字段 === 

    public GameObject m_prefab;
    public Transform m_parent;
    public Transform m_targetPos;
    //周期
    public float m_durationMin;
    public float m_durationMax;
    private NX_Timer m_autoSpawnTimer;

    //数量
    public int m_numMin;
    public int m_numMax;
    //范围
    public NX_RangeType m_rangeType;
    public float m_rangeMin;
    public float m_rangeMax;
    //状态
    public bool m_isEnable;
    private bool m_isInitTimer;

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
        UpdateAutoSpawn();
	}
	#endregion
	
	#region === 公有函数 === 

    public void DefaultConstruct(GameObject prefab, Transform parent, Transform targetPos)
    {
        m_prefab = prefab;
        m_parent = parent;
        m_targetPos = targetPos;
        DefaultConstruct();
    }

    void DefaultConstruct()
    {
        m_autoSpawnTimer = new NX_Timer();
        m_durationMin = 0.1f;
        m_durationMax = 0.25f;

        m_numMin = 1;
        m_numMax = 3;

        m_rangeMin = 1;
        m_rangeMax = 3;
    }
    public void SpawnOne()
    {
        Debug.Log("生成一个");
        Vector3 pos = m_targetPos.transform.position + Random.insideUnitSphere*Random.Range(m_rangeMin, m_rangeMax);
        GameObject go = GameObject.Instantiate(m_prefab, pos, Quaternion.identity) as GameObject;
        go.transform.SetParent(m_parent);
    }

    public void SpawnOne(float delayTime)
    {
        Invoke("SpawnOne", delayTime);
    }

    public void SetEnable(bool b)
    {
        if (m_isEnable != b)
        {
            m_isEnable = b;
            if (b)
            {
                m_autoSpawnTimer.Continue();
            }
            else
            {
                m_autoSpawnTimer.Pause();
            }            
        }
        //UpdateAutoSpawn();
    }

	#endregion
	
	#region === 私有函数 === 
    void UpdateAutoSpawn()
    {
        if (m_isEnable)
        {
            if (!m_isInitTimer)
            {
                Debug.Log("初始化定时器");
                m_autoSpawnTimer.Set(Random.RandomRange(m_durationMin, m_durationMax));
                m_isInitTimer = true;
            }
            if (m_autoSpawnTimer.IsTimeOut())
            {
                Debug.Log("到时，生成一个");
                int num = Random.RandomRange(m_numMin, m_numMax);
                for (int i = 0; i < num; i++)
                {
                    SpawnOne();
                }
                m_isInitTimer = false;
            }
        }
    }
	#endregion
	
	#endregion
}
