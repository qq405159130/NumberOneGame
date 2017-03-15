using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CharacterPropInfo
{
    enum InfoSequence
    {
        m_id = 0            ,
        m_name              ,
        m_des               ,
        m_modelId           ,
        m_headId            ,
        m_atkType           ,
        m_atkClass          ,
        m_hp                ,
        m_speed             ,
        m_atkSpeed          ,
        m_atkRange          ,
        m_phyAtk            ,
        m_magAtk            ,
        m_phyDef            ,
        m_magDef            ,
        m_hit               ,
        m_dodge             ,
        m_crit              ,
        m_antiCrit          ,
        m_Skill1Id	        ,
        m_Skill2Id	        ,
        m_Skill3Id	        ,
        m_Skill4Id	        ,
        m_Skill5Id	        ,
        m_IsBoss	        ,
        m_ArmyId	        ,
        m_ArmyCount         ,
    }

    static NX_Dictionary<int, Character.Prop> _dict;
    static string m_infoPath = Application.dataPath + "//..//IO//CSV\\" + "Monster.csv";
    public static TextAsset m_textAsset;

    public static NX_Dictionary<int, Character.Prop> Dict
    {
        get
        {
            if (_dict == null)
                _dict = new NX_Dictionary<int, Character.Prop>();
            return _dict;
        }
    }

    public static void ReadInfo()
    {
        //string str = File.ReadAllText(m_infoPath);

        //string str = File.OpenText(m_infoPath).ReadToEnd();

        //FileStream fs = File.Open(m_infoPath, FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
        //StreamReader sr = new StreamReader(fs);
        //string str = sr.ReadToEnd();

        //FileInfo fi = new FileInfo(m_infoPath);
        ////fi.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        //string str = fi.OpenText().ReadToEnd();

        FileStream fs = new FileStream(m_infoPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
        string str = sr.ReadToEnd();

        ReadInfo(str);
    }

    static void ReadInfo(string str)
    {
        int startRowIndex = 4;
        if (str == null)
            Debug.LogError("error path");
        string[] strs = str.Split('\n');

        for (int i = startRowIndex; i < strs.Length; i++)
        {
            string[] propStrs = strs[i].Split(',');
            if (strs[i].Length == 0 || propStrs.Length == 0)
                continue;

            Character.Prop prop = new Character.Prop();

            prop.m_id = GetPropInt(propStrs, (int)InfoSequence.m_id);
            prop.m_name = GetPropStr(propStrs, (int)InfoSequence.m_name);
            prop.m_des = GetPropStr(propStrs, (int)InfoSequence.m_des);
            prop.m_modelId = GetPropInt(propStrs, (int)InfoSequence.m_modelId);
            prop.m_headId = GetPropInt(propStrs, (int)InfoSequence.m_headId);
            prop.m_atkType = GetPropInt(propStrs, (int)InfoSequence.m_atkType);
            prop.m_atkClass = GetPropInt(propStrs, (int)InfoSequence.m_atkClass);
            prop.m_hp = GetPropInt(propStrs, (int)InfoSequence.m_hp);
            prop.m_speed = GetPropInt(propStrs, (int)InfoSequence.m_speed);
            prop.m_atkSpeed = GetPropInt(propStrs, (int)InfoSequence.m_atkSpeed);
            prop.m_atkRange = GetPropInt(propStrs, (int)InfoSequence.m_atkRange);
            prop.m_phyAtk = GetPropInt(propStrs, (int)InfoSequence.m_phyAtk);
            prop.m_magAtk = GetPropInt(propStrs, (int)InfoSequence.m_magAtk);
            prop.m_phyDef = GetPropInt(propStrs, (int)InfoSequence.m_phyDef);
            prop.m_magDef = GetPropInt(propStrs, (int)InfoSequence.m_magDef);
            prop.m_hit = GetPropInt(propStrs, (int)InfoSequence.m_hit);
            prop.m_dodge = GetPropInt(propStrs, (int)InfoSequence.m_dodge);
            prop.m_crit = GetPropInt(propStrs, (int)InfoSequence.m_crit);
            prop.m_antiCrit = GetPropInt(propStrs, (int)InfoSequence.m_antiCrit);
            prop.m_Skill1Id = GetPropInt(propStrs, (int)InfoSequence.m_Skill1Id);
            prop.m_Skill2Id = GetPropInt(propStrs, (int)InfoSequence.m_Skill2Id);
            prop.m_Skill3Id = GetPropInt(propStrs, (int)InfoSequence.m_Skill3Id);
            prop.m_Skill4Id = GetPropInt(propStrs, (int)InfoSequence.m_Skill4Id);
            prop.m_Skill5Id = GetPropInt(propStrs, (int)InfoSequence.m_Skill5Id);
            prop.m_IsBoss = GetPropInt(propStrs, (int)InfoSequence.m_IsBoss);
            prop.m_ArmyId = GetPropInt(propStrs, (int)InfoSequence.m_ArmyId);
            prop.m_ArmyCount = GetPropInt(propStrs, (int)InfoSequence.m_ArmyCount);

            prop.m_hpMax = prop.m_hp;

            Dict.Add(prop.m_id, prop);
            Debug.Log("New Info id:" + prop.m_id);
        }
        Debug.Log("Load CharacterInfo done, Dict.Count:" + Dict.Count);
    }
    private static int GetPropInt(string[] strs,int index)
    {
        if (string.IsNullOrEmpty(strs[index]))
            return 0;
        return int.Parse(strs[index]);
    }
    private static string GetPropStr(string[] strs, int index)
    {
        return strs[index];
    }
    //public static bool TryGetInfo(int id,out Character.Prop prop)
    //{
    //    if (_dict.Dict.TryGetValue(id, out prop))
    //    {
    //        Debug.Log("TryGetInfo id:" + id + "success" + ", out prop id : " + prop.m_id);
    //        return true;
    //    }
    //    Debug.Log("TryGetInfo id:" + id + "failure" + ", out prop id : " + prop.m_id);
    //    return false;
    //}

    //public static bool TryGetInfo(int id, out Character.Prop prop)
    //{
    //    Character.Prop newProp;
    //    if (_dict.Dict.TryGetValue(id, out newProp))
    //    {
    //        prop = newProp;
    //        Debug.Log("TryGetInfo id:" + id + "success" + ", out prop id : " + prop.m_id);
    //        return true;
    //    }
    //    prop = newProp;
    //    Debug.Log("TryGetInfo id:" + id + "failure" + ", out prop id : " + prop.m_id);
    //    return false;
    //}

    public static bool TryGetInfo(int id, out Character.Prop prop)
    {
        Character.Prop newProp;
        if (_dict.Dict.TryGetValue(id, out newProp))
        {
            prop = new Character.Prop();
            Character.Prop.CopyFrom(prop, newProp);
            Debug.Log("TryGetInfo id:" + id + "success" + ", out prop id : " + prop.m_id);
            return true;
        }
        else
        {
            prop = new Character.Prop();
            Character.Prop.CopyFrom(prop, newProp);
            Debug.Log("TryGetInfo id:" + id + "failure" + ", out prop id : " + prop.m_id);
            return false;
        }
    }

}
