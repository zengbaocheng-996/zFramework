local manager = {}

function manager:New()
    local instance = {}
    setmetatable(instance, self)
    self.__index = self
    return instance
end

function manager:Awake()
end

function manager:Update()
end

function manager:OnDestroy()
end

return manager