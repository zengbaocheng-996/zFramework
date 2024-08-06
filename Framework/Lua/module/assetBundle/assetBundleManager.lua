local mananger = require("module/manager")
local assetBundleManager = mananger:New()

local AssetBundleManager = CS.zFramework.AssetBundleManager

function assetBundleManager.Awake()
    assetBundleManager.loadedAssetBundle = AssetBundleManager.LoadFromFile()
end
-- // var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("MyObject");
-- // Instantiate(prefab);
return assetBundleManager