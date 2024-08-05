using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace zFramework
{
    public static class TransformUtils
    {
        public static Button FindButton(string ButtonName)
        {
            return GameObject.Find(ButtonName).GetComponent<Button>();
        }
    }

}
