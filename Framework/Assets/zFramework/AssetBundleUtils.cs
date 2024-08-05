using System.IO;
using UnityEngine;

namespace zFramework
{
    public static class AssetBundleUtils
    {
        public static AssetBundle LoadFromFile()
        {
            string assetBundleDirectory = "Assets/AssetBundles";
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(assetBundleDirectory, "AssetBundles"));
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return null;
            }
            return myLoadedAssetBundle;
        }
    }  
}

