using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NX_ImageProcessorTest : MonoBehaviour {

	#region === 字段 === 

    private string m_folder = "//..//IO//ImageProcessor//";
    private string m_fileName = "test_";
    private string m_extension = ".png";
    private Texture2D m_imageA;
    private Texture2D m_imageB;
    private NX_IODownloader m_IODownloader;
    private Queue<Texture2D> m_imageQueue = new Queue<Texture2D>();

    private bool m_isDownloadDone;

    private Transform m_imageGoA;
    private Transform m_imageGoB;
    private Transform m_imageGoC;

    public NX_ImageProcessor.ImageMixType m_mixType = NX_ImageProcessor.ImageMixType.Normal;
    private NX_ImageProcessor.ImageMixType m_mixTypeStore = NX_ImageProcessor.ImageMixType.Normal;

	#endregion
	
	#region === 属性 === 
	
	#endregion
	
	#region === 方法 === 
	
	#region === Unity默认函数 === 
	void Awake ()
	{
	    m_IODownloader = this.gameObject.AddComponent<NX_IODownloader>();
	}
	
	void Start ()
	{

	}
	
	void Update () 
	{
	    if (!m_isDownloadDone)
	    {
	        if (m_imageQueue.Count < 2)
	        {
	            if (!m_IODownloader.IsLoading)
	            {
	                Debug.Log("开始下载");
	                string filePath = "file:///" + Application.dataPath + m_folder + m_fileName + Random.Range(0, 10) +
	                                  m_extension;
	                m_IODownloader.Doadload(filePath, NX_IODownloader.ResourceType.Texture);
	            }
	            else
	            {
	                Debug.Log("下载中...");
	                if (m_IODownloader.IsDone)
	                {
	                    Debug.Log("下载成功");
	                    Texture2D temp = m_IODownloader.GetResult() as Texture2D;
	                    m_imageQueue.Enqueue(temp);
	                }
	            }
	        }
	        else
	        {
	            m_isDownloadDone = true;
	            m_imageA = m_imageQueue.Dequeue();
	            m_imageB = m_imageQueue.Dequeue();
	        }
	    }
	    else
	    {
	        if (m_imageGoA == null)
	        {
	            m_imageGoA = this.transform.Find("m_imageGoA");
	            if (m_imageGoA == null)
	            {
	                m_imageGoA = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
	                m_imageGoA.name = "m_imageGoA";
                    m_imageGoA.SetParent(this.transform);
	            }
	        }
            if (m_imageGoB == null)
            {
                m_imageGoB = this.transform.Find("m_imageGoB");
                if (m_imageGoB == null)
                {
                    m_imageGoB = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
                    m_imageGoB.name = "m_imageGoB";
                    m_imageGoB.SetParent(this.transform);
                    m_imageGoB.transform.position = Vector3.right*2f;
                }
            }
            if (m_imageGoC == null)
            {
                m_imageGoC = this.transform.Find("m_imageGoC");
                if (m_imageGoC == null)
                {
                    m_imageGoC = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
                    m_imageGoC.name = "m_imageGoC";
                    m_imageGoC.SetParent(this.transform);
                    m_imageGoC.transform.position = Vector3.right * 4f;
                }
            }

	        if (m_mixTypeStore != m_mixType)
	        {
	            m_mixTypeStore = m_mixType;
	            Refresh();
	        }
	    }
	}

	#endregion
	
	#region === 公有函数 === 

    public void Refresh()
    {
        m_imageGoA.GetComponent<Renderer>().material.mainTexture = m_imageA;
        m_imageGoB.GetComponent<Renderer>().material.mainTexture = m_imageB;
        Texture2D result = NX_ImageProcessor.ImageMix(m_imageA, m_imageB, m_mixType);
        m_imageGoC.GetComponent<Renderer>().material.mainTexture = result;

    }
	#endregion
	
	#region === 私有函数 === 
	
	#endregion
	
	#endregion
}
