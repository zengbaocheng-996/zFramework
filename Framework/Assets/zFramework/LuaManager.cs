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
        private readonly string _mainFileName = "main";

        private static LuaManager _luaManager;

        private readonly LuaEnv _luaenv;
        private LuaTable _mainEnv;

        private Action _mainAwake;
        private Action _mainStart;
        private Action _mainUpdate;
        private Action _mainOnDestroy;

        private void BindEventFunctions()
        {
            _mainEnv.Get("Awake", out _mainAwake);
            _mainEnv.Get("Start", out _mainStart);
            _mainEnv.Get("Update", out _mainUpdate);
            _mainEnv.Get("OnDestroy", out _mainOnDestroy);
        }

        private void SetMainEnv()
        {
            _mainEnv = _luaenv.NewTable();
            LuaTable meta = _luaenv.NewTable();
            meta.Set("__index", _luaenv.Global);
            _mainEnv.SetMetaTable(meta);
            meta.Dispose();
            _mainEnv.Set("self", this);
            _luaenv.DoString("require '" + _mainFileName + "'", _mainFileName, _mainEnv);
        }

        public void Awake()
        {
            SetMainEnv();
            BindEventFunctions();
            _mainAwake();
        }

        public void Start()
        {
            _mainStart();
        }

        public void Update()
        {
            _mainUpdate();
            _luaenv.Tick();
        }

        public void OnDestroy()
        {
            _mainOnDestroy();
            _mainAwake = null;
            _mainStart = null;
            _mainUpdate = null;
            _mainOnDestroy = null;
            _mainEnv.Dispose();
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