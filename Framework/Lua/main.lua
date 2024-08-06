local system = require("system/system")
local profiler = require("profiler")

function Awake()
	system.Awake()
end

function Start()
	-- print("lua start...")
end

local function profilerTest()
    local sum = 0
    for i = 1, 100000 do
        sum = sum + i
    end
    while sum > 1 do
        sum = sum / 2
    end
end

function Update()
    profiler.BeginSample("LuaUpdate")
    profilerTest()
    profiler.EndSample()
end

function OnDestroy()
    -- print("lua destroy...")
end