using UnityEngine;
using System.Collections;
using System.IO;

public class NX_IODownloaderTest : MonoBehaviour {

	#region === 字段 === 

    public string filePath = "//..//IO//Store" + "\\" + "test.png";
    private NX_IODownloader m_IODownloader;
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
	    m_IODownloader = this.gameObject.AddComponent<NX_IODownloader>();
	}
	
	void Update () 
	{
        //模板
        //if (!m_isDownloadDone)
        //{
        //    if (m_imageQueue.Count < 2)
        //    {
        //        if (!m_IOUtility.IsLoading)
        //        {
        //            Debug.Log("开始下载");
        //            string filePath = "file:///" + Application.dataPath + m_folder + m_fileName + Random.Range(0, 10) +
        //                              m_extension;
        //            m_IOUtility.Doadload(filePath, NX_IOUtility.ResourceType.Texture);
        //        }
        //        else
        //        {
        //            Debug.Log("下载中...");
        //            if (m_IOUtility.IsDone)
        //            {
        //                Debug.Log("下载成功");
        //                Texture2D temp = m_IOUtility.GetResult() as Texture2D;
        //                m_imageQueue.Enqueue(temp);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        m_isDownloadDone = true;
        //        m_imageA = m_imageQueue.Dequeue();
        //        m_imageB = m_imageQueue.Dequeue();
        //    }
        //}
	}

    void OnGUI()
    {
        if (GUILayout.Button("下载资源"))
        {
            m_IODownloader.Doadload("file:///" + Application.dataPath + filePath, NX_IODownloader.ResourceType.Texture);
        }

        if (m_IODownloader.IsDone)
        {
            Texture texture = m_IODownloader.GetResult() as Texture;
            GameObject.CreatePrimitive(PrimitiveType.Plane).GetComponent<Renderer>().material.mainTexture = texture;

            Debug.Log("IsDone .texture name is:" + texture);
        }
    }

    #endregion
	
	#region === 公有函数 === 
	
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion
}
