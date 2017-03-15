using UnityEngine;
using System.Collections;

public class NX_SpawnerTest : MonoBehaviour {

	#region === 字段 === 

    private NX_Spawner m_spawner;
    public GameObject m_prefab;
    public Transform m_parent;
    public Transform m_targetPos;
    private string m_prefabPathStr = "TestCube";
    private float m_delaySpawnOne;
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
        GUILayout.BeginHorizontal();
        GUILayout.Label("加载资源:");
        m_prefabPathStr = GUILayout.TextField(m_prefabPathStr);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("生成对象：");
        if (m_prefab)
        {
            GUILayout.Label(m_prefab.name);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("父对象：");
        if (m_parent)
        {
            GUILayout.Label(m_parent.name);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("位置：");
        if (m_targetPos)
        {
            GUILayout.Label(m_targetPos.name);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("初始化方法二：组件"))
        {
            Set2();
        }
        if (m_spawner != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("定时n秒:");

            m_delaySpawnOne = float.Parse(GUILayout.TextField(m_delaySpawnOne.ToString(), 3));
            if (GUILayout.Button("生成一个"))
            {
                m_spawner.SpawnOne(m_delaySpawnOne);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续生成：");
            if (GUILayout.Button("继续"))
            {
                m_spawner.SetEnable(true);
            }
            if (GUILayout.Button("暂停"))
            {
                m_spawner.SetEnable(false);
            }
            GUILayout.EndHorizontal();
        }
    }
	#endregion
	
	#region === 公有函数 === 

    public void Set2()
    {
        m_prefab = Resources.Load("TestCube") as GameObject;
        m_parent = this.transform;
        m_targetPos = this.transform;

        m_spawner = this.gameObject.AddComponent<NX_Spawner>();
        m_spawner.DefaultConstruct(m_prefab,m_parent,m_targetPos);

        //m_spawner.SetAutoSpawn(true);
    }
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion
}
