local AssetBundleUtils = CS.zFramework.AssetBundleUtils

local assetManager = {}

function assetManager.Awake()
    assetManager.loadedAssetBundle = AssetBundleUtils.LoadFromFile()
end
-- // var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("MyObject");
-- // Instantiate(prefab);
return assetManager