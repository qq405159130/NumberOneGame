using UnityEngine;
using System.Collections;

public class NX_SpawnFormTest : MonoBehaviour {

	#region === 字段 === 

    private NX_SpawnerForm m_spawnerForm;

    public GameObject m_formParent;
    private string m_formParentPath = "SpawnForm";
    public string m_spawnPointClassStr = "NX_SpawnerForm";
    private float m_spawnGapTime = 0.3f;

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
        m_formParentPath = GUILayout.TextField(m_formParentPath.ToString());
       
        m_spawnPointClassStr = GUILayout.TextField(m_spawnPointClassStr.ToString());

        if (GUILayout.Button("阵型构造器 初始化"))
        {
            m_spawnerForm = this.gameObject.AddComponent<NX_SpawnerForm>();
            m_formParent = Resources.Load(m_formParentPath, typeof (GameObject)) as GameObject;
            m_spawnerForm.DefaultConstruct(m_formParent,m_spawnPointClassStr);
        }
        if (m_spawnerForm)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("每一间隔的刷新时间:");
            //m_spawnGapTime = int.Parse(GUILayout.TextField(m_spawnGapTime.ToString(), 3));
            m_spawnerForm.m_spawnGapTime = m_spawnGapTime;
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("启动"))
            {
                m_spawnerForm.SetEnable(true);
            }
            if (GUILayout.Button("暂停"))
            {
                m_spawnerForm.SetEnable(false);
            }
        }
    }
	#endregion
	
	#region === 公有函数 === 
	
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion
}
