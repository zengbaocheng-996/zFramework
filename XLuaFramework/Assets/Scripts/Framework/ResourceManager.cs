using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UObject = UnityEngine.Object;
using UnityEditor;
public class ResourceManager : MonoBehaviour
{
    internal class BundleInfo
    {
        public string AssetsName;
        public string BundleName;
        public List<string> Dependences;
    }
    //���Bundle��Ϣ�ļ���
    private Dictionary<string, BundleInfo> m_BundleInfos = new Dictionary<string, BundleInfo>();
    /// <summary>
    /// �����汾�ļ�
    /// </summary>
    private void ParseVersionFile()
    {
        //�汾�ļ���·��
        string url = Path.Combine(PathUtil.BundleResourcePath, AppConst.FileListName);
        string[] data= File.ReadAllLines(url);
        //�����ļ���Ϣ
        for(int i = 0;i<data.Length; i++)
        {
            BundleInfo bundleInfo = new BundleInfo();
            string[] info = data[i].Split('|');
            bundleInfo.AssetsName = info[0];
            bundleInfo.BundleName = info[1];
            //list����:���������֣����ɶ�̬����
            bundleInfo.Dependences = new List<string>(info.Length - 2);
            for (int j = 2; j < info.Length; j++)
            {
                bundleInfo.Dependences.Add(info[j]);
            }
            m_BundleInfos.Add(bundleInfo.AssetsName,bundleInfo);
        }
    }
    /// <summary>
    /// �첽������Դ
    /// </summary>
    /// <param name="assetName">��Դ��</param>
    /// <param name="action">��ɻص�</param>
    /// <returns></returns>
    IEnumerator LoadBundleAsync(string assetName, Action<UObject> action = null)
    {
        Debug.Log("LoadBundleAsync");
        string bundleName = m_BundleInfos[assetName].BundleName;
        string bundlePath = Path.Combine(PathUtil.BundleResourcePath, bundleName);
        List<string> dependences = m_BundleInfos[assetName].Dependences;
        if (dependences != null && dependences.Count > 0)
        {
            for (int i = 0; i < dependences.Count; i++) 
            {
                yield return LoadBundleAsync(dependences[i]);
            }
        }

        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return request;
        AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync(assetName);
        yield return bundleRequest;
        //if(action!=null&&bundleRequest!=null)
        //{
        //    action.Invoke(bundleRequest.asset);
        //}
        action?.Invoke(bundleRequest?.asset);
    }
    /// <summary>
    /// �༭������������Դ
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="action"></param>
    void EditorLoadAsset(string assetName, Action<UObject> action = null)
    {
        Debug.Log("EditorLoadAsset");
        UObject obj = AssetDatabase.LoadAssetAtPath(assetName, typeof(UObject));
        if (obj == null)
        {
            Debug.LogError("assets name is not exist: " + assetName);
        }
        action?.Invoke(obj);
    }

    private void LoadAsset(string assetName,Action<UObject>action)
    {
        if(AppConst.GameMode == GameMode.EditorMode)
        {
            EditorLoadAsset(assetName, action);
        }
        else
        {
            StartCoroutine(LoadBundleAsync(assetName, action));         
        }
    }
    //Tag:ж������ʱ����
    void Start()
    {
        ParseVersionFile();
        LoadTest("Cube", OnComplete);
    }
    private void OnComplete(UObject obj)
    {
        GameObject go = Instantiate(obj) as GameObject;
        go.transform.SetParent(transform);
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
    }
    public void LoadUI(string assetName,Action<UObject>action = null)
    {
        LoadAsset(PathUtil.GetUIPath(assetName), action);
    }    
    public void LoadTest(string assetName, Action<UObject>action = null)
    {
        LoadAsset(PathUtil.GetTestPath(assetName), action);
    }
}
