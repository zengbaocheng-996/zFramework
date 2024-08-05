local uiManger    = require("ui/uiManager")
local sceneManger = require("scene/sceneManager")
local assetManger = require("asset/assetManager")

function Awake()
	uiManger.Awake()
	sceneManger.Awake()
	assetManger.Awake()
end

function Start()
	-- print("lua start...")
end

function Update()
    -- print("lua update...")
end

function OnDestroy()
    -- print("lua destroy...")
end