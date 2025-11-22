using System.Collections.Generic;
using UnityEngine;

public static class UIHelper
{
    public static Transform FindDeepChild(Transform root, string name, Dictionary<string, Transform> cache = null)
    {
        foreach (Transform child in root)
        {
            if (cache != null) {
                if (cache.ContainsKey(child.name)) 
                    return cache[child.name];
                else
                    cache.Add(child.name, child);
            }

            if (child.name == name) return child;

            Transform result = FindDeepChild(child, name , cache);

            if (result != null) return result;
        }
        return null;
    }
}
