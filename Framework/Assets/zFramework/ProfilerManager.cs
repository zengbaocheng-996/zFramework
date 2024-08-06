using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zFramework
{
    public static class ProfilerManager
    {
        public static void BeginSample(string name)
        {
            UnityEngine.Profiling.Profiler.BeginSample(name);
        }

        public static void EndSample()
        {
            UnityEngine.Profiling.Profiler.EndSample();
        }
    } 
    
}

