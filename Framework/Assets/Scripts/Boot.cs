using UnityEngine;
using zFramework;

public class Boot : MonoBehaviour
{
    private LuaManager _luaManager;

    void Awake()
    {
        _luaManager = LuaManager.GetInstance();
        _luaManager.Bind();

        _luaManager.Awake();
    }

    void Start()
    {
        _luaManager.Start();
    }

    void Update()
    {
        _luaManager.Update();
    }

    private void OnDestroy()
    {
        _luaManager.OnDestroy();
    }
}