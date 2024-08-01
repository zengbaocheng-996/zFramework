using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.Services.Core.Environments;
using XLua;

namespace zFramework
{
    public class LuaManager
    {
        private string _mainFileName = "main";

        private static LuaManager _luaManager;

        private readonly LuaEnv _luaenv;

        private Action _luaAwake;
        private Action _luaStart;
        private Action _luaUpdate;
        private Action _luaOnDestroy;

        public void RequireMain()
        {
            _luaenv.DoString("require '" + _mainFileName + "'");
        }

        public void BindEventFunctions()
        {
            _luaenv.Global.Get("Awake",     out _luaAwake);
            _luaenv.Global.Get("Start",     out _luaStart);
            _luaenv.Global.Get("Update",    out _luaUpdate);
            _luaenv.Global.Get("OnDestroy", out _luaOnDestroy);
        }

        public void Bind()
        {
            RequireMain();
            BindEventFunctions();
        }

        public void Awake()
        {
            _luaAwake();
        }

        public void Start()
        {
            _luaStart();
        }

        public void Update()
        {
            _luaUpdate();
            _luaenv.Tick();
        }

        public void OnDestroy()
        {
            _luaOnDestroy();
            _luaAwake     = null;
            _luaStart     = null;
            _luaUpdate    = null;
            _luaOnDestroy = null;
            // _luaenv = null;
            // _luaenv.Dispose();
        }

        private LuaManager()
        {
            _luaenv = new LuaEnv();
            _luaenv.AddLoader(LuaFileLoader);
        }

        public static LuaManager GetInstance()
        {
            if (_luaManager == null)
            {
                _luaManager = new LuaManager();
            }

            return _luaManager;
        }

        private byte[] LuaFileLoader(ref string filePath)
        {
            string path = Environment.CurrentDirectory + "/Lua/" + filePath + ".lua";
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                Debug.LogError(path + "doesn't exist!");
            }
            return null;
        }
    }
}