using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class NX_SpawnerForm : MonoBehaviour {

	#region === 字段 === 

    private bool m_isEnable;

    public GameObject m_formParent;
    public string m_spawnPointClassStr;
    public List<List<GameObject>> m_formList = new List<List<GameObject>>();
    public List<GameObject>[] m_formArray;
    private int m_x;
    private int m_y;
    //private int m_xMax;
    private int m_yMax;


    public bool m_isIgnoreEmpty = true;//忽略刷新数组中空的数组

    public float m_spawnGapTime = 0.02f;
    private NX_Timer m_spawnGapTimer = new NX_Timer();
    private int m_curSpawnIndex;
    
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
	    UpdateSpawn();
	}
	#endregion
	
	#region === 公有函数 === 

    public void DefaultConstruct(GameObject formParent,string spawnPointClassStr)
    {
        m_formParent = formParent;
        m_spawnPointClassStr = spawnPointClassStr;

        InitFormList();


        #region

        //Type type = Type.GetType(spawnPointClassStr);
        ////m_formParent.GetComponents<>();
        //Debug.Log("type is class?" + type.IsClass);
        //Debug.Log(type.Name);
        //Debug.Log(type.GetType());

        //Type type2 = typeof (NX_Coin);
        //Debug.Log(type2);
        //Debug.Log(type2.GetType());

        //Assembly assem = Assembly.GetExecutingAssembly();

        //Debug.Log("程序集全名:" + assem.FullName);
        //Debug.Log("程序集的版本：" + assem.GetName().Version);
        //Debug.Log("程序集初始位置:" + assem.CodeBase);
        //Debug.Log("程序集位置：" + assem.Location);
        //Debug.Log("程序集入口：" + assem.EntryPoint);
        //Type[] types = assem.GetTypes();
        //Debug.Log("程序集下包含的类型:");
        //foreach (var item in types)
        //{
        //    Debug.Log("类：" + item.Name);
        //}

        #endregion
    }

    public void SetEnable(bool b)
    {
        m_isEnable = b;
    }
    
   
	#endregion
	
	#region === 私有函数 === 
    private void InitFormList()
    {
        GameObject parentGo = Instantiate(m_formParent, this.transform.position, Quaternion.identity) as GameObject;
        NX_SpawnPoint[] SPs = parentGo.GetComponentsInChildren<NX_SpawnPoint>();

        ///交错数组
        //遍历得到最大y轴
        foreach (var sp in SPs)
        {
            if (sp.m_spawnOrder > m_yMax)
            {
                m_yMax = sp.m_spawnOrder;
            }
        }
        m_yMax += 1;
        Debug.Log("当前m_yMax : " + m_yMax);
        m_formArray = new List<GameObject>[m_yMax];//TODO   
        //遍历得到各y轴最大x
        foreach (var sp in SPs)
        {
            Debug.Log("正在查找 y: " + sp.m_spawnOrder);
            if (m_formArray[sp.m_spawnOrder] == null)
                m_formArray[sp.m_spawnOrder] = new List<GameObject>();
            m_formArray[sp.m_spawnOrder].Add(sp.gameObject);

            sp.gameObject.SetActive(false);
        }
    }
    void UpdateSpawn()
    {
        if (m_isEnable)
        {
            m_spawnGapTimer.Continue();
            if (!m_spawnGapTimer.m_IsInit)
            {
                m_spawnGapTimer.m_IsInit = true;
                m_spawnGapTimer.Set(m_spawnGapTime);
                //Debug.Log("初始化定时器，准备下一批");
            }
            if (m_spawnGapTimer.IsTimeOut())
            {
                if (m_curSpawnIndex == m_formArray.Length)
                {
                    return;
                }
                //Debug.Log("到时，刷新一批");
                if (m_formArray[m_curSpawnIndex] == null)
                {
                    //Debug.Log("本批为空，移动到下一批");
                    m_curSpawnIndex++;
                }
                else
                {
                    //Debug.Log("本批不为空,激活本批");
                    //交错数组
                    int i = 0;
                    foreach (var sp in m_formArray[m_curSpawnIndex])
                    {
                        //Debug.Log("某批第?个:" + i);
                        i++;
                        sp.gameObject.SetActive(true);
                        m_curSpawnIndex++;
                    }
                }
                m_spawnGapTimer.m_IsInit = false;
            }
        }
        else
        {
            m_spawnGapTimer.Pause();
        }
    }
	#endregion
	
	#endregion
}
