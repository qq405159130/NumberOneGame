using System;
using UnityEngine;
using System.Collections;
using System.Reflection;

public class ReflectClassTest : MonoBehaviour
{

	#region === 字段 === 
	
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
	#endregion

	#region === 公有函数 === 
    [ContextMenu("Func")]
	public void Func()
    {
        ReflectClass nc = new ReflectClass();
        Type t = nc.GetType();
        object obj = Activator.CreateInstance(t);
        //取得ID字段 
        FieldInfo fi = t.GetField("ID");
        //给ID字段赋值 
        fi.SetValue(obj, 001);
        //取得MyName属性 
        PropertyInfo pi1 = t.GetProperty("Name");
        //给MyName属性赋值 
        pi1.SetValue(obj, "grayworm", null);
        PropertyInfo pi2 = t.GetProperty("Info");
        pi2.SetValue(obj, "hi.baidu.com/grayworm", null);
        //取得show方法 
        MethodInfo mi = t.GetMethod("Show");
        //调用show方法 
        mi.Invoke(obj, null);
        Debug.Log("被构造实例的值 1：" + fi.GetValue(obj));
        Debug.Log("被构造实例的值 2：" + pi1.GetValue(obj, null));
        Debug.Log("被构造实例的值 3：" + pi2.GetValue(obj, null));

        PropertyInfo[] pis = t.GetProperties();
        foreach (var pi in pis)
        {
            Debug.Log(pi.Name);
        }


    }
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion

  
}
