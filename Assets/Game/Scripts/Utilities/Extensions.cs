using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetElementLooping<T>(this List<T> list, int index)
    {
        var lastIndex = list.Count - 1;
        if (lastIndex == 0) return list[0];

        if (index > lastIndex) index %= list.Count;
        return list[index];
    }
}