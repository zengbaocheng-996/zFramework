local mananger = require("module/manager")
local sceneManager = mananger:New()

local SceneManagement = CS.UnityEngine.SceneManagement
local SceneManager    = SceneManagement.SceneManager

function sceneManager.Awake()
end

function sceneManager.LoadScene(nextSceneName)
    -- // var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("MyObject");
    -- // Instantiate(prefab);
    -- assetManger.loadedAssetBundle.LoadAsset(nextSceneName,function()SceneManager.LoadScene(nextSceneName)end);
    SceneManager.LoadScene(nextSceneName)
end

return sceneManager