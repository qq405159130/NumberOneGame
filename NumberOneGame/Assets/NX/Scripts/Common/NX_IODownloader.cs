using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NX_IODownloader : MonoBehaviour
{
    public enum ResourceType
    {
        AssetBundle,
        Texture,
        AudioClip,
        Movie,
        Text,
        Bytes
    }
	#region === 字段 === 

    private bool _isDone;
    private bool _isLoading;
    private object _result;

  
    #endregion
	
	#region === 属性 === 

   
    /// <summary>
    /// 运行状态量，开始下载为真，IsDone复位后跟着复位
    /// </summary>
    public bool IsLoading
    {
        get { return _isLoading; }
    }
    /// <summary>
    /// 结束标志位，在下载完毕后为真，读取后自动复位
    /// </summary>
    public bool IsDone
    {
        get
        {
            if (_isDone)
            {
                _isDone = false;
                _isLoading = false;
                return true;
            }
            else
                return false;
        }
    }
    public object GetResult()
    {
        if (_isLoading)
            return default(object);
        return _result;
    }

    #endregion
	
	#region === 方法 === 
	
	#region === Unity默认函数 === 

	#endregion
	
	#region === 公有函数 === 

    public void Doadload(string url,ResourceType type)
    {
        StartCoroutine(DoadloadByWWW(url,type));
    }
   
	#endregion
	
	#region === 私有函数 === 
    private IEnumerator DoadloadByWWW(string url,ResourceType type)
    {
        _isDone = false;
        _isLoading = true;

        WWW www = new WWW(url);
        yield return new WaitForSeconds(0.5f);
        while (!www.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (www.error != null)
        {
            Debug.Log("www error :" + www.error);
        }

        switch (type)
        {
            case ResourceType.AssetBundle:
                _result = www.assetBundle;
                break;
            case ResourceType.Texture:
                _result = www.texture;
                break;
            case ResourceType.AudioClip:
                _result = www.audioClip;
                break;
            case ResourceType.Movie:
                _result = www.movie;
                break;
            case ResourceType.Text:
                _result = www.text;
                break;
            case ResourceType.Bytes:
                _result = www.bytes;
                break;
        }

        //DoSth();

        _isDone = true;

        www.Dispose();
    }

    private IEnumerator DoadloadByStream(string url)
    {
        yield return 0;
    }

    protected void DoSth()
    {
        
    }

    private Texture CutOutImage(Texture texture,Rect rect)
    {

        return new Texture();
    }
    #endregion

    #endregion
}
