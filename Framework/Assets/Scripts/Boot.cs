using UnityEngine;
using zFramework;

public class Boot : MonoBehaviour
{
    private readonly LuaManager _luaManager = LuaManager.GetInstance();

    void Awake()
    {
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