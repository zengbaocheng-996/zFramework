using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    //��Ŀ¼
    public static readonly string AssetPath = Application.dataPath;

    //��Ҫ��bundle��Ŀ¼
    public static readonly string BuildResourcesPath = AssetPath + "/BuildResources/";
    
    //bundle���Ŀ¼
    public static readonly string BundleOutPath = Application.streamingAssetsPath;

    /// <summary>
    /// ��ȡUnity�����·��
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetUnityPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        return path.Substring(path.IndexOf("Assets"));
    }

    /// <summary>
    /// ��ȡ��׼·��
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetStandardPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        return path.Trim().Replace("\\", "/");
    }
}
