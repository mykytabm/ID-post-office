using System;
using System.Collections;
using System.Globalization;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

public static class Utils
{
    public static IEnumerator Wait(float waitInMs, Action action)
    {
        yield return new WaitForSeconds(waitInMs);
        action();
    }

    public static string GoldToString(ulong gold)
    {
        var convertedGold = 0f;

        if (gold >= 100000000 && gold < 1000000000)
        {
            convertedGold = (float)gold / 1000000;
            return convertedGold.ToString("F2") + "M";
        }

        return gold.ToString("N0", new CultureInfo("en-US"));
    }
}