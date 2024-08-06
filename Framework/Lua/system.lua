local system = {}

system.managers = {
    uiManager          = require("module/ui/uiManager"),
    sceneManager       = require("module/scene/sceneManager"),
    assetBundleManager = require("module/assetBundle/assetBundleManager"),
}

function system.Awake()
    for k, v in pairs(system.managers) do
        print(k.." Awake!")
        v.Awake()
    end
end

function system.GetManager(name)
    local manager = system.managers[name]
    if manager then
        return manager
    else
        print(name.." doesn't exist!")
    end
end

return system