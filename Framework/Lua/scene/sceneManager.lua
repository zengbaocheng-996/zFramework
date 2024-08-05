local SceneManagement = CS.UnityEngine.SceneManagement
local SceneManager    = SceneManagement.SceneManager

local sceneManager = {}

function sceneManager.Awake()
    print("sceneManager Awake")
end

function sceneManager.LoadScene(nextSceneName)
    SceneManager.LoadScene(nextSceneName)
end

return sceneManager