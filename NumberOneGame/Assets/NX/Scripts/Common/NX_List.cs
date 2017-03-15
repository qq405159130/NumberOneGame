using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class NX_List <T>{
    
    private List<T> _list;
    private string _name;
    private static int num  = 0;//已创建 副本数量

    public NX_List(string name = "nx_list")
    {
        _list = new List<T>();
        if (name == "nx_list")
            _name = string.Format(name + "({0})", num);
        else
            _name = name;
        num++;
    }

    public bool Remove(T key)
    {
        if (_list.Contains(key))
        {
            _list.Remove(key);
            return true;
        }
        else
        {
            Debug.LogWarning(string.Format("List移除失败:{0}不存在{1}", _name, key.ToString()));
            //Debug.LogWarning(string.Format("List.Count:" + Count));
        }
        return false;
    }

    public void Add(T key)
    {
        if (!_list.Contains(key))
        {
            _list.Add(key);
        }
        else
        {
            Debug.LogWarning(string.Format("List添加失败:{0}已存在{1}", _name, key.ToString()));
        }
    }

    public int Count
    {
        get
        {
            return _list.Count;
        }
    }

    public List<T> List
    {
        get { return _list; }
    }

    public string Name
    {
        get { return _name; }
    }
    public void Clear()
    {
        _list.Clear();
    }
}
