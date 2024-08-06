local profiler = {}

local ProfilerManager = CS.zFramework.ProfilerManager

function profiler.BeginSample(name)
    ProfilerManager.BeginSample(name)
end

function profiler.EndSample()
    ProfilerManager.EndSample()
end

return profiler