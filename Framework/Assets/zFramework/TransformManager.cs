using UnityEngine;
using UnityEngine.UI;

namespace zFramework
{
    public static class TransformManager
    {
        public static Button FindButton(string ButtonName)
        {
            return GameObject.Find(ButtonName).GetComponent<Button>();
        }
    }

}
