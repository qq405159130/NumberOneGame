using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NX_Dictionary<T1, T2>
{
    private Dictionary<T1, T2> _dict;
    private string _name;
    private static int num  = 0;//已创建 副本数量

    public NX_Dictionary(string name = "nx_dict")
    {
        _dict = new Dictionary<T1, T2>();
        if (name == "nx_dict")
            _name = string.Format(name + "({0})", num);
        else
            _name = name;
        num++;
        
    }

    public bool Remove(T1 key)
    {
        if (_dict.ContainsKey(key))
        {
            _dict.Remove(key);
            return true;
        }
        else
        {
            Debug.LogWarning(string.Format("Dict移除失败:{0}不存在{1}",_name,key.ToString()));
        }
        return false;
    }

    public void Add(T1 key, T2 value)
    {
        if (!_dict.ContainsKey(key))
        {
            _dict.Add(key, value);
        }
        else
        {
            Debug.LogWarning(string.Format("Dict添加失败:{0}已存在{1}", _name, key.ToString()));
        }
    }

    public int Count
    {
        get
        {
            return _dict.Count;
        }
    }

    public Dictionary<T1, T2> Dict
    {
        get { return _dict; }
    }

    public string Name
    {
        get { return _name; }
    }

    public void Clear()
    {
        _dict.Clear();
    }
}
