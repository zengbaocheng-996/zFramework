local GameObject = CS.UnityEngine.GameObject
local TransformUtils = CS.zFramework.TransformUtils
local FindButton = TransformUtils.FindButton
local uiManager = {}
local sceneManger = require("scene/sceneManager")

function uiManager.Awake()
    print("uiManager Awake")
    local Button = FindButton("Button")
    print(Button.name)
    Button.onClick:AddListener(uiManager.ButtonClick)
end

function uiManager.ButtonClick()
    sceneManger.LoadScene("Test")
    print("click!!!")
end

return uiManager