using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UObject = UnityEngine.Object;
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

    public void LoadAsset(string assetName,Action<UObject>action)
    {
        StartCoroutine(LoadBundleAsync(assetName,action));
    }
    void Start()
    {
        ParseVersionFile();
        LoadAsset("Assets/BuildResources/Cube.prefab",OnComplete);
    }

    private void OnComplete(UObject obj)
    {
        GameObject go = Instantiate(obj) as GameObject;
        go.transform.SetParent(transform);
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
    }
}
