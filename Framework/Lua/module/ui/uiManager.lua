local mananger = require("module/manager")
local uiManager = mananger:New()

local GameObject = CS.UnityEngine.GameObject
local TransformManager = CS.zFramework.TransformManager
local FindButton = TransformManager.FindButton
local system_enum = require("system/system_enum")
local sceneManager = require(system_enum.manager.sceneManager)
function uiManager.Awake()
    local Button = FindButton("Button")
    Button.onClick:AddListener(uiManager.ButtonClick)
end

function uiManager.ButtonClick()
    sceneManager.LoadScene("Test")
    print("click!!!")
end

return uiManager