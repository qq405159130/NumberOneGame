using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class NX_IOUtility {

    public static void ScanAllTo(string srcPath,string tgtPath)
    {
        DirectoryInfo srcDiRoot = new DirectoryInfo(srcPath);
        FileInfo[] srcFis = srcDiRoot.GetFiles();
        DirectoryInfo[] srcDis = srcDiRoot.GetDirectories();
        Debug.Log(string.Format("当前源目录是：{0}，其中包含{1}个文件夹、{2}个文件", srcDiRoot.FullName,srcDis.Length, srcFis.Length));
        
        //处理当前根目录
        //string tgtDiRoot = srcDiRoot.FullName.Replace("Xml", "ABC");
        if (!Directory.Exists(tgtPath))
        {
            Debug.Log("目标目录不存在，创建：" + new DirectoryInfo(tgtPath).FullName);
            Directory.CreateDirectory(tgtPath);
        }
        else
        {
            //Debug.Log("目标目录已存在：" + tgtPath);
            Debug.Log("目标目录已存在：" + new DirectoryInfo(tgtPath).FullName);
        }

        //处理当前目录下的文件
        if (srcFis.Length != 0)
        {
            //DoSth
            for (int i = 0; i < srcFis.Length; i++)
            {
                string tgtFilePath = tgtPath + "//" + srcFis[i].Name;
                CopyToByStream(srcFis[i], tgtFilePath);
            }
        }
        //迭代到子目录
        if (srcDis.Length != 0)
        {
            for (int i = 0; i < srcDis.Length; i++)
            {
                string tgtFolderPath = tgtPath + "//" + srcDis[i].Name;
                ScanAllTo(srcDis[i].FullName, tgtFolderPath);
            }
        }
    }

    public static void CopyToByStream(FileInfo srcFi,string tgtPath, bool isOverride = true)
    {
        FileInfo tgtFi = new FileInfo(tgtPath);
        if (tgtFi.Exists)
        {
            if(srcFi.Length == tgtFi.Length && srcFi.LastWriteTime == tgtFi.LastWriteTime)
            {
                return;
            }
        }

        bool isTypeA = false;
        if (isTypeA)
        {
            //TypeA
            srcFi.CopyTo(tgtPath, true);
            return;
        }

        //TypeB
        //TODO 目标文件是否存在，目标文件路径是否存在
        FileStream fsRead = srcFi.OpenRead();
        FileStream fsWrite = new FileStream(tgtPath, FileMode.Create);//CreateNew : if exist,throw exception
        
        byte[] bytes = new byte[4096];
        while (true)
        {
            int length = fsRead.Read(bytes, 0, bytes.Length);
            //if(length != 1024)//Test
            //    Debug.Log("Test>>本次读取不满，长度为:" + length);
            if (length == 0)
            {
                break;
            }
            else
            {
                fsWrite.Write(bytes, 0, length);
            }
        }
        fsRead.Close();
        fsWrite.Close();
    }
}
