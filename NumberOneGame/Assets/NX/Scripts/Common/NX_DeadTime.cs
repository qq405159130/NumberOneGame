using UnityEngine;
using System.Collections;

public class NX_DeadTime : MonoBehaviour {

	#region === 字段 === 

    public int m_deadTimer;
	#endregion
	
	#region === 属性 === 
	
	#endregion
	
	#region === 方法 === 
	
	#region === Unity默认函数 === 
	void Awake () 
	{
        Destroy(this.gameObject,m_deadTimer);
	}
	
	void Start () 
	{
		
	}
	
	void Update () 
	{
	
	}
	#endregion
	
	#region === 公有函数 === 
	
	#endregion
	
	#region === 私有函数 === 

	#endregion
	
	#endregion
}
