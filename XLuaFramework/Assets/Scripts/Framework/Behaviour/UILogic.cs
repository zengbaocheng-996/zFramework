using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogic : LuaBehaviour
{
    Action m_LuaOnOpen;
    Action m_LuaOnClose;
    public override void Init(string luaName)
    {
        base.Init(luaName);
        m_ScriptEnv.Get("OnOpen", out m_LuaOnOpen);
        m_ScriptEnv.Get("OnClose", out m_LuaOnClose);
    }

    public void OnOpen()
    {
        m_LuaOnOpen?.Invoke();        
    }

    public void Close()
    {
        m_LuaOnClose?.Invoke();
    }

    protected override void Clear()
    {
        base.Clear();
        m_LuaOnOpen = null;
        m_LuaOnClose = null;
    }
}
