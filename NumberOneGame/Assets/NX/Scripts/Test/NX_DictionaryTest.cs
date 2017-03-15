using UnityEngine;
using System.Collections;

public class NX_DictionaryTest : MonoBehaviour {

	#region === 字段 === 

    private NX_Dictionary<int, string> m_dict;
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
        if (GUILayout.Button("创建 dict"))
        {
            m_dict = new NX_Dictionary<int, string>("goodname");
        }
        if (m_dict != null)
        {
            GUILayout.Label("dict name:" + m_dict.Name);
            GUILayout.Label("dict count:" + m_dict.Count);
            if (GUILayout.Button("dict random add"))
            {
                m_dict.Add(Random.Range(0, 10), Random.Range(0, 10).ToString());
            }
            if (GUILayout.Button("dict random remove"))
            {
                m_dict.Remove(Random.Range(0, 10));
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
