using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using Directory = System.IO.Directory;

public class LuaManager : MonoBehaviour
{
    //所有的lua文件名
    public List<string> LuaNames = new List<string>();
    //缓存lua脚本内容
    private Dictionary<string, byte[]> m_LuaScripts;

    public LuaEnv LuaEnv;

    Action InitOK;

    public void Init(Action init)
    {
        InitOK += init;
        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(Loader);
        m_LuaScripts = new Dictionary<string, byte[]>();
#if UNITY_EDITOR
        if(AppConst.GameMode == GameMode.EditorMode)
            EditorLoadLuaScript();
        else
#endif
            LoadLuaScript();
    }
    public void StartLua(string name)
    {
        LuaEnv.DoString(string.Format("require '{0}'", name));
    }
    
    byte[] Loader(ref string name)
    {
        return GetLuaScript(name);
    }

    public byte[] GetLuaScript(string name)
    {
        //require ui.login
        name = name.Replace(".", "/");
        string fileName = PathUtil.GetLuaPath(name);
        byte[] luaScript = null;
        if(!m_LuaScripts.TryGetValue(fileName, out luaScript))
            Debug.LogError("lua script is not exist: "+fileName);
        return luaScript;
    }

    void LoadLuaScript()
    {
        foreach (string name in LuaNames)
        {
            Manager.Resource.LoadLua(name, (UnityEngine.Object obj)=>
            {
                AddLuaScript(name, (obj as TextAsset).bytes);
                if (m_LuaScripts.Count >= LuaNames.Count)
                {
                    //所有lua加载完成的时候
                    InitOK?.Invoke();
                    LuaNames.Clear();
                    LuaNames = null;
                }
            });
        }
    }
    
    public void AddLuaScript(string assetsName, byte[] luaScript)
    {
        // m_LuaScripts.Add(assetsName, luaScript);
        m_LuaScripts[assetsName] = luaScript;
    }
#if UNITY_EDITOR
    void EditorLoadLuaScript()
    {
        string[] luaFiles = Directory.GetFiles(PathUtil.LuaPath, "*.bytes", SearchOption.AllDirectories);
        for (int i = 0; i < luaFiles.Length; i++)
        {
            string fileName = PathUtil.GetStandardPath(luaFiles[i]);
            byte[] file = File.ReadAllBytes(fileName);
            AddLuaScript(PathUtil.GetUnityPath(fileName), file);
        }
        InitOK?.Invoke();
    }
#endif
    private void Update()
    {
        if (LuaEnv != null)
        {
            LuaEnv.Tick();
        }
    }
}
