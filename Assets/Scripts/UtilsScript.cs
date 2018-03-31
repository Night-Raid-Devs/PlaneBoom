using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilsScript {
    public static GameObject[] FindGameObjectsWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Select(obj => obj.transform.parent.gameObject).ToArray();
    }
}
