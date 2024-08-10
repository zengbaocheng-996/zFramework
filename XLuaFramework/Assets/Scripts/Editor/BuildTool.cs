using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
public class BuildTool : Editor
{
    [MenuItem("Tools/Build Windows Bundle")]
    static void BundleWindowsBuild()
    {
        Build(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/Build Android Bundle")]
    static void BundleAndroidBuild()
    {
        Build(BuildTarget.Android);
    }
    [MenuItem("Tools/Build iPhone Bundle")]
    static void BundleiPhoneBuild()
    {
        Build(BuildTarget.iOS);
    }
    static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
        // �ļ���Ϣ�б�
        List<string> bundleInfos = new List<string>();
        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath, "*", SearchOption.AllDirectories);
        for(int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
                continue;
            AssetBundleBuild assetBundle = new AssetBundleBuild();
            string fileName = PathUtil.GetStandardPath(files[i]);
            Debug.Log("file:" + fileName);

            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };
            //Debug.Log(fileName);
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath, "").ToLower();
            
            assetBundle.assetBundleName = bundleName + ".ab";
            //Debug.Log(assetBundle.assetBundleName);
            assetBundleBuilds.Add(assetBundle);
            //����ļ���������Ϣ
            List<string> dependenceInfo = GetDependence(assetName);
            string bundleInfo = assetName + "|" + bundleName;
            if(dependenceInfo.Count > 0)
            {
                bundleInfo = bundleInfo + "|" + string.Join("|", dependenceInfo);
            }
            bundleInfos.Add(bundleInfo);
        }
        if (Directory.Exists(PathUtil.BundleOutPath))
            Directory.Delete(PathUtil.BundleOutPath, true);
        Directory.CreateDirectory(PathUtil.BundleOutPath);
        //Debug.Log(PathUtil.BundleOutPath);
        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);
        File.WriteAllLines(PathUtil.BundleOutPath + "/" + AppConst.FileListName, bundleInfos);
        AssetDatabase.Refresh();

    }
    /// <summary>
    /// ��ȡ�����ļ��б�
    /// </summary>
    /// <param name="curFile"></param>
    /// <returns></returns>
    static List<string> GetDependence(string curFile)
    {
        List<string> dependence = new List<string>();
        string[] files = AssetDatabase.GetDependencies(curFile);
        dependence = files.Where(file => !file.EndsWith(".cs") && !file.Equals(curFile)).ToList();
        return dependence;
    }
}
