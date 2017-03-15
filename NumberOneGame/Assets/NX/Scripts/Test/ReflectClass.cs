using UnityEngine;
using System.Collections;

public class ReflectClass
{

    private int _id;
    private string _name;
    private string _info;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Info
    {
        get { return _info; }
        set { _info = value; }
    }

    public ReflectClass()
    {
        Name = "缺省名字";
        Id = 100;
        Debug.Log("ReflectClass 的构造函数");
    }

    public void Func1()
    {
        Debug.Log("方法一");
        Id += 1;
        Name += "(1)";
    }

    public void Func2(int i = 10)
    {
        Id -= i;
    }
}
