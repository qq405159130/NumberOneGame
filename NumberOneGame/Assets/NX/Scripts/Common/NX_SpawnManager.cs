using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NX_SpawnManager : MonoBehaviour {


    //生成 管理 Prefab
    public GameObject[] m_SOPrefabs;
    public GameObject[] m_SPTypePrefabs;
    public Transform m_SPParent;
    public List<Transform> m_freeSPList = new List<Transform>();

    public List<GameObject> m_curSOList = new List<GameObject>();

    public int m_level;

	void Start () 
	{
	
	}
	
	void Update ()
	{
	    
	}

    public void Spawn()
    {
        ClearCurrentWave();
        AddLevel();
        SpawnOneWave();
    }
    private void SpawnOneWave()
    {
        //生成放置点
        GameObject spawnPointType = Instantiate(m_SPTypePrefabs[m_level],m_SPParent.transform.position,Quaternion.identity) as GameObject;
        Transform[] spawnPointsArray = spawnPointType.transform.GetComponentsInChildren<Transform>();
        
        foreach (var sp in spawnPointsArray)
        {
            m_freeSPList.Add(sp);//添加列表
            //生成物体
            GameObject randomPrefab = m_SOPrefabs[Random.Range(0, m_SOPrefabs.Length)];
            GameObject so = Instantiate(randomPrefab, sp.transform.position, Quaternion.identity)as GameObject;

            m_curSOList.Add(so);//添加列表
        }
    }

    private void ClearCurrentWave()
    {
        //销毁物体
        Transform[] temp = m_SPParent.GetComponentsInChildren<Transform>();
        foreach (var sp in temp)
        {
            Destroy(sp.gameObject);
        }
        //清空列表
        m_curSOList.Clear();
        m_freeSPList.Clear();
    }

    private void AddLevel()
    {
        m_level ++;
        m_level %= m_SPTypePrefabs.Length;
    }
    public void AddCurSOList(GameObject spawnObject)
    {
        m_curSOList.Add(spawnObject);
    }

    public void RemoveCurSOList(GameObject spawnObject)
    {
        m_curSOList.Remove(spawnObject);
    }
}
